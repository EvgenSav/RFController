using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace RFController {
    public partial class DevicesForm : Form {
        ServiceForm serviceForm;
        TempForm tempForm;
        LogForm logForm;
        AddNewDevForm addNewDevForm;
        SettingsForm settingsForm;
        GraphForm tempGraphForm;

        Action<int> FormUpdater;
        MyDB<int, RfDevice> DevBase;
        MyDB<int, TempAtChannel> TemperatureLog;

        Dictionary<int, int> ControlsHash;
        MTRF Mtrf64;
        List<SortedDictionary<int, Control>> AllDevicesControls;
        Control Template;
        System.Timers.Timer t1;
        System.Timers.Timer t2;
        Control CurBright;
        int? LoopedDevKey;
        int SelectedTab = 0;

        public DevicesForm() {
            InitializeComponent();
            DevBase = GetDeviceBase();
            TemperatureLog = GetTempBase();
            Mtrf64 = new MTRF();
            List<Mtrf> connected = Mtrf64.GetAvailableComPorts();
            if (connected.Count != 0) {
                if (connected.Count > 1) {
                    ChooseModule c = new ChooseModule(Mtrf64, connected);
                    c.Show();
                } else {
                    Mtrf64.OpenPort(connected[0].ComPortName);
                }
            } else {
                MessageBox.Show("Mtrf not connected", "Warning!");
            }

            Template = groupBox1;
            flowLayoutPanel1.Controls.Remove(groupBox1);

            AllDevicesControls = new List<SortedDictionary<int, Control>>();
            AllDevicesControls.Add(new SortedDictionary<int, Control>());
            foreach (var item in DevBase.Data) {
                Control c = GetCopy(Template, 0);
                c.Text = item.Value[0].Name;
                flowLayoutPanel1.Controls.Add(c);
                AllDevicesControls[0].Add(item.Key, c);
            }

            ControlsHash = new Dictionary<int, int>();

            Mtrf64.NewDataReceived += Dev1_NewDataReceived;
            this.FormClosing += DevicesForm_FormClosing;

            FormUpdater = new Action<int>(UpdateForm);
            UpdateForm(0);
            t1 = new System.Timers.Timer { AutoReset = false, Interval = 500 };
            t1.Elapsed += T1_Elapsed;
            t2 = new System.Timers.Timer { AutoReset = true, Interval = 500 };
            t2.Elapsed += T2_Elapsed;
            InitRooms();
        }

        //initialize Rooms with devices controls
        void InitRooms() {
            var groupedByRoomDevices = from r in DevBase.Data
                                 where r.Value[r.Value.Count - 1].Room != null
                                 select new {
                                     Name = r.Value[r.Value.Count - 1].Room,
                                     Dev = r.Value[r.Value.Count - 1],
                                     Key = r.Key
                                 } into devs
                                 where devs.Dev.Room != null
                                 group devs by devs.Dev.Room;
            int tabIdx = 0;
            foreach (var room in groupedByRoomDevices) {
                TabControl.TabPageCollection tabPages = tabControl1.TabPages;
                tabPages.Add(room.Key);
                AllDevicesControls.Add(new SortedDictionary<int, Control>());
                tabIdx++;
                tabPages[tabIdx].BackColor = Color.White;
                tabPages[tabIdx].Controls.Add(new FlowLayoutPanel {
                    AutoSize = flowLayoutPanel1.AutoSize,
                    AutoSizeMode = flowLayoutPanel1.AutoSizeMode,
                    BackColor = flowLayoutPanel1.BackColor,
                    Location = flowLayoutPanel1.Location
                });
                foreach (var item in room) {
                    Control devControl = GetCopy(AllDevicesControls[0][item.Key], 0);
                    tabControl1.TabPages[tabIdx].Controls[0].Controls.Add(devControl);
                    AllDevicesControls[tabIdx].Add(item.Key, devControl);
                }
            }
        }
        //timer event handler for switch looping
        private void T2_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (LoopedDevKey != null) {
                RfDevice rfd = DevBase.Data[(int)LoopedDevKey][0];
                if (rfd.DevType > 0) {
                    Mtrf64.SendCmd(0, 2, NooCmd.Switch, rfd.Addr);
                } else {
                    Mtrf64.SendCmd(rfd.Channel, 0, NooCmd.Switch);
                }
            }
        }

        //timer event handler for regulating brightness
        private void T1_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            int DevKey = ControlsHash[CurBright.GetHashCode()];
            RfDevice Device = DevBase.Data[DevKey][0];
            int DevBright = 0;
            string bright = CurBright.Text.TrimEnd(' ', '%');
            Int32.TryParse(bright, out int result);
            if (Device.Type == NooDevType.PowerUnitF) { //Noo-F
                DevBright = Round(((float)result / 100) * 255);
                if (DevBright != 0) {
                    Mtrf64.SendCmd(0, Mode.FTx, NooCmd.SetBrightness, Device.Addr, d0: DevBright);
                }
            } else if (Device.Type == NooDevType.PowerUnit) { //Noo
                DevBright = Round(((float)result / 100) * 128);
                Mtrf64.SendCmd(Device.Channel, Mode.Tx, NooCmd.SetBrightness, fmt: 1, d0: DevBright);
            }
        }

        private void DevicesForm_FormClosing(object sender, FormClosingEventArgs e) {
            Mtrf64.NewDataReceived -= Dev1_NewDataReceived;
            if (logForm != null) logForm.Close();
            if (tempForm != null) tempForm.Close();
            if (serviceForm != null) serviceForm.Close();
            if (tempGraphForm != null) tempGraphForm.Close();
            TemperatureLog.SaveToFile(String.Format("{0} templog.json", DateTime.Now.ToShortDateString()));
            DevBase.SaveToFile("BindedDeviceList.json");
        }

        public void UpdateForm(int currentTabIdx) {
            //ICollection<int> keys1 = AllDevicesControls[currentTabIdx].Keys;
            //ICollection<int> keys2 = DevBase.Data.Keys;
            //IEnumerable<int> subKeys1 = keys1.Except(keys2);
            //IEnumerable<int> subKeys2 = keys2.Except(keys1);
            //if (subKeys1.Count() != 0) { //There are more controls than existing devices in the ControlForm
            //    foreach (var item in subKeys1) {
            //        Control c = AllDevicesControls[currentTabIdx][item];
            //        flowLayoutPanel1.Controls.Remove(c); //remove controls for non existing devices
            //        AllDevicesControls[currentTabIdx].Remove(item);
            //        break;
            //    }
            //}
            //if (subKeys2.Count() != 0) { //There are more existing devices than controls in the ControlForm
            //    foreach (var item in subKeys2) {
            //        Control c = GetCopy(Template, 0);
            //        flowLayoutPanel1.Controls.Add(c); //add controls
            //        AllDevicesControls[currentTabIdx].Add(item, c);
            //    }
            //}
            //update info of each device
            foreach (var EachDeviceControls in AllDevicesControls[currentTabIdx]) {
                RfDevice Device = DevBase.Data[EachDeviceControls.Key][0];
                EachDeviceControls.Value.Text = Device.Name.ToString();
                foreach (var item in EachDeviceControls.Value.ContextMenuStrip.Items) {
                    int contMenuStripHash = item.GetHashCode();
                    if (!ControlsHash.ContainsKey(contMenuStripHash)) {
                        ControlsHash.Add(contMenuStripHash, EachDeviceControls.Key);
                    }
                }
                if (!ControlsHash.ContainsKey(EachDeviceControls.Value.GetHashCode())) {
                    ControlsHash.Add(EachDeviceControls.Value.GetHashCode(), EachDeviceControls.Key);
                }
                Control.ControlCollection cc1 = EachDeviceControls.Value.Controls;
                foreach (Control control in cc1) {
                    switch (control.Name) {
                        case "TypeBox":
                            control.Text = GetDeviceType(DevBase.Data[EachDeviceControls.Key][0]);
                            break;
                        case "StatePictBox": //state indication
                            PictureBox pictureBox = (PictureBox)control;
                            pictureBox.BackColor = Color.LightGreen;
                            if (Device.Type == NooDevType.PowerUnit ||
                                Device.Type == NooDevType.PowerUnitF) { //power blocks
                                if (Device.State != 0) {
                                    control.Show();
                                } else {
                                    control.Hide();
                                }
                            } else { control.Hide(); }
                            break;

                        case "StateBox":
                            if (Device.Type == NooDevType.PowerUnit || Device.Type == NooDevType.PowerUnitF) {
                                int brightBoxHash = control.GetHashCode();
                                control.Visible = true;
                                float bright = ((float)Device.Bright / 255) * 100;
                                control.Text = Round(bright).ToString() + " %";

                                if (!ControlsHash.ContainsKey(brightBoxHash)) {
                                    ControlsHash.Add(brightBoxHash, EachDeviceControls.Key);
                                    control.MouseEnter += Control_MouseEnter;
                                    control.MouseLeave += Control_MouseLeave;
                                    control.MouseWheel += Bright_ValueChanged;
                                    control.MouseClick += Device_MouseClick;
                                }
                            } else if (Device.Type == NooDevType.Sensor) {
                                switch (Device.DevType) {
                                    case SensorsTypes.PT112:
                                        if (!TemperatureLog.Data.ContainsKey(EachDeviceControls.Key)) {
                                            TemperatureLog.Data.Add(EachDeviceControls.Key, new List<TempAtChannel>());
                                        }
                                        int dataCounts = TemperatureLog.Data[EachDeviceControls.Key].Count;
                                        if (dataCounts > 0) {
                                            TempAtChannel temp = TemperatureLog.Data[EachDeviceControls.Key][dataCounts - 1];
                                            control.Text = temp.ToString();
                                        } else {
                                            control.Text = "no data";
                                        }
                                        if (!ControlsHash.ContainsKey(control.GetHashCode())) {
                                            ControlsHash.Add(control.GetHashCode(), EachDeviceControls.Key);
                                            control.Click += ShowTemp_Click;
                                        }
                                        break;
                                    case SensorsTypes.PM112:
                                        if (!TemperatureLog.Data.ContainsKey(EachDeviceControls.Key)) {
                                            TemperatureLog.Data.Add(EachDeviceControls.Key, new List<TempAtChannel>());
                                        }
                                        int data_counts = TemperatureLog.Data[EachDeviceControls.Key].Count;
                                        if (data_counts > 0) {
                                            TempAtChannel temp = TemperatureLog.Data[EachDeviceControls.Key][data_counts - 1];

                                            control.Text = temp.CurrentTime.ToShortTimeString();
                                        } else {
                                            control.Text = "no data";
                                        }
                                        if (!ControlsHash.ContainsKey(control.GetHashCode())) {
                                            ControlsHash.Add(control.GetHashCode(), EachDeviceControls.Key);
                                            control.Click += ShowTemp_Click;
                                        }
                                        break;
                                    default:
                                        break;
                                }

                            } else {
                                control.Visible = false;
                            }
                            break;
                        case "DimmerEn":
                            if (Device.Type == NooDevType.PowerUnit ||
                                Device.Type == NooDevType.PowerUnitF) {

                                CheckBox cb = (CheckBox)control;
                                int DimBtnHash = control.GetHashCode();
                                if (!ControlsHash.ContainsKey(DimBtnHash)) {
                                    ControlsHash.Add(DimBtnHash, EachDeviceControls.Key);
                                    cb.CheckedChanged += Cb_CheckedChanged;
                                }
                                cb.Checked = Device.IsDimmable;
                            } else {
                                control.Visible = false;
                            }
                            break;
                    }
                }
            }
            Size s1 = flowLayoutPanel1.Size;
            s1.Height = s1.Height + 35;
            s1.Width = s1.Width + 15;
            tabControl1.Size = s1;

        }

        //reset focus from Bright Regulating Label        
        private void Control_MouseLeave(object sender, EventArgs e) {
            this.ActiveControl = null;
        }
        //set focus to Bright Regulating Label
        private void Control_MouseEnter(object sender, EventArgs e) {
            Label lb = (Label)sender;
            lb.Focus();
        }

        string GetDeviceType(RfDevice dev) {
            string res = "";
            switch (dev.Type) {
                case NooDevType.RemController:
                    res = "Пульт";
                    break;
                case NooDevType.Sensor:
                    switch (dev.DevType) {
                        case 1:
                            res = "PT112";
                            break;
                        case 2:
                            res = "PT111";
                            break;
                        case 3:
                            res = "PM111";
                            break;
                        case 5:
                            res = "PM112";
                            break;
                    }
                    break;
                case NooDevType.PowerUnitF:
                    switch (dev.DevType) {
                        case 0:
                            res = "MTRF-64";
                            break;
                        case 1:
                            res = "SLF-1-300";
                            break;
                        case 2:
                            res = "SRF-10-1000";
                            break;
                        case 3:
                            res = "SRF-1-3000";
                            break;
                        case 4:
                            res = "SRF-1-3000M";
                            break;
                        case 5:
                            res = "SUF-1-300";
                            break;
                        case 6:
                            res = "SRF-1-3000T";
                            break;
                    }
                    break;
            }
            return res;
        }

        int Round(float val) {
            if ((val - (int)val) > 0.5) return (int)val + 1;
            else return (int)val;
        }

        private void ShowTemp_Click(object sender, EventArgs e) {
            int DevKey = ControlsHash[sender.GetHashCode()];
            if (tempGraphForm != null) {
                tempGraphForm.Close();
            }
            tempGraphForm = new GraphForm(Mtrf64, TemperatureLog.Data[DevKey]);
            tempGraphForm.Show();
        }

        private void Bright_ValueChanged(object sender, MouseEventArgs e) {
            CurBright = (Control)sender;
            string bright = CurBright.Text.TrimEnd(' ', '%');
            Int32.TryParse(bright, out int result);
            if ((result > 0 || e.Delta > 0) && (e.Delta < 0 || result < 100)) {
                CurBright.Text = (result + (e.Delta / 120)).ToString() + " %";
                t1.Stop();
                //CurBright = c;
                t1.Start();
            }
        }

        private void Cb_CheckedChanged(object sender, EventArgs e) {
            CheckBox cb = (CheckBox)sender;
            int DevKey = sender.GetHashCode();
            RfDevice rfDev = DevBase.Data[ControlsHash[DevKey]][0];
            rfDev.IsDimmable = cb.Checked;
            DevBase.Data[ControlsHash[DevKey]][0] = rfDev;
            UpdateForm(0);
        }




        private void Dev1_NewDataReceived(object sender, EventArgs e) {
            ParseIncomingData();
            //Update 
            if (InvokeRequired) {
                BeginInvoke(FormUpdater, SelectedTab);
            } else {
                FormUpdater(SelectedTab);
            }
        }

        private Control GetCopy(Control c, int i) {
            Type t;
            ConstructorInfo[] ci;
            ParameterInfo[] p;

            t = c.GetType();
            ci = t.GetConstructors();
            p = ci[0].GetParameters();

            Control copy = (Control)ci[0].Invoke(p);
            copy.Size = c.Size;
            copy.ContextMenuStrip = new ContextMenuStrip();

            foreach (ToolStripMenuItem item in c.ContextMenuStrip.Items) {
                copy.ContextMenuStrip.Items.Add(item.Text);
            }

            copy.ContextMenuStrip.Items[0].Click += Remove_Click;
            copy.ContextMenuStrip.Items[1].Click += ShowInfo_Click;
            copy.ContextMenuStrip.Items[2].Click += Settings_Click;
            copy.ContextMenuStrip.Items[3].Click += SwitchLoop_Click;
            copy.MouseClick += Device_MouseClick;

            foreach (Control item in c.Controls) {
                t = item.GetType();
                ci = t.GetConstructors();
                p = ci[0].GetParameters();
                Control newCntrol = (Control)ci[0].Invoke(p);
                newCntrol.Font = item.Font;
                newCntrol.Name = item.Name;
                newCntrol.Size = item.Size;
                newCntrol.Text = item.Text;
                newCntrol.Location = item.Location;
                copy.Controls.Add(newCntrol);
            }
            return copy;
        }

        private void Device_MouseClick(object sender, MouseEventArgs e) {
            int DevKey = ControlsHash[sender.GetHashCode()];
            RfDevice Device = DevBase.Data[DevKey][0];
            if (Device.Type == NooDevType.PowerUnitF) { //Noo-F
                Mtrf64.SendCmd(Device.Channel, Mode.FTx, NooCmd.Switch, Device.Addr);
            } else if (Device.Type == NooDevType.PowerUnit) { //Noo
                Mtrf64.SendCmd(Device.Channel, Mode.Tx, NooCmd.Switch);
            }
        }
        #region Open DB
        private MyDB<int, TempAtChannel> GetTempBase() {
            MyDB<int, TempAtChannel> tempLog;
            try {
                using (StreamReader s1 = new StreamReader(new FileStream(String.Format("{0} templog.json", DateTime.Now.ToShortDateString()), FileMode.Open))) {
                    string strlog = s1.ReadToEnd();
                    JsonSerializerSettings set1 = new JsonSerializerSettings {
                        Formatting = Formatting.Indented
                    };
                    tempLog = JsonConvert.DeserializeObject<MyDB<int, TempAtChannel>>(strlog, set1);
                }
            } catch {
                tempLog = new MyDB<int, TempAtChannel>();
            }
            return tempLog;
        }

        private MyDB<int, RfDevice> GetDeviceBase() {
            MyDB<int, RfDevice> devBase;
            try {
                using (StreamReader s1 = new StreamReader(
                    new FileStream("BindedDeviceList.json", FileMode.Open))) {
                    string devs = s1.ReadToEnd();
                    JsonSerializerSettings set1 = new JsonSerializerSettings {
                        Formatting = Formatting.Indented
                    };
                    devBase = JsonConvert.DeserializeObject<MyDB<int, RfDevice>>(devs, set1);
                }
            } catch {
                devBase = new MyDB<int, RfDevice>();
            }
            return devBase;
        }
        #endregion
        #region Parsing received data
        private void ParseIncomingData() {
            RfDevice Device = new RfDevice();
            bool ContainsDevice = false;
            if (Mtrf64.rxBuf.AddrF != 0) {
                if (DevBase.Data.ContainsKey(Mtrf64.rxBuf.AddrF)) {
                    Device = DevBase.Data[Mtrf64.rxBuf.AddrF][0];
                    ContainsDevice = true;
                }
            } else {
                if (DevBase.Data.ContainsKey(Mtrf64.rxBuf.Ch)) {
                    Device = DevBase.Data[Mtrf64.rxBuf.Ch][0];
                    ContainsDevice = true;
                }
            }

            if (ContainsDevice)
                switch (Mtrf64.rxBuf.Cmd) {
                    case NooCmd.On:
                        if (Mtrf64.rxBuf.Mode == 0) {
                            Device.State = 1;
                            Device.Bright = Mtrf64.rxBuf.D0;
                        }
                        break;
                    case NooCmd.Off:
                        if (Mtrf64.rxBuf.Mode == 0) {
                            Device.State = 0;
                        }
                        break;
                    case NooCmd.SetBrightness:
                        if (Mtrf64.rxBuf.Mode == 0) {
                            Device.Bright = Mtrf64.rxBuf.D0;
                        }
                        break;
                    case NooCmd.Unbind:
                        Mtrf64.Unbind(Mtrf64.rxBuf.Ch, Mtrf64.rxBuf.Mode);
                        break;
                    case NooCmd.SensTempHumi:
                        Mtrf64.StoreTemperature(ref Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]);
                        TemperatureLog.Add(Mtrf64.rxBuf.Ch,
                        new TempAtChannel(DateTime.Now, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        break;
                    case NooCmd.TemporaryOn:
                        int DevKey = Mtrf64.rxBuf.Ch;
                        int count = TemperatureLog.Data[DevKey].Count;
                        if (count > 0) {
                            DateTime previous = TemperatureLog.Data[DevKey][count - 1].CurrentTime;
                            if (DateTime.Now.Subtract(previous).Seconds > 4) {
                                TemperatureLog.Add(Mtrf64.rxBuf.Ch, new TempAtChannel(DateTime.Now, Mtrf64.rxBuf.D0));
                            }
                        } else {
                            TemperatureLog.Add(Mtrf64.rxBuf.Ch, new TempAtChannel(DateTime.Now, Mtrf64.rxBuf.D0));
                        }
                        break;

                    case NooCmd.SendState:
                        //if(dev1.rxBuf.D0 == 5) { //suf-1-300
                        switch (Mtrf64.rxBuf.Fmt) {
                            case 0: //state
                                Device.DevType = Mtrf64.rxBuf.D0;
                                Device.FirmwareVer = Mtrf64.rxBuf.D1;
                                Device.State = Mtrf64.rxBuf.D2;
                                Device.Bright = Mtrf64.rxBuf.D3;
                                break;
                            case 16: //settings
                                Device.Settings = Mtrf64.rxBuf.D1 << 8 | Mtrf64.rxBuf.D0;
                                break;
                            case 17: //dimmer correction lvls
                                Device.DimCorrLvlHi = Mtrf64.rxBuf.D0;
                                Device.DimCorrLvlLow = Mtrf64.rxBuf.D1;
                                break;
                            case 18:
                                Device.OnLvl = Mtrf64.rxBuf.D0;
                                break;
                        }
                        break;
                    default:
                        break;

                }

        }
        #endregion
        #region Effects
        private void SeriesOnBtn_Click(object sender, EventArgs e) {
            foreach (var item in DevBase.Data) {
                RfDevice rfd = item.Value[0];
                Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.On);
            }
        }

        private void SeriesOffBtn_Click(object sender, EventArgs e) {
            foreach (var item in DevBase.Data) {
                RfDevice rfd = item.Value[0];
                Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.Off);
            }
        }
        private void FaderBtn_Click(object sender, EventArgs e) {
            for (int i = 0; i < 10; i++) {
                foreach (var item in DevBase.Data) {
                    RfDevice rfd = item.Value[0];
                    Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.On);
                }
                foreach (var item in DevBase.Data.Reverse()) {
                    RfDevice rfd = item.Value[0];
                    Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.Off);
                }
            }
        }
        #endregion
        #region Main menu
        private void ShowLog_MenuItem_Click(object sender, EventArgs e) {
            if (!ShowLog_MenuItem.Checked) {
                logForm = new LogForm(Mtrf64);
                logForm.FormClosing += (obj, args) => {
                    ShowLog_MenuItem.Checked = false;
                };
                logForm.Show();
                ShowLog_MenuItem.Checked = true;
            } else {
                logForm.Close();
                ShowLog_MenuItem.Checked = false;
            }
        }
        private void Temperature_MenuItem_Click(object sender, EventArgs e) {
            if (!Temperature_MenuItem.Checked) {
                tempForm = new TempForm(Mtrf64, TemperatureLog);
                tempForm.FormClosing += (obj, args) => {
                    Temperature_MenuItem.Checked = false;
                };
                tempForm.Show();
                Temperature_MenuItem.Checked = true;
            } else {
                tempForm.Close();
                Temperature_MenuItem.Checked = false;
            }
        }

        private void AddNewDevice_MenuItem_Click(object sender, EventArgs e) {
            addNewDevForm = new AddNewDevForm(DevBase, Mtrf64);
            addNewDevForm.Show();
            addNewDevForm.FormClosed += (_sender, args) => {
                this.UpdateForm(0);
            };
        }
        private void ServiceToolStrip_MenuItem_Click(object sender, EventArgs e) {
            if (!serviceToolStripMenuItem.Checked) {
                serviceForm = new ServiceForm(Mtrf64);
                serviceForm.FormClosed += (_sender, args) => {
                    serviceToolStripMenuItem.Checked = false;
                };
                serviceForm.Show();
                serviceToolStripMenuItem.Checked = true;
            } else {
                serviceForm.Close();
                serviceToolStripMenuItem.Checked = false;
            }
        }
        #endregion
        #region Context menu
        private void Settings_Click(object sender, EventArgs e) {
            int devAddr = ControlsHash[sender.GetHashCode()];
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 16);
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 17);
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 18);
            if (settingsForm != null) { settingsForm.Close(); }
            settingsForm = new SettingsForm(Mtrf64, DevBase, devAddr);
            settingsForm.Show();
        }
        private void ShowInfo_Click(object sender, EventArgs e) {
            int hash = sender.GetHashCode();
            RfDevice rf = DevBase.Data[ControlsHash[hash]][0];
            string res = String.Format("Device type: {0} \n" +
                "Firmware version: {1}", rf.DevType, rf.FirmwareVer);
            MessageBox.Show(res);
        }

        private void Remove_Click(object sender, EventArgs e) {
            int Devkey = ControlsHash[sender.GetHashCode()];

            foreach (TabPage PanelAtPage in tabControl1.TabPages) {
                if (AllDevicesControls[tabControl1.TabPages.IndexOf(PanelAtPage)].ContainsKey(Devkey)) {    //if this tab contains element, that we want to delete
                    Control toDelete = AllDevicesControls[tabControl1.TabPages.IndexOf(PanelAtPage)][Devkey];
                    PanelAtPage.Controls[0].Controls.Remove(toDelete);
                }
            }
            foreach (var ControlsAtPage in AllDevicesControls) {
                ControlsAtPage.Remove(Devkey);
            }
            DevBase.Data.Remove(Devkey);
            UpdateForm(SelectedTab);
        }

        private void SwitchLoop_Click(object sender, EventArgs e) {
            ToolStripMenuItem tmi = (ToolStripMenuItem)sender;
            if (!tmi.Checked) {
                tmi.Checked = true;
                t2.Start();
                LoopedDevKey = ControlsHash[sender.GetHashCode()];
            } else {
                tmi.Checked = false;
                t2.Stop();
            }
        }
        #endregion

        public static class SensorsTypes {
            public const int PT112 = 1;
            public const int PT111 = 2;
            public const int PM111 = 3;
            public const int PM112 = 5;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            SelectedTab = ((TabControl)sender).SelectedIndex;
            UpdateForm(SelectedTab);
        }
    }
}
