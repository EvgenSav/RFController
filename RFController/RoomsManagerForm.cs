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
    public partial class RoomsManagerForm : Form {
        List<string> Rooms;
        public RoomsManagerForm(List<string> rooms) {
            InitializeComponent();
            Rooms = rooms;
            foreach (var item in Rooms) {
                checkedListBox1.Items.Add(item);
            }
        }

        private void AddRoomBtn_Click(object sender, EventArgs e) {
            Rooms.Add(textBox1.Text);
            textBox1.Text = "";
            UpdateForm();
        }

        public void UpdateForm() {
            checkedListBox1.Items.Clear();
            foreach (var item in Rooms) {
                checkedListBox1.Items.Add(item);
            }
        }

        private void RemoveRoomBtn_Click(object sender, EventArgs e) {
            if (checkedListBox1.SelectedIndex != -1) {
                List<int> toRemove = new List<int>();
                //create remove items index list
                foreach (var item in checkedListBox1.CheckedIndices) {
                    toRemove.Add((int)item);
                }
                //reverse list to delete from end
                toRemove.Reverse();
                //delete selected
                foreach (var item in toRemove) {
                    Rooms.RemoveAt(item);
                }
            }
            UpdateForm();
        }
    }
}
