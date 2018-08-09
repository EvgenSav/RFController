using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFController {
    public partial class DevView : UserControl {
        RfDevice Device;
        public EventHandler DevClicked;
        public EventHandler ShowActionLogClicked;

        public EventHandler Remove_mi_Click;
        public EventHandler Info_mi_Click;
        public EventHandler RedirectTo_mi_Click;
        public EventHandler MoveToRoom_mi_Click;
        public EventHandler Settings_mi_Click;
        public EventHandler<MouseEventArgs> BrightChanged;

        Action UpdView;
        public DevView(RfDevice dev) : base() {
            InitializeComponent();
            Device = dev;
            UpdView = new Action(UpdtView);

            if (Device != null) {
                if (Device.Type == NooDevType.Sensor) {
                    if (Device.ExtDevType == SensorsTypes.PT112) {
                        TypePict.Image = Image.FromFile(@"Icons\icons8-temp-25.png");
                    }
                    if (Device.ExtDevType == SensorsTypes.PM112) {
                        TypePict.Image = Image.FromFile(@"Icons\icons8-move-25.png");
                    }
                    foreach (Control box in Controls) {
                        foreach (Control item in box.Controls) {
                            item.Click += Temp_Click;
                        }
                        box.Click += Temp_Click;
                    }
                }
                if (Device.Type == NooDevType.RemController) {
                    TypePict.Image = Image.FromFile(@"Icons\icons8-rc-25.png");
                }

                if (Device.Type == NooDevType.PowerUnit || Device.Type == NooDevType.PowerUnitF) {
                    TypePict.Image = Image.FromFile(@"Icons\icons8-pu_2-25.png");
                    StateLbl.MouseWheel += StateLbl_MouseWheel;
                    foreach (Control box in Controls) {
                        foreach (Control item in box.Controls) {
                            item.Click += DevBox_Click;
                        }
                        box.Click += DevBox_Click;
                    }
                }

                foreach (Control box in Controls) {
                    box.Tag = Device.Key;
                    foreach (Control item in box.Controls) {
                        item.Tag = Device.Key;
                    }
                }
                SetupContextMenu();
                UpdateView();
            }
        }

        private void Temp_Click(object sender, EventArgs e) {
            if (ShowActionLogClicked != null) {
                ShowActionLogClicked(sender, e);
            }
        }

        private void StateLbl_MouseWheel(object sender, MouseEventArgs e) {
            if (BrightChanged != null) {
                BrightChanged(sender, e);
            }
        }

        private void SetupContextMenu() {
            switch (Device.Type) {
                case NooDevType.PowerUnit:
                    contextMenuStrip1.Items.RemoveByKey("RedirectTo_mi");
                    break;
                case NooDevType.PowerUnitF:
                    contextMenuStrip1.Items.RemoveByKey("RedirectTo_mi");
                    break;
                default:
                    contextMenuStrip1.Items.RemoveByKey("Settings_mi");
                    contextMenuStrip1.Items.RemoveByKey("SwitchLoop_mi");
                    break;
            }
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items) {
                item.Tag = Device.Key;
                switch (item.Name) {
                    case "Info_mi":
                        item.Click += Info_ToolStripMenuItem_Click;
                        break;
                    case "Remove_mi":
                        item.Click += Remove_ToolStripMenuItem_Click;
                        break;
                    case "Settings_mi":
                        item.Click += Settings_ToolStripMenuItem_Click;
                        break;
                    case "SwitchLoop_mi":
                        break;
                    case "MoveTo_mi":
                        item.MouseHover += MoveTo_ToolStripMenuItem_MouseHover;
                        break;
                    case "RedirectTo_mi":
                        item.MouseHover += RedirectTo_ToolStripMenuItem_MouseHover;
                        break;
                    case "Log_mi":
                        item.Click += Temp_Click;
                        break;
                }
            }
        }

        private void DevBox_Click(object sender, EventArgs e) {
            if (DevClicked != null) {
                DevClicked(sender, e);
            }
        }

        public void UpdtView() {
            DevBox.Name = Device.Name;
            DevBox.Text = Device.Name;
            TypeNameLbl.Text = Device.GetDevTypeName();
            if (Device.Type == NooDevType.PowerUnit || Device.Type == NooDevType.PowerUnitF) {
                if (Device.State != 0) {
                    StateLbl.Text = Device.Bright.ToString() + " %";
                } else {
                    StateLbl.Text = "0 %";
                }
            }
            if (Device.Type == NooDevType.Sensor) {
                int cnt = 0;
                switch (Device.ExtDevType) {
                    case SensorsTypes.PT112:
                            cnt = Device.Log.Count;
                            if (cnt > 0) {
                                StateLbl.Text = Device.Log[cnt - 1].ToString();
                            } else {
                                StateLbl.Text = "No data";
                            }
                        break;
                    case SensorsTypes.PM112:
                        cnt = Device.Log.Count;
                        if (cnt > 0) {
                            StateLbl.Text = Device.Log[cnt - 1].CurrentTime.ToShortTimeString();
                        } else {
                            StateLbl.Text = "No data";
                        }
                        break;
                }
            }
            if (Device.Type == NooDevType.RemController) {
                int cnt = Device.Log.Count;
                if (cnt > 0) {
                    StateLbl.Text = Device.Log[cnt - 1].CurrentTime.ToShortTimeString();
                } else {
                    StateLbl.Text = "No data";
                }
            }
        }
        public void UpdateView() {
            if (InvokeRequired) {
                BeginInvoke(UpdView);
            } else {
                UpdView();
            }
        }

        private void Info_ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Info_mi_Click != null) {
                Info_mi_Click(sender, e);
            }
        }

        private void Remove_ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Remove_mi_Click != null) {
                Remove_mi_Click(sender, e);
            }
        }

        private void MoveTo_ToolStripMenuItem_MouseHover(object sender, EventArgs e) {
            if (MoveToRoom_mi_Click != null) {
                MoveToRoom_mi_Click(sender, e);
            }
        }

        private void Settings_ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Settings_mi_Click != null) {
                Settings_mi_Click(sender, e);
            }
        }

        private void RedirectTo_ToolStripMenuItem_MouseHover(object sender, EventArgs e) {
            if (RedirectTo_mi_Click != null) {
                RedirectTo_mi_Click(sender, e);
            }
        }
    }
}
