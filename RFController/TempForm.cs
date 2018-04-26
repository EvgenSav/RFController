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
    public partial class TempForm : Form {
        MTRF dev1;
        MyDB<int, TempAtChannel> TemperatureLog;
        Action<TextBox, string> ControlUpdater;
        Control.ControlCollection c1;
        GraphForm trend1;
        Dictionary<int, TextBox> controls = new Dictionary<int, TextBox>();
        public TempForm(MTRF dev, MyDB<int, TempAtChannel> tempLog) {
            InitializeComponent();
            dev1 = dev;
            TemperatureLog = tempLog;
            ControlUpdater = new Action<TextBox, string>(UpdateTempBox);
            dev1.NewDataReceived += Dev_NewDataReceived;
            this.FormClosing += Form3_CloseHandler;
            c1 = flowLayoutPanel1.Controls;

            //set control's properties
            foreach (TextBox item in c1) {
                item.Click += Item_Click;
                item.Enabled = false;
                controls.Add(item.GetHashCode(), item);
            }

            //update cotrols
            foreach (var item in TemperatureLog.Data) {
                float temp = item.Value[item.Value.Count - 1].Value;
                string st1 = String.Format("Ch:{0}   {1:#.##} {2}C", item.Key, temp, (char)176);
                c1[item.Key].Text = st1;
                c1[item.Key].Enabled = true;
                c1[item.Key].BackColor = Color.LightBlue;
            }
        }

        private void Item_Click(object sender, EventArgs e) {
            TextBox tb = controls[sender.GetHashCode()];
            if (tb.Enabled) {
                if (trend1 != null) {
                    trend1.Close();                    
                }
                List<TempAtChannel> chnData = TemperatureLog.Data[tb.TabIndex];
                trend1 = new GraphForm(dev1, chnData);
                trend1.Show();
            }
        }

        private void Form3_CloseHandler(object sender, EventArgs e) {
            dev1.NewDataReceived -= Dev_NewDataReceived;
            if(trend1 != null) {
                trend1.Close();
            }
        }

        private void Dev_NewDataReceived(object sender, EventArgs e) {
            foreach (var item in TemperatureLog.Data) {
                float temp = item.Value[item.Value.Count - 1].Value;
                string st1 = String.Format("Ch:{0}   {1:#.##} {2}C", item.Key, temp, (char)176);
                c1[item.Key].BeginInvoke(ControlUpdater, c1[item.Key], st1);
            }
        }

        private void UpdateTempBox(TextBox tb, string strToShow) {
            tb.Text = strToShow;
            tb.Enabled = true;
            tb.BackColor = Color.LightGreen;
        }
    }
}
