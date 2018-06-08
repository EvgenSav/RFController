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
                ChannelSel.Items.Add(i);
            }

            Type modeType = typeof(NooMode);
            System.Reflection.FieldInfo[] modeTypeFieldsInfo = modeType.GetFields();
            foreach (System.Reflection.FieldInfo modeField in modeTypeFieldsInfo) {
                ModeSel.Items.Add(new { Name = modeField.Name, Value = modeField.GetValue(modeField) });
            }
            ModeSel.DataSource = ModeSel.Items;
            ModeSel.DisplayMember = "Name";
            ModeSel.ValueMember = "Value";

            Type cmdType = typeof(NooCmd);
            System.Reflection.FieldInfo[] cmdTypeFieldsInfo = cmdType.GetFields();
            foreach (System.Reflection.FieldInfo cmdField in cmdTypeFieldsInfo) {
                CmdSel.Items.Add(new { cmdField.Name, Value = cmdField.GetValue(cmdField) });
            }
            CmdSel.DataSource = CmdSel.Items;
            CmdSel.DisplayMember = "Name";
            CmdSel.ValueMember = "Value";

            Type ctrType = typeof(NooCtr);
            System.Reflection.FieldInfo[] ctrTypeFieldsInfo = ctrType.GetFields();
            foreach (System.Reflection.FieldInfo ctrField in ctrTypeFieldsInfo) {
                CtrSel.Items.Add(new { Name = ctrField.Name, Value = ctrField.GetValue(ctrField) });
            }
            CtrSel.DisplayMember = "Name";
            CtrSel.ValueMember = "Value";
            CtrSel.DataSource = CtrSel.Items;
        }

        private void SendBtn_Click(object sender, EventArgs e) {
            if (ChannelSel.SelectedIndex != -1 && ModeSel.SelectedIndex != -1 && CmdSel.SelectedIndex != -1) {
                Mtrf64.SendCmd(ChannelSel.SelectedIndex, (int)ModeSel.SelectedValue, (int)CmdSel.SelectedValue,
                    (int)Adr.Value, fmt: (int)Format.Value, (int)D0.Value, (int)D1.Value, (int)D2.Value, (int)D3.Value, 
                    MtrfMode: (int)CtrSel.SelectedValue);
            }
        }
    }

}
