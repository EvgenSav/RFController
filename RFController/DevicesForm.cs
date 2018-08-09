using System;
using System.Collections.Generic;
using System.Collections;
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
        RoomsManagerForm roomsManager;
        ServiceForm service;
        LogForm log;
        AddNewDevForm addNewDev;
        SettingsForm settings;
        GraphForm tempGraph;
        SceneryManager sceneryManager;

        Action<int> FormUpdater;
        MyDB<int, RfDevice> DevBase;
        public static MyDB<int, List<ILogItem>> ActionLog { get; private set; }

        MTRF Mtrf64;

        List<string> Rooms;
        List<Scenery> Sceneries;
        Control Template;
        System.Timers.Timer t1;
        System.Timers.Timer t2;
        Control CurBright;
        int? LoopedDevKey;
        int SelectedTab = 0;
        RfDevice WaitingForActionDev;

        public DevicesForm() {
            InitializeComponent();
            ActionLog = GetActionLog();
            DevBase = GetDeviceBase();
            
            Rooms = GetRooms();
            Sceneries = GetSceneries();

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
            DevicesPanel.Controls.Remove(groupBox1);

            Mtrf64.NewDataReceived += Dev1_NewDataReceived;
            this.FormClosing += DevicesForm_FormClosing;

            FormUpdater = new Action<int>(UpdateForm);

            t1 = new System.Timers.Timer { AutoReset = false, Interval = 500 };
            t1.Elapsed += T1_Elapsed;
            t2 = new System.Timers.Timer { AutoReset = true, Interval = 250 };
            t2.Elapsed += T2_Elapsed;

            ReInitSceneriesPanel();
            InitRooms();
            UpdateForm(0);
        }

        void ReInitSceneriesPanel() {
            ScenariesPanel.Controls.Clear();
            foreach (var item in Sceneries) {
                Button scenCallBtn = new Button {
                    Text = item.Name,
                    Name = item.Name,
                    Size = new Size(40, 40),
                    Margin = new Padding(0, 0, 0, 0)
                };
                scenCallBtn.Click += ScenCallBtn_Click;
                ScenariesPanel.Controls.Add(scenCallBtn);
            }
        }

        private void ScenCallBtn_Click(object sender, EventArgs e) {
            Button pressedBtn = (Button)sender;
            Scenery calledScenery = Sceneries.Find(new Predicate<Scenery>((scen) => { return (scen.Name == pressedBtn.Text); }));
            foreach (var scenItem in calledScenery.SceneryData) {
                if (DevBase.Data.ContainsKey(scenItem.Key)) {
                    if (scenItem.Value.State > 0) {
                        DevBase.Data[scenItem.Key].SetBright(Mtrf64, scenItem.Value.Bright);
                    } else {
                        DevBase.Data[scenItem.Key].SetOff(Mtrf64);
                    }
                }
            }
        }

        //initialize Rooms with devices controls
        void InitRooms() {
            TabControl.TabPageCollection tabPages = RoomSelector.TabPages;
            for (int tabIdx = 0; tabIdx < Rooms.Count; tabIdx++) {
                string curRoom = Rooms[tabIdx];
                if (tabPages.Count <= tabIdx) {
                    tabPages.Add(curRoom);
                }
                tabPages[tabIdx].BackColor = Color.White;
                if (tabIdx != 0) {
                    tabPages[tabIdx].Controls.Add(new FlowLayoutPanel {
                        Name = curRoom,
                        AutoSize = DevicesPanel.AutoSize,
                        AutoSizeMode = DevicesPanel.AutoSizeMode,
                        BackColor = DevicesPanel.BackColor,
                        MinimumSize = DevicesPanel.MinimumSize,
                        MaximumSize = DevicesPanel.MaximumSize,
                        Location = DevicesPanel.Location
                    });
                }
                if (curRoom != "All") {
                    var devsInRoom = from Device in DevBase.Data
                                     where Device.Value.Room == curRoom
                                     select Device;

                    foreach (var item in devsInRoom) {
                        DevView view = new DevView(item.Value);
                        item.Value.Views.Add(curRoom, view);
                        AddControl(item.Key, curRoom);
                    }
                } else {
                    foreach (var item in DevBase.Data) {
                        DevView view = new DevView(item.Value);
                        view.Info_mi_Click = new EventHandler(ShowInfo_Click);
                        view.Remove_mi_Click = new EventHandler(RemoveDevice_Click);
                        view.MoveToRoom_mi_Click = new EventHandler(MoveTo_MouseHover);
                        view.Settings_mi_Click = new EventHandler(Settings_Click);
                        view.RedirectTo_mi_Click = new EventHandler(RedirectTo_MouseHover);
                        view.BrightChanged = new EventHandler<MouseEventArgs>(Bright_ValueChanged);
                        view.ShowActionLogClicked = new EventHandler(ShowActionLog_Click);
                        view.DevClicked = new EventHandler(Device_Click);
                        item.Value.Views.Add(curRoom, view);
                        AddControl(item.Key, curRoom);
                    }
                }
            }
        }

        //timer event handler for switch looping
        private void T2_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (LoopedDevKey != null) {
                RfDevice rfd = DevBase.Data[(int)LoopedDevKey];
                rfd.SetSwitch(Mtrf64);
            }
        }

        //timer event handler for regulating brightness
        private void T1_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            int DevKey = (int)CurBright.Tag;
            RfDevice Device = DevBase.Data[DevKey];
            string bright = CurBright.Text.TrimEnd(' ', '%');
            Int32.TryParse(bright, out int brightRes);
            if (brightRes <= 100) {
                Device.SetBright(Mtrf64, brightRes);
            }
        }

        private void DevicesForm_FormClosing(object sender, FormClosingEventArgs e) {
            Mtrf64.NewDataReceived -= Dev1_NewDataReceived;
            if (log != null) log.Close();
            if (service != null) service.Close();
            if (tempGraph != null) tempGraph.Close();
            if (roomsManager != null) roomsManager.Close();
            if (sceneryManager != null) sceneryManager.Close();

            ActionLog.SaveToFile(String.Format("{0} templog.json", DateTime.Now.ToShortDateString()));
            DevBase.SaveToFile("BindedDeviceList.json");

            using (StreamWriter s1 = new StreamWriter(new FileStream("rooms.json", FileMode.Create, FileAccess.ReadWrite))) {
                s1.Write(JsonConvert.SerializeObject(Rooms, Formatting.Indented));
            }
            using (StreamWriter s1 = new StreamWriter(new FileStream("sceneries.json", FileMode.Create, FileAccess.ReadWrite))) {
                s1.Write(JsonConvert.SerializeObject(Sceneries, Formatting.Indented));
            }
        }

        public void UpdateForm(int currentTabIdx) {

            Size s1 = DevicesPanel.Size;

            s1.Height = s1.Height + 32;
            s1.Width += 15;
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

        public static int Round(float val) {
            if ((val - (int)val) > 0.5) return (int)val + 1;
            else return (int)val;
        }

        private void ShowActionLog_Click(object sender, EventArgs e) {
            int devKey;
            if ((sender as ToolStripMenuItem) != null) {
                devKey = (int)((ToolStripMenuItem)sender).Tag;
            } else {
                devKey = (int)((Control)sender).Tag;
            }
            
            if (tempGraph != null) {
                tempGraph.Close();
            }
            tempGraph = new GraphForm(Mtrf64, DevBase.Data[devKey].Log);
            tempGraph.Show();
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
        //   1.5 Move To(move deivices besides rooms)
        //   1.6 Redirect To(for RC)
        //2. StatePictBox(green square in the up-right corner of groupbox) indicates On-Off state of power units
        //3. StateBox indicates bright lvl of  power units
        

        private void Device_Click(object sender, EventArgs e) {
            int DevKey = (int)((Control)sender).Tag;
            RfDevice Device = DevBase.Data[DevKey];
            Device.SetSwitch(Mtrf64);
        }
        #region Open DB
        private MyDB<int, List<ILogItem>> GetActionLog() {
            MyDB<int, List<ILogItem>> tempLog;
            try {
                using (StreamReader s1 = new StreamReader(new FileStream(String.Format("{0} templog.json", DateTime.Now.ToShortDateString()), FileMode.Open))) {
                    string strlog = s1.ReadToEnd();
                    JsonSerializerSettings set1 = new JsonSerializerSettings {
                        Formatting = Formatting.Indented,
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    tempLog = JsonConvert.DeserializeObject<MyDB<int, List<ILogItem>>>(strlog, set1);
                }
            } catch {
                tempLog = new MyDB<int, List<ILogItem>>();
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
        private List<string> GetRooms() {
            List<string> rooms;
            try {
                using (StreamReader s1 = new StreamReader(new FileStream("rooms.json", FileMode.Open))) {
                    rooms = JsonConvert.DeserializeObject<List<string>>(s1.ReadToEnd());
                }
            } catch {
                rooms = new List<string>(new string[] { "All" });
            }
            return rooms;
        }
        private List<Scenery> GetSceneries() {
            List<Scenery> sceneries;
            try {
                using (StreamReader s1 = new StreamReader(new FileStream("sceneries.json", FileMode.Open))) {
                    JsonSerializerSettings jsonSet = new JsonSerializerSettings {
                        Formatting = Formatting.Indented
                    };
                    sceneries = JsonConvert.DeserializeObject<List<Scenery>>(s1.ReadToEnd(), jsonSet);
                }
            } catch {
                sceneries = new List<Scenery>(64);
            }
            return sceneries;
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

            if (ContainsDevice) {
                switch (Mtrf64.rxBuf.Cmd) {
                    case NooCmd.Switch:
                        //redirect
                        if (Device.Type == NooDevType.RemController && Device.Redirect.Count != 0) { 
                            foreach (var item in Device.Redirect) {
                                RfDevice dev = DevBase.Data[item];
                                dev.SetSwitch(Mtrf64);
                            }                            
                        }
                        //Device.Log.Add(new LogItem(DateTime.Now, Device.State));
                        Device.Log.Add(new LogItem(DateTime.Now, NooCmd.Switch));
                        break;
                    case NooCmd.On:
                        if (Mtrf64.rxBuf.Mode == 0) {
                            Device.State = 1;
                            if (Device.Type == NooDevType.PowerUnitF) {
                                Device.Bright = Mtrf64.rxBuf.D0;
                            }
                            Device.Log.Add(new PuLogItem(DateTime.Now, NooCmd.On, Device.State,Device.Bright));
                        }
                        break;
                    case NooCmd.Off:
                        if (Mtrf64.rxBuf.Mode == 0) {
                            Device.State = 0;
                        }
                        Device.Log.Add(new PuLogItem(DateTime.Now, NooCmd.On, Device.State, Device.Bright));
                        break;
                    case NooCmd.SetBrightness:
                        Device.ReadSetBrightAnswer(Mtrf64);
                        break;
                    case NooCmd.Unbind:
                        //Mtrf64.Unbind(Mtrf64.rxBuf.Ch, Mtrf64.rxBuf.Mode);
                        break;
                    case NooCmd.SensTempHumi:
                        Mtrf64.StoreTemperature(ref Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]);
                        if (ActionLog.Data.ContainsKey(Mtrf64.rxBuf.Ch)) {
                            ActionLog.Data[Mtrf64.rxBuf.Ch].Add(new SensLogItem(DateTime.Now, NooCmd.SensTempHumi, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        } else {
                            ActionLog.Data.Add(Mtrf64.rxBuf.Ch, new List<ILogItem>());
                            ActionLog.Data[Mtrf64.rxBuf.Ch].Add(new SensLogItem(DateTime.Now, NooCmd.SensTempHumi, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        }
                        //TemperatureLog.Add(Mtrf64.rxBuf.Ch,
                        //new TempAtChannel(DateTime.Now, Mtrf64.LastTempBuf[Mtrf64.rxBuf.Ch]));
                        break;
                    case NooCmd.TemporaryOn:
                        int DevKey = Mtrf64.rxBuf.Ch;
                        if (!ActionLog.Data.ContainsKey(DevKey)) {
                            ActionLog.Data.Add(DevKey, new List<ILogItem>());
                        }
                        int count = ActionLog.Data[DevKey].Count;
                        if (count > 0) {
                            DateTime previous = ActionLog.Data[DevKey][count - 1].CurrentTime;
                            if (DateTime.Now.Subtract(previous).Seconds > 4) {
                                ActionLog.Data[DevKey].Add(new LogItem(DateTime.Now, Mtrf64.rxBuf.D0));
                            }
                        } else {
                            ActionLog.Data[DevKey].Add(new LogItem(DateTime.Now, Mtrf64.rxBuf.D0));
                        }
                        break;

                    case NooCmd.SendState:
                        //if(dev1.rxBuf.D0 == 5) { //suf-1-300
                        switch (Mtrf64.rxBuf.Fmt) {
                            case 0: //state
                                Device.ReadState(Mtrf64);
                                Device.Log.Add(new PuLogItem(DateTime.Now, Mtrf64.rxBuf.Cmd, Device.State, Device.Bright));
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
                foreach (var item in Device.Views) {
                    item.Value.UpdateView();
                }
            }

        }
        #endregion
        #region Main menu
        private void ShowLog_MenuItem_Click(object sender, EventArgs e) {
            if (!ShowLog_MenuItem.Checked) {
                log = new LogForm(Mtrf64);
                log.FormClosing += (obj, args) => {
                    ShowLog_MenuItem.Checked = false;
                };
                log.Show();
                ShowLog_MenuItem.Checked = true;
            } else {
                log.Close();
                ShowLog_MenuItem.Checked = false;
            }
        }

        private void AddNewDevice_MenuItem_Click(object sender, EventArgs e) {
            addNewDev = new AddNewDevForm(DevBase, Mtrf64, Rooms);
            addNewDev.Show();
            addNewDev.FormClosed += (_sender, args) => {
                if (addNewDev.AddingOk) {
                    WaitingForActionDev = addNewDev.Device;
                    int keyToAdd = addNewDev.KeyToAdd;
                    DevBase.Data.Add(keyToAdd, WaitingForActionDev);
                    AddControl(keyToAdd, WaitingForActionDev.Room);
                    this.UpdateForm(SelectedTab);
                }
            };
        }
        private void ServiceToolStrip_MenuItem_Click(object sender, EventArgs e) {
            if (!serviceToolStripMenuItem.Checked) {
                service = new ServiceForm(Mtrf64);
                service.FormClosed += (_sender, args) => {
                    serviceToolStripMenuItem.Checked = false;
                };
                service.Show();
                serviceToolStripMenuItem.Checked = true;
            } else {
                service.Close();
                serviceToolStripMenuItem.Checked = false;
            }
        }
        private void RoomsManagerToolStripMenuItem_Click(object sender, EventArgs e) {
            if (roomsManager != null) {
                roomsManager.Close();
            }
            roomsManager = new RoomsManagerForm(Rooms);
            roomsManager.Show();
            roomsManager.FormClosed += RoomsManagerForm_FormClosed;
        }
        private void SceneryManagerToolStripMenuItem_Click(object sender, EventArgs e) {
            if (sceneryManager != null) {
                sceneryManager.Close();
            }
            sceneryManager = new SceneryManager(DevBase, Sceneries);
            sceneryManager.FormClosed += SceneryManager_FormClosed;
            sceneryManager.Show();
        }

        private void SceneryManager_FormClosed(object sender, FormClosedEventArgs e) {
            ReInitSceneriesPanel();
        }
        #endregion
        #region Context menu
        #region Settings
        private void Settings_Click(object sender, EventArgs e) {
            int devAddr = (int)((ToolStripMenuItem)sender).Tag;
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 16, MtrfMode: NooCtr.SendByAdr);
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 17, MtrfMode: NooCtr.SendByAdr);
            Mtrf64.SendCmd(0, 2, NooCmd.ReadState, devAddr, fmt: 18, MtrfMode: NooCtr.SendByAdr);
            if (settings != null) { settings.Close(); }
            settings = new SettingsForm(Mtrf64, DevBase, devAddr);
            settings.Show();
        }
        #endregion
        #region Show info
        private void ShowInfo_Click(object sender, EventArgs e) {
            int hash = sender.GetHashCode();
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            RfDevice rf = DevBase.Data[(int)menuItem.Tag];
            string res = String.Format("Device type: {0} \n" +
                "Firmware version: {1}", rf.ExtDevType, rf.FirmwareVer);
            MessageBox.Show(res);
        }
        #endregion
        #region Remove device
        private void RemoveDevice_Click(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            int devKey = (int)menuItem.Tag;
            //find rooms that contains device
            var roomsToRemove = DevBase.Data.Where(dev => dev.Key == devKey).Select(dev => dev.Value.Room);

            RfDevice devToRemove = DevBase.Data[devKey];
            switch (devToRemove.Type) {
                case NooDevType.PowerUnit:
                    DialogResult step11 = MessageBox.Show("Delete device?", "Warning!", MessageBoxButtons.YesNo);
                    if (step11 == DialogResult.Yes) {
                        MessageBox.Show("After you click OK, you'de have about 15 sec. " +
                            "to confirm unbind by pressing service button at power unit");
                        Mtrf64.SendCmd(devToRemove.Channel, NooMode.Tx, NooCmd.Unbind);
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
                        Mtrf64.SendCmd(0, NooMode.FTx, NooCmd.Service, addrF: devToRemove.Addr, d0: 1, MtrfMode: NooCtr.SendByAdr);
                        Mtrf64.SendCmd(0, NooMode.FTx, NooCmd.Unbind, addrF: devToRemove.Addr, MtrfMode: NooCtr.SendByAdr);
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
                        Mtrf64.Unbind(devToRemove.Channel, NooMode.Rx);
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
                        Mtrf64.Unbind(devToRemove.Channel, NooMode.Rx);
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
                LoopedDevKey = (int)((Control)sender).Tag;
            } else {
                tmi.Checked = false;
                t2.Stop();
            }
        }
        #endregion
        #region Move to
        //on mouse hover this function creates room list for mooving
        private void MoveTo_MouseHover(object sender, EventArgs e) {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            int DevKey = (int)menuItem.Tag;

            List<string> curRoom = new List<string> {
                Rooms[SelectedTab]
            };
            menuItem.DropDownItems.Clear();
            int dropDownItemIdx = 0;
            foreach (string item in Rooms.Except(curRoom)) {
                menuItem.DropDownItems.Add(item);
                menuItem.DropDownItems[dropDownItemIdx].Click += MoveToNewRoom_Click;
                menuItem.DropDownItems[dropDownItemIdx].Tag = Rooms.IndexOf(item);
                dropDownItemIdx++;
            }

        }

        private void MoveToNewRoom_Click(object sender, EventArgs e) {
            ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;
            int hash = sender.GetHashCode();
            int devKey = (int)((ToolStripDropDownItem)sender).Tag;

            RemoveControl(devKey, Rooms[SelectedTab]);
            AddControl(devKey, toolStripDropDownItem.Text);
            DevBase.Data[devKey].Room = toolStripDropDownItem.Text;
            toolStripDropDownItem.Dispose();
        }
        #endregion
        #region Redirect to
        private void RedirectTo_MouseHover(object sender, EventArgs e) {
            ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)sender;
            int devKey = (int)toolStripDropDownItem.Tag;
            RfDevice dev = DevBase.Data[devKey];
            var redirectListeners = DevBase.Data.Where((x) =>
               x.Value.Type == NooDevType.PowerUnit || x.Value.Type == NooDevType.PowerUnitF
            );
            int num = 0;
            toolStripDropDownItem.DropDownItems.Clear();
            foreach (var item in redirectListeners) {
                toolStripDropDownItem.DropDownItems.Add(item.Value.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)toolStripDropDownItem.DropDownItems[num];
                tsmi.Tag = item.Key;
                tsmi.Name = item.Value.Name;
                tsmi.CheckOnClick = true;
                if (dev.Redirect.Contains(item.Key)) {
                    tsmi.Checked = true;
                }
                tsmi.CheckedChanged += RedirectToSelected_Click;
                num++;
            }
        }

        private void RedirectToSelected_Click(object sender, EventArgs e) {
            ToolStripMenuItem toolStripDropDownItem = (ToolStripMenuItem)sender;
            var findDevByName = DevBase.Data.Where(dev => dev.Value.Name == toolStripDropDownItem.Name);
            RfDevice redirectSource = DevBase.Data[(int)toolStripDropDownItem.OwnerItem.Tag];
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



        void AddControl(int devKeyToAdd, string roomToAdd) {
            int roomIdx = Rooms.IndexOf(roomToAdd);
            if (!DevBase.Data[devKeyToAdd].Views.ContainsKey(roomToAdd)) {
                DevBase.Data[devKeyToAdd].Views.Add(roomToAdd, new DevView(DevBase.Data[devKeyToAdd]));
            }
            RoomSelector.TabPages[roomIdx].Controls[0].Controls.Add(DevBase.Data[devKeyToAdd].Views[roomToAdd]);
        }

        void RemoveControl(int devKey, string roomToRemove) {
            if (roomToRemove != null) { //delete from room
                int roomIdx = Rooms.IndexOf(roomToRemove);
                DevView toRem = DevBase.Data[devKey].Views[roomToRemove];
                RoomSelector.TabPages[roomIdx].Controls[0].Controls.Remove(toRem);
                DevBase.Data[devKey].Views.Remove(roomToRemove);
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
                            AutoSize = DevicesPanel.AutoSize,
                            AutoSizeMode = DevicesPanel.AutoSizeMode,
                            BackColor = DevicesPanel.BackColor,
                            MinimumSize = DevicesPanel.MinimumSize,
                            MaximumSize = DevicesPanel.MaximumSize,
                            Location = DevicesPanel.Location
                        });
                    }
                }
            }
        }

        private void ShowScenBtn_Click(object sender, EventArgs e) {
            if (ScenariesPanel.Visible) {
                ScenariesPanel.Visible = false;
            } else {
                ScenariesPanel.Visible = true;
            }
        }
    }
    public static class SensorsTypes {
        public const int PT112 = 1;
        public const int PT111 = 2;
        public const int PM111 = 3;
        public const int PM112 = 5;
    }
}
