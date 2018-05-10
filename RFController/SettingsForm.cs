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

namespace RFController {
    public partial class SettingsForm : Form {
        MyDB<int,RfDevice> DeviceList;
        RfDevice RfDevice;
        MTRF Mtrf64;
        int DevKey;
        public SettingsForm(MTRF dev, MyDB<int,RfDevice> data, int key) {
            InitializeComponent();
            Mtrf64 = dev;
            DeviceList = data;
            RfDevice = DeviceList.Data[key];
            DevKey = key;
            groupBox1.Text = RfDevice.Name;

            UpdateForm();
            SaveState.CheckedChanged += SaveState_CheckedChanged;
            Mtrf64.NewDataReceived += Dev1_NewDataReceived;
            FormClosed += SettingsForm_FormClosed;
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e) {
            Mtrf64.NewDataReceived -= Dev1_NewDataReceived;
        }

        void UpdateForm() {
            RfDevice = DeviceList.Data[DevKey];
            switch(Mtrf64.rxBuf.Fmt) {
                case 16:
                    SaveState.Checked = ((RfDevice.Settings & 0x01) != 0);
                    Dimmer.Checked = ((RfDevice.Settings & 0x02) != 0);
                    DefaultOn.Checked = ((RfDevice.Settings & 0x20) != 0);
                    break;
                case 17:
                    float dimLvlHi = ((float)RfDevice.DimCorrLvlHi / 255) * 100;
                    float dimLvlLow = ((float)RfDevice.DimCorrLvlLow / 255) * 100;
                    DimLvlHi.Text = Round(dimLvlHi).ToString();
                    DimLvlLow.Text = Round(dimLvlLow).ToString();
                    break;
                case 18:
                    float onLvl = ((float)RfDevice.OnLvl / 255) * 100;
                    OnLvl.Text = Round(onLvl).ToString();
                    break;
            }   
        }

        int Round(float val) {
            if ((val - (int)val) > 0.5) {
                return (int)val + 1;
            } else {
                return (int)val;
            }            
        }

        private void Dev1_NewDataReceived(object sender, EventArgs e) {
            BeginInvoke(new Action(UpdateForm));
        }

        private void SaveState_CheckedChanged(object sender, EventArgs e) {
           
        }

        private void ApplySettingsBtn_Click(object sender, EventArgs e) {
            int settings = 0;
            if(SaveState.Checked)   settings |=  0x01;
            if (Dimmer.Checked) {
                settings |= 0x02;              
            } else {
                settings &= ~0x02;     
            }
            if(DefaultOn.Checked)   settings |= 0x20;

            int DimCorrHi = (int)((DimLvlHi.Value / 100) * 255);
            int DimCorrLow = (int)((DimLvlLow.Value / 100) * 255);
            int OnBrightLvl = (int)((OnLvl.Value / 100) * 255);

            Mtrf64.SendCmd(0, 2, NooCmd.WriteState, RfDevice.Addr, 16, settings, settings >> 8, 255, 255);
            Mtrf64.SendCmd(0, 2, NooCmd.WriteState, RfDevice.Addr, fmt: 17, d0: DimCorrHi, d1: DimCorrLow,255,255);
            Mtrf64.SendCmd(0, 2, NooCmd.WriteState, RfDevice.Addr, fmt: 18, d0: OnBrightLvl,0,255,255);
        }

        private void Dimmer_CheckedChanged(object sender, EventArgs e) {
            if (Dimmer.Checked) {
                groupBox2.Enabled = true;                
            } else {
                groupBox2.Enabled = false;
            }
        }

    }
}
