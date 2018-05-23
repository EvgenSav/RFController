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
            this.BindBtn = new System.Windows.Forms.Button();
            this.ChannelSel = new System.Windows.Forms.ComboBox();
            this.UnbindBtn = new System.Windows.Forms.Button();
            this.ModeSel = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SendBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CmdSel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Format = new System.Windows.Forms.NumericUpDown();
            this.D0 = new System.Windows.Forms.NumericUpDown();
            this.D1 = new System.Windows.Forms.NumericUpDown();
            this.D2 = new System.Windows.Forms.NumericUpDown();
            this.D3 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Adr = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Format)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Adr)).BeginInit();
            this.SuspendLayout();
            // 
            // BindBtn
            // 
            this.BindBtn.Location = new System.Drawing.Point(273, 12);
            this.BindBtn.Name = "BindBtn";
            this.BindBtn.Size = new System.Drawing.Size(75, 23);
            this.BindBtn.TabIndex = 5;
            this.BindBtn.Text = "Bind";
            this.BindBtn.UseVisualStyleBackColor = true;
            this.BindBtn.Click += new System.EventHandler(this.BtnBind_Click);
            // 
            // ChannelSel
            // 
            this.ChannelSel.FormattingEnabled = true;
            this.ChannelSel.Location = new System.Drawing.Point(97, 89);
            this.ChannelSel.Name = "ChannelSel";
            this.ChannelSel.Size = new System.Drawing.Size(63, 21);
            this.ChannelSel.TabIndex = 6;
            // 
            // UnbindBtn
            // 
            this.UnbindBtn.Location = new System.Drawing.Point(354, 12);
            this.UnbindBtn.Name = "UnbindBtn";
            this.UnbindBtn.Size = new System.Drawing.Size(75, 23);
            this.UnbindBtn.TabIndex = 7;
            this.UnbindBtn.Text = "Unbind";
            this.UnbindBtn.UseVisualStyleBackColor = true;
            this.UnbindBtn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BtnUnbind_KeyDown);
            // 
            // ModeSel
            // 
            this.ModeSel.FormattingEnabled = true;
            this.ModeSel.Location = new System.Drawing.Point(175, 89);
            this.ModeSel.Name = "ModeSel";
            this.ModeSel.Size = new System.Drawing.Size(62, 21);
            this.ModeSel.TabIndex = 9;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 219);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(617, 22);
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
            this.label2.Location = new System.Drawing.Point(192, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Mode";
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(483, 91);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(75, 23);
            this.SendBtn.TabIndex = 15;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.BindBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Format";
            // 
            // CmdSel
            // 
            this.CmdSel.FormattingEnabled = true;
            this.CmdSel.Location = new System.Drawing.Point(354, 91);
            this.CmdSel.Name = "CmdSel";
            this.CmdSel.Size = new System.Drawing.Size(105, 21);
            this.CmdSel.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Cmd";
            // 
            // Format
            // 
            this.Format.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Format.Location = new System.Drawing.Point(273, 89);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(64, 23);
            this.Format.TabIndex = 20;
            // 
            // D0
            // 
            this.D0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.D0.Location = new System.Drawing.Point(97, 134);
            this.D0.Name = "D0";
            this.D0.Size = new System.Drawing.Size(63, 23);
            this.D0.TabIndex = 21;
            // 
            // D1
            // 
            this.D1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.D1.Location = new System.Drawing.Point(183, 134);
            this.D1.Name = "D1";
            this.D1.Size = new System.Drawing.Size(62, 23);
            this.D1.TabIndex = 22;
            // 
            // D2
            // 
            this.D2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.D2.Location = new System.Drawing.Point(273, 134);
            this.D2.Name = "D2";
            this.D2.Size = new System.Drawing.Size(64, 23);
            this.D2.TabIndex = 23;
            // 
            // D3
            // 
            this.D3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.D3.Location = new System.Drawing.Point(359, 134);
            this.D3.Name = "D3";
            this.D3.Size = new System.Drawing.Size(70, 23);
            this.D3.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(114, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "D0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "D1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "D2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(381, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "D3";
            // 
            // Adr
            // 
            this.Adr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Adr.Location = new System.Drawing.Point(97, 177);
            this.Adr.Name = "Adr";
            this.Adr.Size = new System.Drawing.Size(63, 23);
            this.Adr.TabIndex = 29;
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 241);
            this.Controls.Add(this.Adr);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.D3);
            this.Controls.Add(this.D2);
            this.Controls.Add(this.D1);
            this.Controls.Add(this.D0);
            this.Controls.Add(this.Format);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CmdSel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ModeSel);
            this.Controls.Add(this.UnbindBtn);
            this.Controls.Add(this.ChannelSel);
            this.Controls.Add(this.BindBtn);
            this.Name = "ServiceForm";
            this.Text = "Service";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Format)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Adr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BindBtn;
        private System.Windows.Forms.ComboBox ChannelSel;
        private System.Windows.Forms.Button UnbindBtn;
        private System.Windows.Forms.ComboBox ModeSel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CmdSel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown Format;
        private System.Windows.Forms.NumericUpDown D0;
        private System.Windows.Forms.NumericUpDown D1;
        private System.Windows.Forms.NumericUpDown D2;
        private System.Windows.Forms.NumericUpDown D3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Adr;
    }
}

