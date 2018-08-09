namespace RFController {
    partial class DevicesForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.DevicesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firmwareVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchLoopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onTimeToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.offTimeToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.replaceToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redirectToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TypeBox = new System.Windows.Forms.Label();
            this.StateBox = new System.Windows.Forms.Label();
            this.StatePictBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLog_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roomsManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sceneryManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RoomSelector = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ShowScenBtn = new System.Windows.Forms.Button();
            this.ScenariesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DevicesPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatePictBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.RoomSelector.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DevicesPanel
            // 
            this.DevicesPanel.AutoScroll = true;
            this.DevicesPanel.AutoSize = true;
            this.DevicesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DevicesPanel.Controls.Add(this.groupBox1);
            this.DevicesPanel.Location = new System.Drawing.Point(3, 6);
            this.DevicesPanel.MaximumSize = new System.Drawing.Size(650, 0);
            this.DevicesPanel.MinimumSize = new System.Drawing.Size(150, 125);
            this.DevicesPanel.Name = "DevicesPanel";
            this.DevicesPanel.Size = new System.Drawing.Size(150, 125);
            this.DevicesPanel.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.ContextMenuStrip = this.ContextMenuStrip1;
            this.groupBox1.Controls.Add(this.TypeBox);
            this.groupBox1.Controls.Add(this.StateBox);
            this.groupBox1.Controls.Add(this.StatePictBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.MaximumSize = new System.Drawing.Size(120, 100);
            this.groupBox1.MinimumSize = new System.Drawing.Size(120, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeDeviceToolStripMenuItem,
            this.firmwareVersionToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.switchLoopToolStripMenuItem,
            this.replaceToToolStripMenuItem,
            this.redirectToToolStripMenuItem});
            this.ContextMenuStrip1.Name = "contextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(181, 158);
            // 
            // removeDeviceToolStripMenuItem
            // 
            this.removeDeviceToolStripMenuItem.Name = "removeDeviceToolStripMenuItem";
            this.removeDeviceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeDeviceToolStripMenuItem.Text = "Remove Device";
            // 
            // firmwareVersionToolStripMenuItem
            // 
            this.firmwareVersionToolStripMenuItem.Name = "firmwareVersionToolStripMenuItem";
            this.firmwareVersionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.firmwareVersionToolStripMenuItem.Text = "Firmware Version";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // switchLoopToolStripMenuItem
            // 
            this.switchLoopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onTimeToolStripMenuItem,
            this.offTimeToolStripMenuItem});
            this.switchLoopToolStripMenuItem.Name = "switchLoopToolStripMenuItem";
            this.switchLoopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.switchLoopToolStripMenuItem.Text = "Switch Loop";
            // 
            // onTimeToolStripMenuItem
            // 
            this.onTimeToolStripMenuItem.Name = "onTimeToolStripMenuItem";
            this.onTimeToolStripMenuItem.Size = new System.Drawing.Size(152, 23);
            this.onTimeToolStripMenuItem.Text = "On Time";
            // 
            // offTimeToolStripMenuItem
            // 
            this.offTimeToolStripMenuItem.Name = "offTimeToolStripMenuItem";
            this.offTimeToolStripMenuItem.Size = new System.Drawing.Size(212, 23);
            this.offTimeToolStripMenuItem.Text = "Off Time";
            // 
            // replaceToToolStripMenuItem
            // 
            this.replaceToToolStripMenuItem.Name = "replaceToToolStripMenuItem";
            this.replaceToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.replaceToToolStripMenuItem.Text = "Move to";
            this.replaceToToolStripMenuItem.MouseHover += new System.EventHandler(this.MoveTo_MouseHover);
            // 
            // redirectToToolStripMenuItem
            // 
            this.redirectToToolStripMenuItem.Name = "redirectToToolStripMenuItem";
            this.redirectToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.redirectToToolStripMenuItem.Text = "Redirect To";
            // 
            // TypeBox
            // 
            this.TypeBox.AutoSize = true;
            this.TypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TypeBox.Location = new System.Drawing.Point(9, 21);
            this.TypeBox.MinimumSize = new System.Drawing.Size(80, 0);
            this.TypeBox.Name = "TypeBox";
            this.TypeBox.Size = new System.Drawing.Size(80, 13);
            this.TypeBox.TabIndex = 17;
            this.TypeBox.Text = "label1";
            // 
            // StateBox
            // 
            this.StateBox.AutoSize = true;
            this.StateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StateBox.Location = new System.Drawing.Point(25, 60);
            this.StateBox.Name = "StateBox";
            this.StateBox.Size = new System.Drawing.Size(85, 24);
            this.StateBox.TabIndex = 16;
            this.StateBox.Text = "Br % / t C";
            // 
            // StatePictBox
            // 
            this.StatePictBox.BackColor = System.Drawing.Color.LightGreen;
            this.StatePictBox.Location = new System.Drawing.Point(94, 7);
            this.StatePictBox.Name = "StatePictBox";
            this.StatePictBox.Size = new System.Drawing.Size(25, 23);
            this.StatePictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StatePictBox.TabIndex = 15;
            this.StatePictBox.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogToolStripMenuItem,
            this.roomsManagerToolStripMenuItem,
            this.sceneryManagerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(954, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLog_MenuItem,
            this.serviceToolStripMenuItem,
            this.addNewDeviceMenuItem});
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.showLogToolStripMenuItem.Text = "Options";
            // 
            // ShowLog_MenuItem
            // 
            this.ShowLog_MenuItem.Name = "ShowLog_MenuItem";
            this.ShowLog_MenuItem.Size = new System.Drawing.Size(158, 22);
            this.ShowLog_MenuItem.Text = "ShowLog";
            this.ShowLog_MenuItem.Click += new System.EventHandler(this.ShowLog_MenuItem_Click);
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            this.serviceToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.serviceToolStripMenuItem.Text = "Service";
            this.serviceToolStripMenuItem.Click += new System.EventHandler(this.ServiceToolStrip_MenuItem_Click);
            // 
            // addNewDeviceMenuItem
            // 
            this.addNewDeviceMenuItem.Name = "addNewDeviceMenuItem";
            this.addNewDeviceMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addNewDeviceMenuItem.Text = "Add new device";
            this.addNewDeviceMenuItem.Click += new System.EventHandler(this.AddNewDevice_MenuItem_Click);
            // 
            // roomsManagerToolStripMenuItem
            // 
            this.roomsManagerToolStripMenuItem.Name = "roomsManagerToolStripMenuItem";
            this.roomsManagerToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.roomsManagerToolStripMenuItem.Text = "Rooms manager";
            this.roomsManagerToolStripMenuItem.Click += new System.EventHandler(this.RoomsManagerToolStripMenuItem_Click);
            // 
            // sceneryManagerToolStripMenuItem
            // 
            this.sceneryManagerToolStripMenuItem.Name = "sceneryManagerToolStripMenuItem";
            this.sceneryManagerToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.sceneryManagerToolStripMenuItem.Text = "Scenery manager";
            this.sceneryManagerToolStripMenuItem.Click += new System.EventHandler(this.SceneryManagerToolStripMenuItem_Click);
            // 
            // RoomSelector
            // 
            this.RoomSelector.Controls.Add(this.tabPage1);
            this.RoomSelector.Location = new System.Drawing.Point(3, 3);
            this.RoomSelector.Multiline = true;
            this.RoomSelector.Name = "RoomSelector";
            this.RoomSelector.Padding = new System.Drawing.Point(3, 3);
            this.RoomSelector.SelectedIndex = 0;
            this.RoomSelector.Size = new System.Drawing.Size(260, 314);
            this.RoomSelector.TabIndex = 15;
            this.RoomSelector.SelectedIndexChanged += new System.EventHandler(this.RoomSelector_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DevicesPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(252, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ShowScenBtn
            // 
            this.ShowScenBtn.AutoSize = true;
            this.ShowScenBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ShowScenBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ShowScenBtn.Location = new System.Drawing.Point(1, 321);
            this.ShowScenBtn.Margin = new System.Windows.Forms.Padding(1);
            this.ShowScenBtn.MaximumSize = new System.Drawing.Size(0, 20);
            this.ShowScenBtn.MinimumSize = new System.Drawing.Size(0, 20);
            this.ShowScenBtn.Name = "ShowScenBtn";
            this.ShowScenBtn.Size = new System.Drawing.Size(264, 20);
            this.ShowScenBtn.TabIndex = 16;
            this.ShowScenBtn.Text = "Sceneries";
            this.ShowScenBtn.UseVisualStyleBackColor = true;
            this.ShowScenBtn.Click += new System.EventHandler(this.ShowScenBtn_Click);
            // 
            // ScenariesPanel
            // 
            this.ScenariesPanel.AutoScroll = true;
            this.ScenariesPanel.AutoSize = true;
            this.ScenariesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ScenariesPanel.Location = new System.Drawing.Point(3, 345);
            this.ScenariesPanel.MaximumSize = new System.Drawing.Size(635, 60);
            this.ScenariesPanel.MinimumSize = new System.Drawing.Size(50, 60);
            this.ScenariesPanel.Name = "ScenariesPanel";
            this.ScenariesPanel.Size = new System.Drawing.Size(50, 60);
            this.ScenariesPanel.TabIndex = 0;
            this.ScenariesPanel.WrapContents = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.RoomSelector);
            this.flowLayoutPanel1.Controls.Add(this.ShowScenBtn);
            this.flowLayoutPanel1.Controls.Add(this.ScenariesPanel);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(266, 408);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // DevicesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(954, 555);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "DevicesForm";
            this.Text = "RFController - Devices";
            this.DevicesPanel.ResumeLayout(false);
            this.DevicesPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StatePictBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.RoomSelector.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel DevicesPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firmwareVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLog_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchLoopToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox onTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox offTimeToolStripMenuItem;
        private System.Windows.Forms.PictureBox StatePictBox;
        private System.Windows.Forms.Label StateBox;
        private System.Windows.Forms.Label TypeBox;
        private System.Windows.Forms.TabControl RoomSelector;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem roomsManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redirectToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sceneryManagerToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel ScenariesPanel;
        private System.Windows.Forms.Button ShowScenBtn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}