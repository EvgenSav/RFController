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
    public partial class SceneryManager : Form {
        MyDB<int, RfDevice> DevBase;
        DropDownTreeView SceneriesView;
        SortedDictionary<int, RfDevice> PowerUnits = new SortedDictionary<int, RfDevice>();
        public SceneryManager(MyDB<int, RfDevice> devices) {
            InitializeComponent();
            DevBase = devices;
            var findPowUnits = DevBase.Data.Where((dev) => {
                return (dev.Value.Type == NooDevType.PowerUnit ||
                dev.Value.Type == NooDevType.PowerUnitF);
            });
            foreach (var item in findPowUnits) {
                PowerUnits.Add(item.Key, item.Value);
            }
            SceneriesView = new DropDownTreeView {
                Location = new Point(20, 100),
                Size = new Size(400, 400),
                Font = new Font("Microsoft Sans Serif", 12)
            };
            SceneriesView.AfterCollapse += SceneriesView_AfterCollapse;
            SceneriesView.AfterExpand += SceneriesView_AfterExpand;

            SceneriesView.AfterCheck += SceneriesView_AfterCheck;
            SceneriesView.CheckBoxes = true;
            Controls.Add(SceneriesView);
        }

        private void SceneriesView_AfterExpand(object sender, TreeViewEventArgs e) {
            DropDownTreeView container = (DropDownTreeView)sender;
            DropDownTreeNode node = (DropDownTreeNode)e.Node;
            container.ShowExtBoxes(node);
        }

        private void SceneriesView_AfterCheck(object sender, TreeViewEventArgs e) {
            DropDownTreeView container = (DropDownTreeView)sender;
            DropDownTreeNode curNode = (DropDownTreeNode)e.Node;
            // Set the bounds of the ComboBox, with
            // a little adjustment to make it look right
            //curNode.StateBox.SetBounds(
            //    curNode.Bounds.X + 80,
            //    curNode.Bounds.Y,
            //    curNode.Bounds.Width,
            //    curNode.Bounds.Height);

            //curNode.BrightBox.SetBounds(
            //    curNode.Bounds.X + 100,
            //    curNode.Bounds.Y,
            //    width: 55,
            //    curNode.Bounds.Height);
            //curNode.BrightBox.Font = new Font("Microsoft Sans Serif", 8);

            //// Need to add the node's NumUpDown and CheckBox to the
            //// TreeView's list of controls for it to work
            //container.Controls.Add(curNode.BrightBox);
            //container.Controls.Add(curNode.StateBox);
            ////curNode.BrightBox.Show();
            ////curNode.StateBox.Show();

            //if (curNode.IsVisible) {
            //    container.HideExtBoxes(curNode);
            //} else {
            //    container.ShowExtBoxes(curNode);
            //}
        }

        private void SceneriesView_AfterCollapse(object sender, TreeViewEventArgs e) {
            DropDownTreeView container = (DropDownTreeView)sender;
            DropDownTreeNode node = (DropDownTreeNode)e.Node;
            container.HideExtBoxes(node);
        }
        int scenNum = 0;

        private void AddBtn_Click(object sender, EventArgs e) {
            if (NameBox.TextLength > 3 && NameBox.TextLength < 32) {
                SceneriesView.Nodes.Add(new DropDownTreeNode(NameBox.Text));
                int nodeNum = 0;
                foreach (var item in PowerUnits) {
                    SceneriesView.Nodes[scenNum].Nodes.Add(new DropDownTreeNode(item.Value.Name));
                    SceneriesView.AddExtBoxes(scenNum);
                    nodeNum++;
                }
                scenNum++;
            }
        }

    }
    public class DropDownTreeNode : TreeNode {
        public DropDownTreeNode(string name) : base(name) {
            BrightBox = new NumericUpDown();
            StateBox = new CheckBox();
        }
        // *snip* Constructors go here
        public NumericUpDown BrightBox { get; set; }
        public CheckBox StateBox { get; set; }
    }

    public class DropDownTreeView : TreeView {
        public DropDownTreeView() : base() {
        }

        public void AddExtBoxes(int nodePos) {
            int cnt = Nodes[nodePos].Nodes.Count;
            Nodes[nodePos].ExpandAll();
            if (cnt > 0) {
                DropDownTreeNode curNode = (DropDownTreeNode)Nodes[nodePos].Nodes[cnt - 1];
                Rectangle pos = curNode.Bounds;
                curNode.StateBox.Bounds = new Rectangle(pos.X + 180, pos.Y, pos.Width, pos.Height);
                pos = curNode.Bounds;
                curNode.BrightBox.Bounds = new Rectangle(pos.X + 200, pos.Y, 55, pos.Height);
                curNode.BrightBox.Font = new Font("Microsoft Sans Serif", 8);
                this.Controls.Add(curNode.BrightBox);
                this.Controls.Add(curNode.StateBox);
            }
        }
 
        //protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e) {
        //    // Are we dealing with a dropdown node?
        //    if (e.Node is DropDownTreeNode) {
        //        m_CurrentNode = (DropDownTreeNode)e.Node;
        //        if (m_CurrentNode.Level != 0) {
        //            // Need to add the node's NumUpDown and CheckBox to the
        //            // TreeView's list of controls for it to work
        //            Controls.Add(m_CurrentNode.BrightBox);
        //            Controls.Add(m_CurrentNode.StateBox);

        //            // Set the bounds of the ComboBox, with
        //            // a little adjustment to make it look right
        //            this.m_CurrentNode.StateBox.SetBounds(
        //                this.m_CurrentNode.Bounds.X + 80,
        //                this.m_CurrentNode.Bounds.Y,
        //                this.m_CurrentNode.Bounds.Width,
        //                this.m_CurrentNode.Bounds.Height);

        //            this.m_CurrentNode.BrightBox.SetBounds(
        //                this.m_CurrentNode.Bounds.X + 100,
        //                this.m_CurrentNode.Bounds.Y,
        //                width: 55,
        //                this.m_CurrentNode.Bounds.Height);
        //            m_CurrentNode.BrightBox.Font = new Font("Microsoft Sans Serif", 8);


        //            //// Listen to the SelectedValueChanged
        //            //// event of the node's ComboBox
        //            //this.m_CurrentNode.ComboBox.SelectedValueChanged +=
        //            //   new EventHandler(ComboBox_SelectedValueChanged);
        //            //this.m_CurrentNode.ComboBox.DropDownClosed +=
        //            //   new EventHandler(ComboBox_DropDownClosed);

        //            // Now show the ComboBox
        //            this.m_CurrentNode.BrightBox.Show();
        //            this.m_CurrentNode.StateBox.Show();
        //        }
        //    }
        //    base.OnNodeMouseClick(e);
        //}

        //void ComboBox_SelectedValueChanged(object sender, EventArgs e) {
        //    HideComboBox();
        //}

        //void ComboBox_DropDownClosed(object sender, EventArgs e) {
        //    HideComboBox();
        //}


        public void HideExtBoxes(DropDownTreeNode curNode) {
            // Hide the ComboBox
            foreach (DropDownTreeNode item in curNode.Nodes) {
                item.BrightBox.Hide();
                item.StateBox.Hide();
            }
        }
        public void ShowExtBoxes(DropDownTreeNode curNode) {
            // Hide the ComboBox
            foreach (DropDownTreeNode item in curNode.Nodes) {
                item.BrightBox.Show();
                item.StateBox.Show();
            }
        }
    }


    class SceneryItem {
        RfDevice DevInScenery;
        int Bright;
        int State;
        public SceneryItem(RfDevice rfDev, int state, int bright) {
            DevInScenery = rfDev;
            State = state;
            Bright = bright;
        }
    }
}
