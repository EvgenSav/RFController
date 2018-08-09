namespace RFController {
    partial class DevView {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevView));
            this.DevBox = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Info_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.Remove_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.SwitchLoop_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.onTimeToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.offTimeToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.MoveTo_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.RedirectTo_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.TypePict = new System.Windows.Forms.PictureBox();
            this.StatePict2 = new System.Windows.Forms.PictureBox();
            this.StatePict = new System.Windows.Forms.PictureBox();
            this.StateLbl = new System.Windows.Forms.Label();
            this.TypeNameLbl = new System.Windows.Forms.Label();
            this.Log_mi = new System.Windows.Forms.ToolStripMenuItem();
            this.DevBox.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypePict)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatePict2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatePict)).BeginInit();
            this.SuspendLayout();
            // 
            // DevBox
            // 
            this.DevBox.AutoSize = true;
            this.DevBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DevBox.ContextMenuStrip = this.contextMenuStrip1;
            this.DevBox.Controls.Add(this.TypePict);
            this.DevBox.Controls.Add(this.StatePict2);
            this.DevBox.Controls.Add(this.StatePict);
            this.DevBox.Controls.Add(this.StateLbl);
            this.DevBox.Controls.Add(this.TypeNameLbl);
            this.DevBox.Location = new System.Drawing.Point(0, 0);
            this.DevBox.MaximumSize = new System.Drawing.Size(120, 100);
            this.DevBox.MinimumSize = new System.Drawing.Size(120, 100);
            this.DevBox.Name = "DevBox";
            this.DevBox.Size = new System.Drawing.Size(120, 100);
            this.DevBox.TabIndex = 0;
            this.DevBox.TabStop = false;
            this.DevBox.Text = "groupBox1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Info_mi,
            this.Remove_mi,
            this.Settings_mi,
            this.SwitchLoop_mi,
            this.MoveTo_mi,
            this.RedirectTo_mi,
            this.Log_mi});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 180);
            // 
            // Info_mi
            // 
            this.Info_mi.Name = "Info_mi";
            this.Info_mi.Size = new System.Drawing.Size(180, 22);
            this.Info_mi.Text = "Info";
            // 
            // Remove_mi
            // 
            this.Remove_mi.Name = "Remove_mi";
            this.Remove_mi.Size = new System.Drawing.Size(180, 22);
            this.Remove_mi.Text = "Remove";
            // 
            // Settings_mi
            // 
            this.Settings_mi.Name = "Settings_mi";
            this.Settings_mi.Size = new System.Drawing.Size(180, 22);
            this.Settings_mi.Text = "Settings";
            // 
            // SwitchLoop_mi
            // 
            this.SwitchLoop_mi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onTimeToolStripMenuItem,
            this.offTimeToolStripMenuItem});
            this.SwitchLoop_mi.Name = "SwitchLoop_mi";
            this.SwitchLoop_mi.Size = new System.Drawing.Size(180, 22);
            this.SwitchLoop_mi.Text = "Switch Loop";
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
            // MoveTo_mi
            // 
            this.MoveTo_mi.Name = "MoveTo_mi";
            this.MoveTo_mi.Size = new System.Drawing.Size(180, 22);
            this.MoveTo_mi.Text = "Move To";
            // 
            // RedirectTo_mi
            // 
            this.RedirectTo_mi.Name = "RedirectTo_mi";
            this.RedirectTo_mi.Size = new System.Drawing.Size(180, 22);
            this.RedirectTo_mi.Text = "Redirect To";
            // 
            // TypePict
            // 
            this.TypePict.Location = new System.Drawing.Point(6, 19);
            this.TypePict.Name = "TypePict";
            this.TypePict.Size = new System.Drawing.Size(23, 24);
            this.TypePict.TabIndex = 20;
            this.TypePict.TabStop = false;
            // 
            // StatePict2
            // 
            this.StatePict2.Location = new System.Drawing.Point(6, 58);
            this.StatePict2.Name = "StatePict2";
            this.StatePict2.Size = new System.Drawing.Size(23, 24);
            this.StatePict2.TabIndex = 19;
            this.StatePict2.TabStop = false;
            // 
            // StatePict
            // 
            this.StatePict.Image = ((System.Drawing.Image)(resources.GetObject("StatePict.Image")));
            this.StatePict.Location = new System.Drawing.Point(94, 8);
            this.StatePict.Name = "StatePict";
            this.StatePict.Size = new System.Drawing.Size(25, 25);
            this.StatePict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StatePict.TabIndex = 18;
            this.StatePict.TabStop = false;
            // 
            // StateLbl
            // 
            this.StateLbl.AutoSize = true;
            this.StateLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StateLbl.Location = new System.Drawing.Point(30, 58);
            this.StateLbl.Name = "StateLbl";
            this.StateLbl.Size = new System.Drawing.Size(85, 24);
            this.StateLbl.TabIndex = 17;
            this.StateLbl.Text = "Br % / t C";
            // 
            // TypeNameLbl
            // 
            this.TypeNameLbl.AutoSize = true;
            this.TypeNameLbl.Location = new System.Drawing.Point(31, 30);
            this.TypeNameLbl.Name = "TypeNameLbl";
            this.TypeNameLbl.Size = new System.Drawing.Size(31, 13);
            this.TypeNameLbl.TabIndex = 0;
            this.TypeNameLbl.Text = "Type";
            // 
            // Log_mi
            // 
            this.Log_mi.Name = "Log_mi";
            this.Log_mi.Size = new System.Drawing.Size(180, 22);
            this.Log_mi.Text = "Log";
            // 
            // DevView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.DevBox);
            this.MaximumSize = new System.Drawing.Size(120, 100);
            this.MinimumSize = new System.Drawing.Size(120, 100);
            this.Name = "DevView";
            this.Size = new System.Drawing.Size(120, 100);
            this.DevBox.ResumeLayout(false);
            this.DevBox.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TypePict)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatePict2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StatePict)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox DevBox;
        private System.Windows.Forms.Label TypeNameLbl;
        private System.Windows.Forms.Label StateLbl;
        private System.Windows.Forms.PictureBox StatePict;
        private System.Windows.Forms.PictureBox StatePict2;
        private System.Windows.Forms.PictureBox TypePict;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Info_mi;
        private System.Windows.Forms.ToolStripMenuItem Remove_mi;
        private System.Windows.Forms.ToolStripMenuItem Settings_mi;
        private System.Windows.Forms.ToolStripMenuItem SwitchLoop_mi;
        private System.Windows.Forms.ToolStripTextBox onTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox offTimeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveTo_mi;
        private System.Windows.Forms.ToolStripMenuItem RedirectTo_mi;
        private System.Windows.Forms.ToolStripMenuItem Log_mi;
    }
}
