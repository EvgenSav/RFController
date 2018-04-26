namespace RFController {
    partial class SettingsForm {
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
            this.SaveState = new System.Windows.Forms.CheckBox();
            this.Dimmer = new System.Windows.Forms.CheckBox();
            this.DefaultOn = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OnLvl = new System.Windows.Forms.NumericUpDown();
            this.DimLvlLow = new System.Windows.Forms.NumericUpDown();
            this.DimLvlHi = new System.Windows.Forms.NumericUpDown();
            this.ApplySettingsBtn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OnLvl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DimLvlLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DimLvlHi)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveState
            // 
            this.SaveState.AutoSize = true;
            this.SaveState.Location = new System.Drawing.Point(6, 19);
            this.SaveState.Name = "SaveState";
            this.SaveState.Size = new System.Drawing.Size(77, 17);
            this.SaveState.TabIndex = 0;
            this.SaveState.Text = "Save state";
            this.SaveState.UseVisualStyleBackColor = true;
            // 
            // Dimmer
            // 
            this.Dimmer.AutoSize = true;
            this.Dimmer.Location = new System.Drawing.Point(6, 65);
            this.Dimmer.Name = "Dimmer";
            this.Dimmer.Size = new System.Drawing.Size(61, 17);
            this.Dimmer.TabIndex = 0;
            this.Dimmer.Text = "Dimmer";
            this.Dimmer.UseVisualStyleBackColor = true;
            this.Dimmer.CheckedChanged += new System.EventHandler(this.Dimmer_CheckedChanged);
            // 
            // DefaultOn
            // 
            this.DefaultOn.AutoSize = true;
            this.DefaultOn.Location = new System.Drawing.Point(6, 42);
            this.DefaultOn.Name = "DefaultOn";
            this.DefaultOn.Size = new System.Drawing.Size(77, 17);
            this.DefaultOn.TabIndex = 0;
            this.DefaultOn.Text = "Default On";
            this.DefaultOn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.ApplySettingsBtn);
            this.groupBox1.Controls.Add(this.SaveState);
            this.groupBox1.Controls.Add(this.DefaultOn);
            this.groupBox1.Controls.Add(this.Dimmer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 242);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.OnLvl);
            this.groupBox2.Controls.Add(this.DimLvlLow);
            this.groupBox2.Controls.Add(this.DimLvlHi);
            this.groupBox2.Location = new System.Drawing.Point(6, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 114);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dimmer correction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Low";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "High";
            // 
            // OnLvl
            // 
            this.OnLvl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OnLvl.Location = new System.Drawing.Point(44, 78);
            this.OnLvl.Name = "OnLvl";
            this.OnLvl.Size = new System.Drawing.Size(68, 23);
            this.OnLvl.TabIndex = 5;
            // 
            // DimLvlLow
            // 
            this.DimLvlLow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DimLvlLow.Location = new System.Drawing.Point(44, 48);
            this.DimLvlLow.Name = "DimLvlLow";
            this.DimLvlLow.Size = new System.Drawing.Size(68, 23);
            this.DimLvlLow.TabIndex = 5;
            // 
            // DimLvlHi
            // 
            this.DimLvlHi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DimLvlHi.Location = new System.Drawing.Point(44, 19);
            this.DimLvlHi.Name = "DimLvlHi";
            this.DimLvlHi.Size = new System.Drawing.Size(68, 23);
            this.DimLvlHi.TabIndex = 5;
            // 
            // ApplySettingsBtn
            // 
            this.ApplySettingsBtn.Location = new System.Drawing.Point(29, 208);
            this.ApplySettingsBtn.Name = "ApplySettingsBtn";
            this.ApplySettingsBtn.Size = new System.Drawing.Size(75, 23);
            this.ApplySettingsBtn.TabIndex = 4;
            this.ApplySettingsBtn.Text = "Apply";
            this.ApplySettingsBtn.UseVisualStyleBackColor = true;
            this.ApplySettingsBtn.Click += new System.EventHandler(this.ApplySettingsBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "On";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OnLvl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DimLvlLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DimLvlHi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox SaveState;
        private System.Windows.Forms.CheckBox Dimmer;
        private System.Windows.Forms.CheckBox DefaultOn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ApplySettingsBtn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown OnLvl;
        private System.Windows.Forms.NumericUpDown DimLvlLow;
        private System.Windows.Forms.NumericUpDown DimLvlHi;
        private System.Windows.Forms.Label label3;
    }
}