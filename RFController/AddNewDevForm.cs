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
        List<string> Rooms;
        
        Action FormUpdater;
        int FindedChannel;
        int SelectedType;
        bool WaitingBindFlag = false;

        public RfDevice Device { get; private set; }
        public int KeyToAdd { get; private set; }
        public bool AddingOk { get; private set; }

        public AddNewDevForm(MyDB<int, RfDevice> devList, MTRF dev, List<string> rooms) {
            InitializeComponent();
            Size = new Size(0, 185);

            FormUpdater = new Action(UpdateForm);

            DevList = devList;
            dev1 = dev;
            Rooms = rooms;

            DevTypeBox.DataSource = new[] {
                new { Name = "Пульт", Value = NooDevType.RemController },
                new { Name = "Сил. блок без обр. связи", Value = NooDevType.PowerUnit },
                new { Name = "Сил. блок с обр. связью", Value = NooDevType.PowerUnitF },
                new { Name = "Датчик", Value = NooDevType.Sensor },
                 };
            DevTypeBox.ValueMember = "Value";
            DevTypeBox.DisplayMember = "Name";

            RoomBox.DataSource = Rooms;

            DevTypeBox.Enabled = false;
            RoomBox.Enabled = false;

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
                    case NooDevType.PowerUnitF:
                        if (dev1.rxBuf.Mode == 2 && dev1.rxBuf.Ctr == 3) {
                            WaitingBindFlag = false;
                            Device.Addr = dev1.rxBuf.AddrF;
                            KeyToAdd = Device.Addr;
                            //DevList.Add(dev1.rxBuf.AddrF, Device);
                            this.BeginInvoke(FormUpdater);
                        }
                        break;
                    case NooDevType.Sensor:
                        if (dev1.rxBuf.Cmd == NooCmd.Bind && dev1.rxBuf.Fmt == 1 &&
                            FindedChannel == dev1.rxBuf.Ch && dev1.rxBuf.Mode == 1) {
                            WaitingBindFlag = false;
                            Device.DevType = dev1.rxBuf.D0;
                            KeyToAdd = FindedChannel;
                            //DevList.Add(FindedChannel, Device);
                            this.BeginInvoke(FormUpdater);
                        }
                        break;
                    default:
                        if (dev1.rxBuf.Cmd == NooCmd.Bind && FindedChannel == dev1.rxBuf.Ch
                            && dev1.rxBuf.Mode == 1) {
                            WaitingBindFlag = false;
                            //DevList.Add(FindedChannel, Device);
                            KeyToAdd = FindedChannel;
                            this.BeginInvoke(FormUpdater);
                        }
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
            AddingOk = true;
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
            RoomBox.Enabled = true;
        }

        private int FindEmptyChannel(int mode) {
            int FAddrCount = 0;
            //Noo-F mode
            if (mode == NooDevType.PowerUnitF) {
                var res = DevList.Data.Where((x) => { return (x.Value.Type == NooDevType.PowerUnitF); });
                foreach (var item in res) {
                    FAddrCount++;
                    //MessageBox.Show(item.Key.ToString());
                }
                if (FAddrCount < 64) return 0; 
                else return -1; //noo F memory is Full
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
            if (SelectedType == NooDevType.PowerUnitF) {
                dev1.SendCmd(0, NooMode.FTx, NooCmd.Bind);
                Status.Text = "Waiting...";
                WaitingBindFlag = true;
                timer1.Interval = 1000;
                timer1.Start();
            } else {
                dev1.SendCmd(FindedChannel, NooMode.Tx, NooCmd.Bind);
                Size = new Size(0, 465);
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
            Size = new Size(0, 570);
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
            KeyToAdd = FindedChannel;
            //DevList.Add(FindedChannel, Device);
            WaitingBindFlag = false;
            UpdateForm();
        }

        private void RoomBox_SelectionChangeCommitted(object sender, EventArgs e) {
            SelectedType = (int)DevTypeBox.SelectedValue;                
            dev1.SendCmd(0, 0, 0, MtrfMode: NooCtr.BindModeDisable); //send disable bind if enabled
            Step2ToolTip.BackColor = Color.LightGreen;         //indicate step 2 - done
            FindedChannel = FindEmptyChannel(SelectedType);    //find empty channel
            if (FindedChannel != -1) {
                Device = new RfDevice {
                    Name = DevNameBox.Text,
                    Type = SelectedType,
                    Channel = FindedChannel,
                    Room = (string) RoomBox.SelectedValue
                };

                switch (SelectedType) {
                    case NooDevType.PowerUnit:
                        WaitingBindFlag = false;
                        Step3ToolTip.Text = "Step 3. Press service button. (LED should start blinking)";
                        Step3ToolTip.Visible = true;
                        Step4Tooltip.Visible = true;
                        BindBtn.Visible = true;
                        Size = new Size(0, 355);
                        break;
                    case NooDevType.PowerUnitF:
                        WaitingBindFlag = false;
                        Step3ToolTip.Text = "Step 3. Press service button. (LED should start blinking)";
                        Step3ToolTip.Visible = true;
                        Step4Tooltip.Visible = true;
                        BindBtn.Visible = true;
                        Size = new Size(0, 355);
                        break;
                    default: //NooDevType.RemController or NooDevType.Sensor     
                        dev1.SendCmd(FindedChannel, NooMode.Rx, 0, MtrfMode: NooCtr.BindModeEnable); //enable bind at finded chnannel
                        Step3ToolTip.Text = "Step 3. Press service button";
                        Step3ToolTip.Visible = true;
                        Size = new Size(0, 280);
                        Step4Tooltip.Visible = false;
                        Status.Text = "Waiting...";

                        WaitingBindFlag = true;
                        timer1.Interval = 25000;
                        timer1.Start();
                        break;
                }
            }
        }
    }
}
