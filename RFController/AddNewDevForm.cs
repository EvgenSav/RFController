using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFController {
    public partial class AddNewDevForm : Form {
        MyDB<int, RfDevice> DevList;    //device list
        MTRF dev1;                      //MTRF driver
        RfDevice rfDev;
        Action FormUpdater;
        int FindedChannel;
        DevType SelectedType;
        bool WaitingBindFlag = false;

        public AddNewDevForm(MyDB<int, RfDevice> devList, MTRF dev) {
            InitializeComponent();
            Size = new Size(0, 185);

            FormUpdater = new Action(UpdateForm);

            DevList = devList;
            dev1 = dev;

            NooDevType[] types = {
                new NooDevType("Пульт / датчик",                DevType.RC),
                new NooDevType("Сил. блок без обр. связи",      DevType.PB),
                new NooDevType("Сил. блок с обр. связью",       DevType.PB_F)
            };

            DevTypeBox.DataSource = types;
            DevTypeBox.ValueMember = "Type";
            DevTypeBox.DisplayMember = "TypeName";

            DevTypeBox.Enabled = false;
            Step3ToolTip.Visible = false;
            Step4Tooltip.Visible = false;
            Step5ToolTip.Visible = false;
            Step6ToolTip.Visible = false;

            BindBtn.Visible = false;

            Status.Visible = true;


            dev1.NewDataReceived += Dev1_NewDataReceived;
            this.FormClosing += AddNewForm_FormClosing;

            timer1.Tick += Tmr_Tick;
        }

        private void AddNewForm_FormClosing(object sender, FormClosingEventArgs e) {
            dev1.NewDataReceived -= Dev1_NewDataReceived;
        }

        private void Dev1_NewDataReceived(object sender, EventArgs e) {
            if (WaitingBindFlag) {
                switch (SelectedType) {
                    case DevType.RC:
                        if (FindedChannel == dev1.rxBuf.Ch && dev1.rxBuf.Mode == 1) {
                            DevList.Add(FindedChannel, rfDev);
                            this.BeginInvoke(FormUpdater);
                        }
                        break;
                    case DevType.PB_F:
                        if (dev1.rxBuf.Mode == 2 && dev1.rxBuf.Ctr == 3) {
                            WaitingBindFlag = false;
                            rfDev.Addr = dev1.rxBuf.AddrF;
                            DevList.Add(dev1.rxBuf.AddrF, rfDev);
                            this.BeginInvoke(FormUpdater);
                        }
                        break;
                    default:
                        break;
                }                
            }            
        }


        private void Tmr_Tick(object sender, EventArgs e) {
            timer1.Stop();
            if (WaitingBindFlag) {
                Status.BackColor = Color.Red;
                Status.Text = "Device not added";
                WaitingBindFlag = false;
                timer1.Interval = 1500;
                timer1.Start();
            } else {
                this.Close();
            }
        }

        public void UpdateForm() {
            Status.BackColor = Color.LightGreen;
            Status.Text = "Device added";
            WaitingBindFlag = false;
            timer1.Interval = 1500;
            timer1.Start();
        }


        private void DevNameBox_KeyUp(object sender, KeyEventArgs e) {
            if (DevNameBox.Text.Length >= 5) {
                //Step 1 - entering name for device
                if (!DevTypeBox.Enabled) {
                    DevTypeBox.Enabled = true;
                    Step2ToolTip.Enabled = true;
                    Step1ToolTip.BackColor = Color.LightGreen;
                }
            } else {
                Step1ToolTip.BackColor = Color.Empty;
                DevTypeBox.Enabled = false;
                Step2ToolTip.Enabled = false;
            }
        }

        private void DevTypeBox_SelectionChangeCommitted(object sender, EventArgs e) {
            SelectedType = (DevType)DevTypeBox.SelectedValue;
            dev1.BindOn(0, 1, bindOff: true);                                           //send disable bind if enabled
            Step2ToolTip.BackColor = Color.LightGreen;                          //indicate step 2 - done
            FindedChannel = FindEmptyChannel((int)DevTypeBox.SelectedValue);    //find empty channel
            if (FindedChannel != -1) {
                rfDev = new RfDevice();
                rfDev.Name = DevNameBox.Text;
                rfDev.Type = (int)SelectedType;
                rfDev.Channel = FindedChannel;

                switch (SelectedType) {
                    case DevType.RC:
                        dev1.BindOn(FindedChannel, 1);  //enable bind at finded chnannel
                        Step3ToolTip.Text = "Step 3. Press service button";
                        Step3ToolTip.Visible = true;
                        Size = new Size(0, 220);
                        Step4Tooltip.Visible = false;
                        Status.Text = "Waiting...";

                        WaitingBindFlag = true;
                        timer1.Interval = 25000;
                        timer1.Start();
                        break;
                    case DevType.PB:
                        WaitingBindFlag = false;
                        Step3ToolTip.Text = "Step 3. Press service button. (LED should start blinking)";
                        Step3ToolTip.Visible = true;
                        Step4Tooltip.Visible = true;
                        BindBtn.Visible = true;
                        Size = new Size(0, 295);
                        break;
                    case DevType.PB_F:
                        WaitingBindFlag = false;
                        Step3ToolTip.Text = "Step 3. Press service button. (LED should start blinking)";
                        Step3ToolTip.Visible = true;
                        Step4Tooltip.Visible = true;
                        BindBtn.Visible = true;
                        Size = new Size(0, 295);
                        break;
                }
            }

        }




        private int FindEmptyChannel(int mode) {
            int FAddrCount = 0;
            //Noo-F mode
            if (mode == 2) {
                var res = DevList.Data.Where((x) => { return (x.Value[0].Type == 2); });
                foreach (var item in res) {
                    FAddrCount++;
                    //MessageBox.Show(item.Key.ToString());
                }
                if (FAddrCount < 64) return 0; //noo F memory is Full
                else return -1;
            } else { //Noo
                for (int i = 0; i < 64; i++) {
                    if (DevList.Data.ContainsKey(i)) {
                        continue;
                    } else {
                        return i;
                    }
                }
                return -1; //noo memory is Full
            }
        }

        private void BindBtn_Click(object sender, EventArgs e) {
            if (SelectedType == DevType.PB_F) {
                dev1.BindOn(FindedChannel, (int)DevType.PB_F);
                Status.Text = "Waiting...";
                WaitingBindFlag = true;
                timer1.Interval = 1000;
                timer1.Start();
            } else {
                dev1.BindOn(FindedChannel, 0);
                Size = new Size(0, 405);
                Step5ToolTip.Visible = true;

                Status.Text = "Waiting for user confirm...";
                WaitingBindFlag = true;
                timer1.Interval = 25000;
                timer1.Start();
            }
            Step3ToolTip.BackColor = Color.LightGreen;
            Step4Tooltip.BackColor = Color.LightGreen;
            BindBtn.Enabled = false;
            NoStep5Btn.Enabled = true;
            YesStep5Btn.Enabled = true;
        }



        private void YesBtnClick(object sender, EventArgs e) {
            BindBtn.Enabled = false;
            Size = new Size(0, 510);
            Step6ToolTip.Visible = true;
            Step5ToolTip.BackColor = Color.LightGreen;
            NoStep5Btn.Enabled = false;
            YesStep5Btn.Enabled = false;
        }
        private void NoBtnClick(object sender, EventArgs e) {
            BindBtn.Enabled = true;
            NoStep5Btn.Enabled = false;
        }

        private void OkBtn_Click(object sender, EventArgs e) {
            Step6ToolTip.BackColor = Color.LightGreen;
            DevList.Add(FindedChannel, rfDev);
            WaitingBindFlag = false;
            UpdateForm();
        }
    }


    public enum DevType { RC = 0, PB, PB_F };

    public struct NooDevType {
        public DevType Type { get; private set; }
        public string TypeName { get; private set; }
        public NooDevType(string tName, DevType type) {
            Type = type;
            TypeName = tName;
        }
    }
}
