namespace RFController {
    partial class ServiceForm {
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.btnConnect = new System.Windows.Forms.Button();
            this.portSel = new System.Windows.Forms.ComboBox();
            this.btnBind = new System.Windows.Forms.Button();
            this.channelSel = new System.Windows.Forms.ComboBox();
            this.btnUnbind = new System.Windows.Forms.Button();
            this.modeSel = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(117, 41);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // portSel
            // 
            this.portSel.FormattingEnabled = true;
            this.portSel.Location = new System.Drawing.Point(16, 43);
            this.portSel.Name = "portSel";
            this.portSel.Size = new System.Drawing.Size(85, 21);
            this.portSel.TabIndex = 3;
            this.portSel.Click += new System.EventHandler(this.portSel_Click);
            // 
            // btnBind
            // 
            this.btnBind.Location = new System.Drawing.Point(16, 87);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(75, 23);
            this.btnBind.TabIndex = 5;
            this.btnBind.Text = "Bind";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.BtnBind_Click);
            // 
            // channelSel
            // 
            this.channelSel.FormattingEnabled = true;
            this.channelSel.Location = new System.Drawing.Point(97, 89);
            this.channelSel.Name = "channelSel";
            this.channelSel.Size = new System.Drawing.Size(80, 21);
            this.channelSel.TabIndex = 6;
            // 
            // btnUnbind
            // 
            this.btnUnbind.Location = new System.Drawing.Point(273, 87);
            this.btnUnbind.Name = "btnUnbind";
            this.btnUnbind.Size = new System.Drawing.Size(75, 23);
            this.btnUnbind.TabIndex = 7;
            this.btnUnbind.Text = "Unbind";
            this.btnUnbind.UseVisualStyleBackColor = true;
            this.btnUnbind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnUnbind_KeyDown);
            // 
            // modeSel
            // 
            this.modeSel.FormattingEnabled = true;
            this.modeSel.Location = new System.Drawing.Point(183, 89);
            this.modeSel.Name = "modeSel";
            this.modeSel.Size = new System.Drawing.Size(84, 21);
            this.modeSel.TabIndex = 9;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 132);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(367, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel1.Text = "Not connected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Channel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Mode";
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 154);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.modeSel);
            this.Controls.Add(this.btnUnbind);
            this.Controls.Add(this.channelSel);
            this.Controls.Add(this.btnBind);
            this.Controls.Add(this.portSel);
            this.Controls.Add(this.btnConnect);
            this.Name = "ServiceForm";
            this.Text = "Service";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox portSel;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.ComboBox channelSel;
        private System.Windows.Forms.Button btnUnbind;
        private System.Windows.Forms.ComboBox modeSel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

