using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace RFController {
    public partial class ServiceForm : Form {
        MTRF Mtrf64;

        public ServiceForm(MTRF dev) {
            InitializeComponent();
            Mtrf64 = dev;
            for (int i = 0; i < 64; i++) {
                channelSel.Items.Add(i);
            }

            portSel.DataSource = Mtrf64.GetAvailableComPorts();
            //modeSel.DataSource = Mtrf64.modes;
            modeSel.DisplayMember = "Name";
            modeSel.ValueMember = "Value"; 
        }


        private void BtnConnect_Click(object sender, EventArgs e) {
            //if (portSel.SelectedIndex != -1) {
            //    string selectedPortName = portSel.Items[portSel.SelectedIndex].ToString();
            //    switch (Mtrf64.ConnectToPort(selectedPortName)) {
            //        case 0:
            //            toolStripStatusLabel1.Text = String.Format("Not connected");
            //            btnConnect.Text = "Connect";
            //            break;
            //        case 1:
            //            toolStripStatusLabel1.Text = String.Format("Connected to: {0}", selectedPortName);
            //            btnConnect.Text = "Disconnect";
            //            break;
            //    }

            //}
        }

        private void BtnBind_Click(object sender, EventArgs e) {
            int selectedChIndex = channelSel.SelectedIndex;
            if (channelSel.SelectedIndex != -1 && modeSel.SelectedIndex != -1) {
                Mtrf64.BindOn((int)channelSel.SelectedItem, (int)modeSel.SelectedValue);
            }
        }

        private void BtnUnbind_KeyDown(object sender, KeyEventArgs e) {
            if (!e.Shift) {
                int selectedChIndex = channelSel.SelectedIndex;
                if (selectedChIndex != -1) {
                    Mtrf64.Unbind((int)channelSel.SelectedItem, (int)modeSel.SelectedValue);
                }
            } else {
                Mtrf64.Unbind(0, (int)modeSel.SelectedValue, unbindAll: true);
            }
        }

        private void portSel_Click(object sender, EventArgs e) {
            portSel.DataSource = Mtrf64.GetAvailableComPorts();
        }
    }

}
