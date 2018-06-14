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
        //MyTreeView SceneriesEditView;
        SortedDictionary<int, RfDevice> PowerUnits = new SortedDictionary<int, RfDevice>();
        List<Scenery> Sceneries;
        int SceneriesNum = 0;

        public SceneryManager(MyDB<int, RfDevice> devices, List<Scenery> sceneries) {
            InitializeComponent();
            DevBase = devices;

            var findPowUnits = DevBase.Data.Where((dev) => {
                return (dev.Value.Type == NooDevType.PowerUnit ||
                dev.Value.Type == NooDevType.PowerUnitF);
            });
            foreach (var item in findPowUnits) {
                PowerUnits.Add(item.Key, item.Value);
            }

            Sceneries = sceneries;

            SceneriesEditViewInit();

            //SceneriesView.AfterCollapse += SceneriesView_AfterCollapse;
            //SceneriesView.AfterExpand += SceneriesView_AfterExpand;

            SceneriesEditView.AfterCheck += SceneriesView_AfterCheck;
            SceneriesEditView.CheckBoxes = true;
            SceneriesEditView.DrawMode = TreeViewDrawMode.OwnerDrawText; //needs for OnNodeDraw working
            AddBtn.Enabled = false;
        }

        public void SceneriesEditViewInit() {
            foreach (Scenery curScenery in Sceneries) {
                SceneriesEditView.Nodes.Add(new MyTreeNode(curScenery.Name));
                int nodeIdx = Sceneries.IndexOf(curScenery);
                TreeNodeCollection curScenDevs = SceneriesEditView.Nodes[nodeIdx].Nodes;
                foreach (var device in PowerUnits) {
                    if (curScenery.SceneryData.ContainsKey(device.Key)) { //device is a part of current scenery
                        SceneryItem curDevAtScenery = curScenery.SceneryData[device.Key];
                        //generate dev. state node at scenery
                        curScenDevs.Add(new MyTreeNode(device.Value.Name, key: device.Key, state: curDevAtScenery.State, bright: curDevAtScenery.Bright) {
                            Checked = true
                        });
                    } else {
                        curScenDevs.Add(new MyTreeNode(device.Value.Name, key: device.Key));
                    }
                }
                SceneriesNum++;
            }
        }

        private void SceneriesView_AfterExpand(object sender, TreeViewEventArgs e) {
            MyTreeView container = (MyTreeView)sender;
            MyTreeNode node = (MyTreeNode)e.Node;
            //container.ShowExtBoxes(node);
            //MessageBox.Show(container.Controls.Count.ToString());
        }

        private void SceneriesView_AfterCheck(object sender, TreeViewEventArgs e) {
            MyTreeView container = (MyTreeView)sender;
            MyTreeNode curNode = (MyTreeNode)e.Node;
            if (curNode.Level > 0) {
                curNode.Parent.Checked = true;
                curNode.BackColor = Color.LightGreen;
            }
        }

        private void SceneriesView_AfterCollapse(object sender, TreeViewEventArgs e) {
            MyTreeView container = (MyTreeView)sender;
            MyTreeNode node = (MyTreeNode)e.Node;
            //container.HideExtBoxes(node);
        }


        private void AddBtn_Click(object sender, EventArgs e) {
            if (NameBox.TextLength > 3 && NameBox.TextLength < 32) {
                SceneriesEditView.Nodes.Add(new MyTreeNode(NameBox.Text));
                Sceneries.Add(new Scenery(NameBox.Text));
                int nodeNum = 0;
                foreach (var item in PowerUnits) {
                    SceneriesEditView.Nodes[SceneriesNum].Nodes.Add(new MyTreeNode(item.Value.Name, key: item.Key));
                    nodeNum++;
                }
                SceneriesNum++;

            }
        }

        private void RemoveBtn_Click(object sender, EventArgs e) {
            List<MyTreeNode> toRemove = new List<MyTreeNode>(64);
            foreach (MyTreeNode item in SceneriesEditView.Nodes) {
                if (item.Checked) {
                    toRemove.Add(item);
                }
            }
            foreach (var item in toRemove) {
                Sceneries.RemoveAt(item.Index);
                SceneriesEditView.Nodes.Remove(item);
                SceneriesNum--;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e) {
            List<MyTreeNode> toSave = new List<MyTreeNode>(64);
            foreach (MyTreeNode scenNameNode in SceneriesEditView.Nodes) {
                if (scenNameNode.Checked) {
                    Scenery toAdd = new Scenery(scenNameNode.Name);
                    Sceneries[scenNameNode.Index] = toAdd;
                    foreach (MyTreeNode devNode in scenNameNode.Nodes) {
                        if (devNode.Checked) {
                            int bright = (int)devNode.BrightBox.Value;
                            int state = (devNode.StateBox.Checked) ? 1 : 0;
                            toAdd.SceneryData.Add(devNode.DevKey, new SceneryItem(devNode.DevKey, state, bright));
                        }
                    }
                }
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e) {
            TextBox name = (TextBox)sender;
            if((name.TextLength > 3 && name.TextLength < 32)) {
                AddBtn.Enabled = true;
                NameLabel.BackColor = Color.LightGreen;
            } else {
                AddBtn.Enabled = false;
                NameLabel.BackColor = Color.Empty;
            }
        }
    }

    public class MyTreeNode : TreeNode {
        public int DevKey { get; set; }
        public NumericUpDown BrightBox { get; set; }
        public CheckBox StateBox { get; set; }
        public PictureBox brIcon { get; set; }
        public MyTreeNode(string name, int key = 0, int bright = 0, int state = 0) : base(name) {
            brIcon = new PictureBox {
                Image = Image.FromFile("C:/Users/Evgen/Dropbox/VisualStudioProjects/Projects/RFController/RFController/bin/Debug/Icons/icons8-sun-25.png"),
                Visible = true
            };
            BrightBox = new NumericUpDown {
                Font = new Font(FontFamily.GenericSansSerif, 10),
                Text = "Bright",
                Value = bright
            };
            StateBox = new CheckBox {
                Checked = (state > 0) ? true : false,
                Text = (state > 0) ? "On" : "Off"
            };
            DevKey = key;
            Name = name;
        }
    }

    public class MyTreeView : TreeView {
        public MyTreeView() : base() {
        }

        protected override void OnAfterExpand(TreeViewEventArgs e) {
            MyTreeNode myNode = (MyTreeNode)e.Node;
            foreach (MyTreeNode item in myNode.Nodes) {
                item.BrightBox.Show();
                item.StateBox.Show();
                item.brIcon.Show();
            }
            base.OnAfterExpand(e);
        }
        protected override void OnAfterCollapse(TreeViewEventArgs e) {
            MyTreeNode myNode = (MyTreeNode)e.Node;
            foreach (MyTreeNode item in myNode.Nodes) {
                item.BrightBox.Hide();
                item.StateBox.Hide();
                item.brIcon.Hide();
            }
            base.OnAfterCollapse(e);
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e) {
            MyTreeNode myNode = (MyTreeNode)e.Node;
            if (myNode.Bounds != new Rectangle(0, 0, 0, 0)) {
                if (myNode.Level > 0) {
                    int nodeIdx = Nodes.IndexOf(e.Node.Parent);
                    //Nodes[nodeIdx].ExpandAll();
                    
                    //Set/reset position of extended boxes
                    Rectangle pos = myNode.Bounds;
                    myNode.StateBox.Bounds = new Rectangle(pos.X + 120, pos.Y, 50, pos.Height);
                    myNode.brIcon.Bounds = new Rectangle(pos.X + 170, pos.Y, 55, pos.Height);
                    myNode.BrightBox.Bounds = new Rectangle(pos.X + 200, pos.Y, 55, pos.Height);
                   
                    if (myNode.BackColor == Color.LightGreen) {
                        e.Graphics.FillRectangle(Brushes.LightGreen, myNode.Bounds);
                    }

                    if (!Controls.Contains(myNode.StateBox)) {
                        myNode.StateBox.CheckedChanged += StateBox_CheckedChanged;
                        Controls.Add(myNode.StateBox);
                    }
                    if (!Controls.Contains(myNode.BrightBox)) {
                        myNode.BrightBox.ValueChanged += BrightBox_ValueChanged;
                        Controls.Add(myNode.BrightBox);
                    }
                    if (!Controls.Contains(myNode.brIcon)) {
                        Controls.Add(myNode.brIcon);
                    }
                }
                Point nodeLocation = myNode.Bounds.Location;
                nodeLocation.Y += 3;
                e.Graphics.DrawString(e.Node.Text, this.Font, Brushes.Black, nodeLocation);
            }
            base.OnDrawNode(e);
        }

        private void StateBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox stateBox = (CheckBox)sender;
            if(stateBox.Checked) {
                stateBox.Text = "On";
            } else {
                stateBox.Text = "Off";
            }
            MyTreeView ctrl = (MyTreeView)stateBox.Parent;
            foreach (MyTreeNode scenNames in ctrl.Nodes) {
                foreach (MyTreeNode dev in scenNames.Nodes) {
                    if (dev.StateBox.Equals(stateBox)) {
                        dev.Checked = true;
                    }
                }
            }
        }

        private void BrightBox_ValueChanged(object sender, EventArgs e) {
            NumericUpDown brightBox = (NumericUpDown)sender;
            MyTreeView ctrl = (MyTreeView)brightBox.Parent;
            foreach (MyTreeNode scenNames in ctrl.Nodes) {
                foreach (MyTreeNode dev in scenNames.Nodes) {
                    if (dev.BrightBox.Equals(brightBox)) {
                        dev.Checked = true;
                        if(brightBox.Value > 0) {
                            dev.StateBox.Checked = true;
                        } else {
                            dev.StateBox.Checked = false;
                        }
                    }
                }
            }
        }

        public void HideExtBoxes(MyTreeNode curNode) {
            // Hide additonal boxes
            foreach (MyTreeNode item in curNode.Nodes) {
                item.BrightBox.Hide();
                item.StateBox.Hide();
            }
        }
        public void ShowExtBoxes(MyTreeNode curNode) {
            // Show additonal boxes
            foreach (MyTreeNode item in curNode.Nodes) {
                item.BrightBox.Show();
                item.StateBox.Show();
            }
        }
    }

    [Serializable]
    public class SceneryItem {
        public int DevId { get; set; } //Device id
        public int Bright { get; set; }
        public int State { get; set; }

        public SceneryItem() : this(0, 0, 0) {
        }
        public SceneryItem(int id, int state, int bright) {
            DevId = id;
            State = state;
            Bright = bright;
        }
    }
    [Serializable]
    public class Scenery {
        public string Name { get; set; }
        public SortedDictionary<int, SceneryItem> SceneryData { get; set; }
        public Scenery(string name) {
            SceneryData = new SortedDictionary<int, SceneryItem>();
            Name = name;
        }
    }
}
