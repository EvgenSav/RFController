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
    public partial class ChooseModule : Form {
        MTRF Mtrf64;
        public ChooseModule(MTRF dev,List<Mtrf> ports) {
            InitializeComponent();
            Mtrf64 = dev;
            listBox1.DataSource = ports;
            listBox1.ValueMember = "ComPortName";
            listBox1.DisplayMember = "Info";
        }

        private void OkBtn_Click(object sender, EventArgs e) {
            Mtrf64.OpenPort(listBox1.SelectedValue.ToString());
            this.Close();
        }
    }
}
