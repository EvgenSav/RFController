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
        RoomsManagerForm roomsManagerForm;
        ServiceForm serviceForm;
        LogForm logForm;
        AddNewDevForm addNewDevForm;
        SettingsForm settingsForm;
        GraphForm tempGraphForm;

        Action<int> FormUpdater;
        MyDB<int, RfDevice> DevBase;
        MyDB<int, List<TempAtChannel>> TemperatureLog;

        Dictionary<int, int> ControlsHash;
        MTRF Mtrf64;
        List<SortedDictionary<int, Control>> AllDevicesControls;
        List<string> Rooms;

        Control Template;
        System.Timers.Timer t1;
        System.Timers.Timer t2;
        Control CurBright;
        int? LoopedDevKey;
        int SelectedTab = 0;
        RfDevice WaitingForActionDev;

        public DevicesForm() {
            InitializeComponent();
            DevBase = GetDeviceBase();
            TemperatureLog = GetTempBase();

            try {
                using (StreamReader s1 = new StreamReader(new FileStream("rooms.json", FileMode.Open))) {
                    Rooms = JsonConvert.DeserializeObject<List<string>>(s1.ReadToEnd());
                }
            } catch {
                Rooms = new List<string>(new string[] { "All" });
            }

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


            ControlsHash = new Dictionary<int, int>();

            Mtrf64.NewDataReceived += Dev1_NewDataReceived;
            this.FormClosing += DevicesForm_FormClosing;

            FormUpdater = new Action<int>(UpdateForm);

            t1 = new System.Timers.Timer { AutoReset = false, Interval = 500 };
            t1.Elapsed += T1_Elapsed;
            t2 = new System.Timers.Timer { AutoReset = true, Interval = 250 };
            t2.Elapsed += T2_Elapsed;
            InitRooms();
            UpdateForm(0);
        }

        //initialize Rooms with devices controls
        void InitRooms() {
            //init room All(tab idx = 0)
            AllDevicesControls.Add(new SortedDictionary<int, Control>());
            TabControl.TabPageCollection tabPages = RoomSelector.TabPages;
            for (int tabIdx = 0; tabIdx < Rooms.Count; tabIdx++) {
                string curRoom = Rooms[tabIdx];
                if (tabPages.Count <= tabIdx) {
                    tabPages.Add(curRoom);
                    AllDevicesControls.Add(new SortedDictionary<int, Control>());
                }
                tabPages[tabIdx].BackColor = Color.White;

                tabPages[tabIdx].Controls.Add(new FlowLayoutPanel {
                    AutoSize = flowLayoutPanel1.AutoSize,
                    AutoSizeMode = flowLayoutPanel1.AutoSizeMode,
                    BackColor = flowLayoutPanel1.BackColor,
                    MinimumSize = flowLayoutPanel1.MinimumSize,
                    MaximumSize = flowLayoutPanel1.MaximumSize,
                    Location = flowLayoutPanel1.Location
                });
                if (curRoom != "All") {
                    var devsInRoom = from Device in DevBase.Data
                                     where Device.Value.Room == curRoom
                                     select Device;

                    foreach (var item in devsInRoom) {
                        AddControl(item.Key, curRoom);
                    }
                } else {
                    foreach (var item in DevBase.Data) {
                        AddControl(item.Key, curRoom);
                    }
                }
            }
        }

        //timer event handler for switch looping
        private void T2_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (LoopedDevKey != null) {
                RfDevice rfd = DevBase.Data[(int)LoopedDevKey];
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
            RfDevice Device = DevBase.Data[DevKey];
            int DevBright = 0;
            string bright = CurBright.Text.TrimEnd(' ', '%');
            Int32.TryParse(bright, out int result);
            if (Device.Type == NooDevType.PowerUnitF) { //Noo-F
                DevBright = Round(((float)result / 100) * 255);
                if (DevBright != 0) {
                    Mtrf64.SendCmd(0, Mode.FTx, NooCmd.SetBrightness, Device.Addr, d0: DevBright);
                }
            } else if (Device.Type == NooDevType.PowerUnit) { //Noo
                DevBright = 28 + result;
                Mtrf64.SendCmd(Device.Channel, Mode.Tx, NooCmd.SetBrightness, fmt: 1, d0: DevBright);
            }
        }

        private void DevicesForm_FormClosing(object sender, FormClosingEventArgs e) {
            Mtrf64.NewDataReceived -= Dev1_NewDataReceived;
            if (logForm != null) logForm.Close();
            if (serviceForm != null) serviceForm.Close();
            if (tempGraphForm != null) tempGraphForm.Close();
            if (roomsManagerForm != null) roomsManagerForm.Close();

            TemperatureLog.SaveToFile(String.Format("{0} templog.json", DateTime.Now.ToShortDateString()));
            DevBase.SaveToFile("BindedDeviceList.json");

            using (StreamWriter s1 = new StreamWriter(new FileStream("rooms.json", FileMode.Create, FileAccess.ReadWrite))) {
                s1.Write(JsonConvert.SerializeObject(Rooms, Formatting.Indented));
            }
        }

        public void UpdateForm(int currentTabIdx) {
            //update info of each device
            foreach (var EachDeviceControls in AllDevicesControls[currentTabIdx]) {
                RfDevice Device = DevBase.Data[EachDeviceControls.Key];
                EachDeviceControls.Value.Text = Device.Name.ToString();

                //Add context menu items hash for each device
                foreach (ToolStripMenuItem item in EachDeviceControls.Value.ContextMenuStrip.Items) {
                    int contMenuStripHash = item.GetHashCode();
                    switch (item.Text) {
                        case "Settings":
                            if (Device.Type == NooDevType.PowerUnitF) {
                                //int contMenuStripHash = item.GetHashCode();
                                if (!ControlsHash.ContainsKey(contMenuStripHash)) {
                                    ControlsHash.Add(contMenuStripHash, EachDeviceControls.Key);
                                }
                            } else {
                                item.Visible = false;
                            }
                            break;
                        case "Redirect To":
                            if (Device.Type != NooDevType.PowerUnit && Device.Type != NooDevType.PowerUnit) {
                                //int contMenuStripHash = item.GetHashCode();
                                if (!ControlsHash.ContainsKey(contMenuStripHash)) {
                                    item.MouseHover += RedirectTo_MouseHover;
                                    ControlsHash.Add(contMenuStripHash, EachDeviceControls.Key);
                                }
                            } else {
                                item.Visible = false;
                            }
                            break;
                        default:
                            //int contMenuStripHash = item.GetHashCode();
                            if (!ControlsHash.ContainsKey(contMenuStripHash)) {
                                ControlsHash.Add(contMenuStripHash, EachDeviceControls.Key);
                            }
                            break;
                    }
                }
                //Add groupbox(devices) items hash for each device
                if (!ControlsHash.ContainsKey(EachDeviceControls.Value.GetHashCode())) {
                    ControlsHash.Add(EachDeviceControls.Value.GetHashCode(), EachDeviceControls.Key);
                }
                Control.ControlCollection cc1 = EachDeviceControls.Value.Controls;
                foreach (Control control in cc1) {
                    switch (control.Name) {
                        case "TypeBox":
                            control.Text = Device.GetDeviceType();
                            if (Device.Type == NooDevType.PowerUnit || Device.Type == NooDevType.PowerUnitF) {
                                int typeBoxHash = control.GetHashCode();
                                if (!ControlsHash.ContainsKey(typeBoxHash)) {
                                    ControlsHash.Add(typeBoxHash, EachDeviceControls.Key);
                                    control.MouseClick += Device_MouseClick;
                                }
                            }
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
                                if (Device.Type == NooDevType.PowerUnitF) {
                                    float bright = ((float)Device.Bright / 255) * 100;
                                    control.Text = Round(bright).ToString() + " %";
                                } else {
                                    if (Device.State != 0 && Device.Bright > 28) {
                                        control.Text = (Device.Bright - 28).ToString() + " %";
                                    } else {
                                        control.Text = 0.ToString() + " %";
                                    }
                                }


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
                    }
                }
            }
            Size s1 = flowLayoutPanel1.Size;
            s1.Height = s1.Height + 35;
            s1.Width = s1.Width + 15;
            RoomSelector.Size = s1;
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
                t1.Start();
            }
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

        //Each device control it's a GroupBox control.
        //GroupBox control consists of:
        //1. ContextMenuStrip
        //   1.1 Remove device
        //   1.2 Show info
        //   1.3 Settings
        //   1.4 Switch Loop(for test, need to delete)
        //2. StatePictBox(green square in the up-right corner of groupbox) indicates On-Off state of power units
        //3. StateBox indicates bright lvl of  power units
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

            //Context menu for devices
            copy.ContextMenuStrip.Items[0].Click += RemoveDevice_Click;
            copy.ContextMenuStrip.Items[1].Click += ShowInfo_Click;
            copy.ContextMenuStrip.Items[2].Click += Settings_Click;
            copy.ContextMenuStrip.Items[3].Click += SwitchLoop_Click;
            copy.ContextMenuStrip.Items[4].MouseHover += MoveTo_MouseHover;

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
            RfDevice Device = DevBase.Data[DevKey];
            if (Device.Type == NooDevType.PowerUnitF) { //Noo-F
                Mtrf64.SendCmd(Device.Channel, Mode.FTx, NooCmd.Switch, Device.Addr);
            } else if (Device.Type == NooDevType.PowerUnit) { //Noo
                if (Device.State != 0) {
                    Mtrf64.SendCmd(Device.Channel, Mode.Tx, NooCmd.Off);
                } else {
                    Mtrf64.SendCmd(Device.Channel, Mode.Tx, NooCmd.On);
                }
            }
        }
        #region Open DB
        private MyDB<int, List<TempAtChannel>> GetTempBase() {
            MyDB<int, List<TempAtChannel>> tempLog;
            try {
                using (StreamReader s1 = new StreamReader(new FileStream(String.Format("{0} templog.json", DateTime.Now.ToShortDateString()), FileMode.Open))) {
                    string strlog = s1.ReadToEnd();
                    JsonSerializerSettings set1 = new JsonSerializerSettings {
                        Formatting = Formatting.Indented
                    };
                    tempLog = JsonConvert.DeserializeObject<MyDB<int, List<TempAtChannel>>>(strlog, set1);
                }
            } catch {
                tempLog = new MyDB<int, List<TempAtChannel>>();
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
                    Device = DevBase.Data[Mtrf64.rxBuf.AddrF];
                    ContainsDevice = true;
                }
            } else {
                if (DevBase.Data.ContainsKey(Mtrf64.rxBuf.Ch)) {
                    Device = DevBase.Data[Mtrf64.rxBuf.Ch];
                    ContainsDevice = true;
                }
            }

            if (ContainsDevice)
                switch (Mtrf64.rxBuf.Cmd) {
                    case NooCmd.Switch:
                        if (Device.Type == NooDevType.RemController && Device.Redirect.Count != 0) {
                            foreach (var item in Device.Redirect) {
                                RfDevice dev = DevBase.Data[item];
                                if (dev.Type == NooDevType.PowerUnitF) {
                                    Mtrf64.SendCmd(dev.Channel, Mode.FTx, NooCmd.Switch, dev.Addr);
                                } else {
                                    if (dev.State == 1) {
                                        Mtrf64.SendCmd(dev.Channel, Mode.Tx, NooCmd.Off, dev.Addr);
                                    } else {
                                        Mtrf64.SendCmd(dev.Channel, Mode.Tx, NooCmd.On, dev.Addr);
                                    }
                                }
                            }
                        }
                        break;
                    case NooCmd.On:
                        if (Mtrf64.rxBuf.Mode == 0) {
                            Device.State = 1;
                            if (Device.Type == NooDevType.PowerUnitF) {
                                Device.Bright = Mtrf64.rxBuf.D0;
                            }
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
                        //Mtrf64.Unbind(Mtrf64.rxBuf.Ch, Mtrf64.rxBuf.Mode);
                        break;
                    case NooCmd.SensTempHumi:
                        Mtrf64.StoreTemperature(ref Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]);
                        if (TemperatureLog.Data.ContainsKey(Mtrf64.rxBuf.Ch)) {
                            TemperatureLog.Data[Mtrf64.rxBuf.Ch].Add(new TempAtChannel(DateTime.Now, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        } else {
                            TemperatureLog.Data.Add(Mtrf64.rxBuf.Ch, new List<TempAtChannel>());
                            TemperatureLog.Data[Mtrf64.rxBuf.Ch].Add(new TempAtChannel(DateTime.Now, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        }
                        //TemperatureLog.Add(Mtrf64.rxBuf.Ch,
                        //new TempAtChannel(DateTime.Now, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        break;
                    case NooCmd.TemporaryOn:
                        int DevKey = Mtrf64.rxBuf.Ch;
                        if (!TemperatureLog.Data.ContainsKey(DevKey)) {
                            TemperatureLog.Data.Add(DevKey, new List<TempAtChannel>());
                        }
                        int count = TemperatureLog.Data[DevKey].Count;
                        if (count > 0) {
                            DateTime previous = TemperatureLog.Data[DevKey][count - 1].CurrentTime;
                            if (DateTime.Now.Subtract(previous).Seconds > 4) {
                                TemperatureLog.Data[DevKey].Add(new TempAtChannel(DateTime.Now, Mtrf64.rxBuf.D0));
                            }
                        } else {
                            TemperatureLog.Data[DevKey].Add(new TempAtChannel(DateTime.Now, Mtrf64.rxBuf.D0));
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
                RfDevice rfd = item.Value;
                Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.On);
            }
        }

        private void SeriesOffBtn_Click(object sender, EventArgs e) {
            foreach (var item in DevBase.Data) {
                RfDevice rfd = item.Value;
                Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.Off);
            }
        }
        private void FaderBtn_Click(object sender, EventArgs e) {
            for (int i = 0; i < 10; i++) {
                foreach (var item in DevBase.Data) {
                    RfDevice rfd = item.Value;
                    Mtrf64.SendCmd(rfd.Channel, mode: 0, NooCmd.On);
                }
                foreach (var item in DevBase.Data.Reverse()) {
                    RfDevice rfd = item.Value;
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

        private void AddNewDevice_MenuItem_Click(object sender, EventArgs e) {
            addNewDevForm = new AddNewDevForm(DevBase, Mtrf64, Rooms);
            addNewDevForm.Show();
            addNewDevForm.FormClosed += (_sender, args) => {
                if (addNewDevForm.AddingOk) {
                    WaitingForActionDev = addNewDevForm.Device;
                    int keyToAdd = addNewDevForm.KeyToAdd;
                    AddControl(keyToAdd, WaitingForActionDev.Room);
                    DevBase.Data.Add(keyToAdd, WaitingForActionDev);
                    this.UpdateForm(SelectedTab);
                }
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
        private void RoomsManagerToolStripMenuItem_Click(object sender, EventArgs e) {
            if (roomsManagerForm != null) {
                roomsManagerForm.Close();
            }
            roomsManagerForm = new RoomsManagerForm(Rooms);
            roomsManagerForm.Show();
            roomsManagerForm.FormClosed += RoomsManagerForm_FormClosed;
        }
        #endregion
        #region Context menu
        #region Settings
        private void Settings_Click(object sender, EventArgs e) {
            int devAddr = ControlsHash[sender.GetHashCode()];
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 16);
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 17);
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 18);
            if (settingsForm != null) { settingsForm.Close(); }
            settingsForm = new SettingsForm(Mtrf64, DevBase, devAddr);
            settingsForm.Show();
        }
        #endregion
        #region Show info
        private void ShowInfo_Click(object sender, EventArgs e) {
            int hash = sender.GetHashCode();
            RfDevice rf = DevBase.Data[ControlsHash[hash]];
            string res = String.Format("Device type: {0} \n" +
                "Firmware version: {1}", rf.DevType, rf.FirmwareVer);
            MessageBox.Show(res);
        }
        #endregion
        #region Remove device
        private void RemoveDevice_Click(object sender, EventArgs e) {

            int devKey = ControlsHash[sender.GetHashCode()];
            //find rooms that contains device
            var roomsToRemove = from room in AllDevicesControls
                                where room.ContainsKey(devKey)
                                select Rooms[AllDevicesControls.IndexOf(room)];
            
            RfDevice devToRemove = DevBase.Data[devKey];
            switch (devToRemove.Type) {
                case NooDevType.PowerUnit:
                    DialogResult step11 = MessageBox.Show("Delete device?", "Warning!", MessageBoxButtons.YesNo);
                    if (step11 == DialogResult.Yes) {
                        MessageBox.Show("After you click OK, you'de have about 15 sec. " +
                            "to confirm unbind by pressing service button at power unit");
                        Mtrf64.Unbind(devToRemove.Channel, Mode.Tx);
                        DialogResult step12 = MessageBox.Show("Unbind confirmed?", "Unbind confirmation", MessageBoxButtons.YesNo);
                        if (step12 == DialogResult.Yes) {
                            //delete controls of device in each room
                            foreach (string roomToRemove in roomsToRemove) {
                                RemoveControl(devKey, roomToRemove);
                            }
                            //Remove device from base
                            DevBase.Data.Remove(devKey);
                        }
                    }
                    break;
                case NooDevType.PowerUnitF:
                    DialogResult step21 = MessageBox.Show("Delete device?", "Warning!", MessageBoxButtons.YesNo);
                    if (step21 == DialogResult.Yes) {
                        Mtrf64.SendCmd(0, Mode.Service, 0, addr: devToRemove.Addr);
                        Mtrf64.Unbind(0, Mode.FTx, devToRemove.Addr);
                        //delete controls of device in each room
                        foreach (string roomToRemove in roomsToRemove) {
                            RemoveControl(devKey, roomToRemove);
                        }
                        //Remove device from base
                        DevBase.Data.Remove(devKey);
                    }
                    break;
                case NooDevType.RemController:
                    DialogResult step31 = MessageBox.Show("Delete device?", "Warning!", MessageBoxButtons.YesNo);
                    if (step31 == DialogResult.Yes) {
                        Mtrf64.Unbind(devToRemove.Channel, Mode.Rx);
                        //delete controls of device in each room
                        foreach (string roomToRemove in roomsToRemove) {
                            RemoveControl(devKey, roomToRemove);
                        }
                        //Remove device from base
                        DevBase.Data.Remove(devKey);
                    }
                    
                    break;
                case NooDevType.Sensor:
                    DialogResult step41 = MessageBox.Show("Delete device?", "Warning!", MessageBoxButtons.YesNo);
                    if (step41 == DialogResult.Yes) {
                        Mtrf64.Unbind(devToRemove.Channel, Mode.Rx);
                        //delete controls of device in each room
                        foreach (string roomToRemove in roomsToRemove) {
                            RemoveControl(devKey, roomToRemove);
                        }
                        //Remove device from base
                        DevBase.Data.Remove(devKey);
                    }
                    break;
            }
            UpdateForm(SelectedTab);
        }
        #endregion
        #region Switch Loop
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
        #region Move to
        //on mouse hover this function creates room list for mooving
        private void MoveTo_MouseHover(object sender, EventArgs e) {
            int DevKey = ControlsHash[sender.GetHashCode()];
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            List<string> curRoom = new List<string> {
                Rooms[SelectedTab]
            };
            menuItem.DropDownItems.Clear();
            int dropDownItemIdx = 0;
            foreach (string item in Rooms.Except(curRoom)) {
                menuItem.DropDownItems.Add(item);
                menuItem.DropDownItems[dropDownItemIdx].Click += MoveToNewRoom_Click;
                int hash = menuItem.DropDownItems[dropDownItemIdx].GetHashCode();
                if (!ControlsHash.ContainsKey(hash)) {
                    ControlsHash.Add(hash, DevKey);
                }
                dropDownItemIdx++;
            }

        }

        private void MoveToNewRoom_Click(object sender, EventArgs e) {
            ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;
            int hash = sender.GetHashCode();
            int devKey = ControlsHash[hash];

            RemoveControl(devKey, Rooms[SelectedTab]);
            AddControl(devKey, toolStripDropDownItem.Text);
            DevBase.Data[devKey].Room = toolStripDropDownItem.Text;
            toolStripDropDownItem.Dispose();
        }
        #endregion
        #region Redirect to
        private void RedirectTo_MouseHover(object sender, EventArgs e) {
            int devKey = ControlsHash[sender.GetHashCode()];
            RfDevice dev = DevBase.Data[devKey];
            ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;
            var redirectListeners = DevBase.Data.Where((x) =>
               x.Value.Type == NooDevType.PowerUnit || x.Value.Type == NooDevType.PowerUnitF
            );
            int num = 0;
            toolStripDropDownItem.DropDownItems.Clear();
            foreach (var item in redirectListeners) {
                toolStripDropDownItem.DropDownItems.Add(item.Value.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)toolStripDropDownItem.DropDownItems[num];
                tsmi.Name = item.Value.Name;
                tsmi.CheckOnClick = true;
                if (dev.Redirect.Contains(item.Key)) {
                    tsmi.Checked = true;
                }
                tsmi.CheckedChanged += RedirectToSelected_Click;
                ControlsHash.Add(tsmi.GetHashCode(), ControlsHash[sender.GetHashCode()]);
                num++;
            }
        }

        private void RedirectToSelected_Click(object sender, EventArgs e) {
            ToolStripMenuItem toolStripDropDownItem = (ToolStripMenuItem)sender;
            var findDevByName = DevBase.Data.Where(dev => dev.Value.Name == toolStripDropDownItem.Name);
            int hash = sender.GetHashCode();
            RfDevice redirectSource = DevBase.Data[ControlsHash[hash]];
            if (toolStripDropDownItem.Checked) {
                foreach (var item in findDevByName) {
                    if (!redirectSource.Redirect.Contains(item.Key)) {
                        redirectSource.Redirect.Add(item.Key);
                    }
                }
            } else {
                foreach (var item in findDevByName) {
                    if (redirectSource.Redirect.Contains(item.Key)) {
                        redirectSource.Redirect.Remove(item.Key);
                    }
                }
            }
            //foreach (var item in redirectSource.Redirect) {
            //    string res = String.Format("Redirect \n From: {0} \n To: {1}", redirectSource.Name, DevBase.Data[item].Name);
            //    MessageBox.Show(res);
            //}
        }
        #endregion
        #endregion

        public static class SensorsTypes {
            public const int PT112 = 1;
            public const int PT111 = 2;
            public const int PM111 = 3;
            public const int PM112 = 5;
        }

        void AddControl(int devKeyToAdd, string roomToAdd) {
            int roomIdx = Rooms.IndexOf(roomToAdd);
            Control devControl = GetCopy(Template, 0);
            if (!AllDevicesControls[roomIdx].ContainsKey(devKeyToAdd)) {
                AllDevicesControls[roomIdx].Add(devKeyToAdd, devControl);
                RoomSelector.TabPages[roomIdx].Controls[0].Controls.Add(devControl);
            }

        }
        void RemoveControl(int devKey, string roomToRemove) {
            Control toRemove;
            //string roomToRemove = DevBase.Data[devKey].Room;
            if (roomToRemove != null) { //delete from room
                int roomIdx = Rooms.IndexOf(roomToRemove);
                toRemove = AllDevicesControls[roomIdx][devKey];
                AllDevicesControls[roomIdx].Remove(devKey);
                RoomSelector.TabPages[roomIdx].Controls[0].Controls.Remove(toRemove);
            }
        }

        private void RoomSelector_SelectedIndexChanged(object sender, EventArgs e) {
            SelectedTab = ((TabControl)sender).SelectedIndex;
            UpdateForm(SelectedTab);
        }
        //Updating form after add/remove room
        private void RoomsManagerForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (RoomSelector.TabCount != Rooms.Count) {
                if (RoomSelector.TabCount > Rooms.Count) { //delete room tab
                    foreach (TabPage tabpage in RoomSelector.TabPages) {
                        if (!Rooms.Contains(tabpage.Text)) { //find room tab to delete
                            int tabIdx = RoomSelector.TabPages.IndexOf(tabpage);
                            RoomSelector.TabPages.Remove(tabpage);
                            AllDevicesControls.RemoveAt(tabIdx); //remove from device controls
                        }
                    }
                } else { //add room tab
                    List<string> roomsTab = new List<string>();
                    foreach (TabPage tabPage in RoomSelector.TabPages) {
                        roomsTab.Add(tabPage.Text);
                    }
                    foreach (var item in Rooms.Except(roomsTab)) {
                        RoomSelector.TabPages.Add(item);
                        RoomSelector.TabPages[Rooms.IndexOf(item)].Controls.Add(new FlowLayoutPanel {
                            AutoSize = flowLayoutPanel1.AutoSize,
                            AutoSizeMode = flowLayoutPanel1.AutoSizeMode,
                            BackColor = flowLayoutPanel1.BackColor,
                            MinimumSize = flowLayoutPanel1.MinimumSize,
                            MaximumSize = flowLayoutPanel1.MaximumSize,
                            Location = flowLayoutPanel1.Location
                        });
                        AllDevicesControls.Add(new SortedDictionary<int, Control>());

                    }
                }
            }
        }
    }
}
