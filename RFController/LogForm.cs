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
    public partial class LogForm : Form {
        Action<TextBox, string> ControlUpdater;
        MTRF dev1;
        public LogForm(MTRF dev) {
            dev1 = dev;
            InitializeComponent();
            dev.DataSent += Dev_DataSent;
            dev.NewDataReceived += Dev_NewDataReceived;
            ControlUpdater = new Action<TextBox, string>(UpdateTempBox);
            this.FormClosing += Form2_CloseHandler;
        }
        private void Form2_CloseHandler(object sender, EventArgs e) {
            dev1.NewDataReceived -= Dev_NewDataReceived;
            dev1.DataSent -= Dev_DataSent;
        }
        private void Dev_NewDataReceived(object sender, EventArgs e) {
            RxBox.BeginInvoke(ControlUpdater, RxBox,dev1.GetLogMsg(dev1.rxBuf));
        }

        private void UpdateTempBox(TextBox tb, string strToShow) {
            tb.AppendText(strToShow);
        }

        private void Dev_DataSent(object sender, EventArgs e) {
            TxBox.BeginInvoke(ControlUpdater, TxBox, dev1.GetLogMsg(dev1.txBuf));
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e) {
            TxBox.Text = "";
        }

        private void clearRxToolStripMenuItem_Click(object sender, EventArgs e) {
            RxBox.Text = "";
        }
    }
}
