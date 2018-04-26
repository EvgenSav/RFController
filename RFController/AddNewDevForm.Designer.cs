namespace RFController {
    partial class AddNewDevForm {
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
            this.DevNameBox = new System.Windows.Forms.TextBox();
            this.DevTypeBox = new System.Windows.Forms.ComboBox();
            this.Step1ToolTip = new System.Windows.Forms.Label();
            this.Step2ToolTip = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Step3ToolTip = new System.Windows.Forms.Label();
            this.BindBtn = new System.Windows.Forms.Button();
            this.Step4Tooltip = new System.Windows.Forms.Label();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.Status = new System.Windows.Forms.TextBox();
            this.Step5ToolTip = new System.Windows.Forms.Label();
            this.YesStep5Btn = new System.Windows.Forms.Button();
            this.NoStep5Btn = new System.Windows.Forms.Button();
            this.Step6ToolTip = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DevNameBox
            // 
            this.DevNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DevNameBox.Location = new System.Drawing.Point(9, 34);
            this.DevNameBox.Name = "DevNameBox";
            this.DevNameBox.Size = new System.Drawing.Size(189, 23);
            this.DevNameBox.TabIndex = 1;
            this.DevNameBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DevNameBox_KeyUp);
            // 
            // DevTypeBox
            // 
            this.DevTypeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DevTypeBox.FormattingEnabled = true;
            this.DevTypeBox.Location = new System.Drawing.Point(9, 92);
            this.DevTypeBox.Name = "DevTypeBox";
            this.DevTypeBox.Size = new System.Drawing.Size(189, 24);
            this.DevTypeBox.TabIndex = 2;
            this.DevTypeBox.SelectionChangeCommitted += new System.EventHandler(this.DevTypeBox_SelectionChangeCommitted);
            // 
            // Step1ToolTip
            // 
            this.Step1ToolTip.AutoSize = true;
            this.Step1ToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step1ToolTip.Location = new System.Drawing.Point(12, 9);
            this.Step1ToolTip.Name = "Step1ToolTip";
            this.Step1ToolTip.Size = new System.Drawing.Size(175, 17);
            this.Step1ToolTip.TabIndex = 4;
            this.Step1ToolTip.Text = "Step 1. Enter device name";
            // 
            // Step2ToolTip
            // 
            this.Step2ToolTip.AutoSize = true;
            this.Step2ToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step2ToolTip.Location = new System.Drawing.Point(12, 66);
            this.Step2ToolTip.Name = "Step2ToolTip";
            this.Step2ToolTip.Size = new System.Drawing.Size(181, 17);
            this.Step2ToolTip.TabIndex = 5;
            this.Step2ToolTip.Text = "Step 2. Choose device type";
            // 
            // Step3ToolTip
            // 
            this.Step3ToolTip.AutoSize = true;
            this.Step3ToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step3ToolTip.Location = new System.Drawing.Point(10, 129);
            this.Step3ToolTip.MaximumSize = new System.Drawing.Size(190, 70);
            this.Step3ToolTip.Name = "Step3ToolTip";
            this.Step3ToolTip.Size = new System.Drawing.Size(190, 34);
            this.Step3ToolTip.TabIndex = 16;
            this.Step3ToolTip.Text = "Step 3. Press service button. (LED should start blinking)";
            // 
            // BindBtn
            // 
            this.BindBtn.Location = new System.Drawing.Point(11, 199);
            this.BindBtn.Name = "BindBtn";
            this.BindBtn.Size = new System.Drawing.Size(188, 25);
            this.BindBtn.TabIndex = 17;
            this.BindBtn.Text = "Bind";
            this.BindBtn.UseVisualStyleBackColor = true;
            this.BindBtn.Click += new System.EventHandler(this.BindBtn_Click);
            // 
            // Step4Tooltip
            // 
            this.Step4Tooltip.AutoSize = true;
            this.Step4Tooltip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step4Tooltip.Location = new System.Drawing.Point(12, 172);
            this.Step4Tooltip.Name = "Step4Tooltip";
            this.Step4Tooltip.Size = new System.Drawing.Size(125, 17);
            this.Step4Tooltip.TabIndex = 18;
            this.Step4Tooltip.Text = "Step 4. Press Bind";
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 150);
            // 
            // Status
            // 
            this.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Status.Location = new System.Drawing.Point(0, 450);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(209, 23);
            this.Status.TabIndex = 25;
            this.Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Step5ToolTip
            // 
            this.Step5ToolTip.AutoSize = true;
            this.Step5ToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step5ToolTip.Location = new System.Drawing.Point(13, 236);
            this.Step5ToolTip.MaximumSize = new System.Drawing.Size(180, 0);
            this.Step5ToolTip.Name = "Step5ToolTip";
            this.Step5ToolTip.Size = new System.Drawing.Size(178, 51);
            this.Step5ToolTip.TabIndex = 26;
            this.Step5ToolTip.Text = "Step 5. Is LED blinking fast? (if not - repeat Step 4 and/or clear memory)";
            // 
            // YesStep5Btn
            // 
            this.YesStep5Btn.Location = new System.Drawing.Point(16, 300);
            this.YesStep5Btn.Name = "YesStep5Btn";
            this.YesStep5Btn.Size = new System.Drawing.Size(52, 25);
            this.YesStep5Btn.TabIndex = 27;
            this.YesStep5Btn.Text = "Yes";
            this.YesStep5Btn.UseVisualStyleBackColor = true;
            this.YesStep5Btn.Click += new System.EventHandler(this.YesBtnClick);
            // 
            // NoStep5Btn
            // 
            this.NoStep5Btn.Location = new System.Drawing.Point(142, 300);
            this.NoStep5Btn.Name = "NoStep5Btn";
            this.NoStep5Btn.Size = new System.Drawing.Size(52, 25);
            this.NoStep5Btn.TabIndex = 28;
            this.NoStep5Btn.Text = "No";
            this.NoStep5Btn.UseVisualStyleBackColor = true;
            this.NoStep5Btn.Click += new System.EventHandler(this.NoBtnClick);
            // 
            // Step6ToolTip
            // 
            this.Step6ToolTip.AutoSize = true;
            this.Step6ToolTip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step6ToolTip.Location = new System.Drawing.Point(11, 333);
            this.Step6ToolTip.MaximumSize = new System.Drawing.Size(180, 0);
            this.Step6ToolTip.Name = "Step6ToolTip";
            this.Step6ToolTip.Size = new System.Drawing.Size(178, 68);
            this.Step6ToolTip.TabIndex = 29;
            this.Step6ToolTip.Text = "Step 6. Press service button another one.(LED should start blinking slow). Then c" +
    "lick OK";
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(9, 413);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(180, 25);
            this.OkBtn.TabIndex = 30;
            this.OkBtn.Text = "Ok";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // AddNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(209, 473);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.Step6ToolTip);
            this.Controls.Add(this.NoStep5Btn);
            this.Controls.Add(this.YesStep5Btn);
            this.Controls.Add(this.Step5ToolTip);
            this.Controls.Add(this.Step4Tooltip);
            this.Controls.Add(this.BindBtn);
            this.Controls.Add(this.Step3ToolTip);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.DevTypeBox);
            this.Controls.Add(this.Step2ToolTip);
            this.Controls.Add(this.DevNameBox);
            this.Controls.Add(this.Step1ToolTip);
            this.MinimumSize = new System.Drawing.Size(225, 130);
            this.Name = "AddNewForm";
            this.Text = "Adding new device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox DevNameBox;
        private System.Windows.Forms.ComboBox DevTypeBox;
        private System.Windows.Forms.Label Step1ToolTip;
        private System.Windows.Forms.Label Step2ToolTip;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label Step3ToolTip;
        private System.Windows.Forms.Button BindBtn;
        private System.Windows.Forms.Label Step4Tooltip;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.TextBox Status;
        private System.Windows.Forms.Label Step5ToolTip;
        private System.Windows.Forms.Button YesStep5Btn;
        private System.Windows.Forms.Button NoStep5Btn;
        private System.Windows.Forms.Label Step6ToolTip;
        private System.Windows.Forms.Button OkBtn;
    }
}