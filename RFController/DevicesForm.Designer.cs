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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firmwareVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchLoopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onTimeToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.offTimeToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.BrightBox = new System.Windows.Forms.NumericUpDown();
            this.DimmerEn = new System.Windows.Forms.CheckBox();
            this.TypeBox = new System.Windows.Forms.TextBox();
            this.OffBtn = new System.Windows.Forms.Button();
            this.OnBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLog_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Temperature_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewDeviceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatePictBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatePictBox)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 29);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(1100, 0);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(150, 250);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(150, 250);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox1.Controls.Add(this.StatePictBox);
            this.groupBox1.Controls.Add(this.BrightBox);
            this.groupBox1.Controls.Add(this.DimmerEn);
            this.groupBox1.Controls.Add(this.TypeBox);
            this.groupBox1.Controls.Add(this.OffBtn);
            this.groupBox1.Controls.Add(this.OnBtn);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.MaximumSize = new System.Drawing.Size(135, 190);
            this.groupBox1.MinimumSize = new System.Drawing.Size(135, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeDeviceToolStripMenuItem,
            this.firmwareVersionToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.switchLoopToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 92);
            // 
            // removeDeviceToolStripMenuItem
            // 
            this.removeDeviceToolStripMenuItem.Name = "removeDeviceToolStripMenuItem";
            this.removeDeviceToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.removeDeviceToolStripMenuItem.Text = "Remove Device";
            // 
            // firmwareVersionToolStripMenuItem
            // 
            this.firmwareVersionToolStripMenuItem.Name = "firmwareVersionToolStripMenuItem";
            this.firmwareVersionToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.firmwareVersionToolStripMenuItem.Text = "Firmware Version";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // switchLoopToolStripMenuItem
            // 
            this.switchLoopToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onTimeToolStripMenuItem,
            this.offTimeToolStripMenuItem});
            this.switchLoopToolStripMenuItem.Name = "switchLoopToolStripMenuItem";
            this.switchLoopToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
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
            // BrightBox
            // 
            this.BrightBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BrightBox.Location = new System.Drawing.Point(19, 48);
            this.BrightBox.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.BrightBox.Name = "BrightBox";
            this.BrightBox.Size = new System.Drawing.Size(100, 23);
            this.BrightBox.TabIndex = 8;
            // 
            // DimmerEn
            // 
            this.DimmerEn.AutoSize = true;
            this.DimmerEn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DimmerEn.Location = new System.Drawing.Point(19, 106);
            this.DimmerEn.Name = "DimmerEn";
            this.DimmerEn.Size = new System.Drawing.Size(89, 21);
            this.DimmerEn.TabIndex = 7;
            this.DimmerEn.Text = "Dimmable";
            this.DimmerEn.UseVisualStyleBackColor = true;
            // 
            // TypeBox
            // 
            this.TypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TypeBox.Location = new System.Drawing.Point(19, 19);
            this.TypeBox.Name = "TypeBox";
            this.TypeBox.Size = new System.Drawing.Size(79, 23);
            this.TypeBox.TabIndex = 4;
            // 
            // OffBtn
            // 
            this.OffBtn.Location = new System.Drawing.Point(70, 77);
            this.OffBtn.Name = "OffBtn";
            this.OffBtn.Size = new System.Drawing.Size(49, 23);
            this.OffBtn.TabIndex = 1;
            this.OffBtn.Text = "Off";
            this.OffBtn.UseVisualStyleBackColor = true;
            // 
            // OnBtn
            // 
            this.OnBtn.Location = new System.Drawing.Point(19, 77);
            this.OnBtn.Name = "OnBtn";
            this.OnBtn.Size = new System.Drawing.Size(45, 23);
            this.OnBtn.TabIndex = 0;
            this.OnBtn.Text = "On";
            this.OnBtn.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLog_MenuItem,
            this.Temperature_MenuItem,
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
            // Temperature_MenuItem
            // 
            this.Temperature_MenuItem.Name = "Temperature_MenuItem";
            this.Temperature_MenuItem.Size = new System.Drawing.Size(158, 22);
            this.Temperature_MenuItem.Text = "Temperature";
            this.Temperature_MenuItem.Click += new System.EventHandler(this.Temperature_MenuItem_Click);
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
            // StatePictBox
            // 
            this.StatePictBox.BackColor = System.Drawing.Color.LightGreen;
            this.StatePictBox.Location = new System.Drawing.Point(109, 7);
            this.StatePictBox.Name = "StatePictBox";
            this.StatePictBox.Size = new System.Drawing.Size(25, 23);
            this.StatePictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StatePictBox.TabIndex = 15;
            this.StatePictBox.TabStop = false;
            // 
            // DevicesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(764, 555);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DevicesForm";
            this.Text = "RFController - Devices";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrightBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StatePictBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button OnBtn;
        private System.Windows.Forms.Button OffBtn;
        private System.Windows.Forms.TextBox TypeBox;
        private System.Windows.Forms.CheckBox DimmerEn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeDeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firmwareVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown BrightBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLog_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Temperature_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewDeviceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchLoopToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox onTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox offTimeToolStripMenuItem;
        private System.Windows.Forms.PictureBox StatePictBox;
    }
}