namespace RFController {
    partial class LogForm {
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
            this.TxBox = new System.Windows.Forms.TextBox();
            this.RxBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearRxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxBox
            // 
            this.TxBox.BackColor = System.Drawing.SystemColors.Control;
            this.TxBox.ContextMenuStrip = this.contextMenuStrip1;
            this.TxBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TxBox.Location = new System.Drawing.Point(12, 24);
            this.TxBox.Multiline = true;
            this.TxBox.Name = "TxBox";
            this.TxBox.ReadOnly = true;
            this.TxBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxBox.Size = new System.Drawing.Size(784, 108);
            this.TxBox.TabIndex = 13;
            // 
            // RxBox
            // 
            this.RxBox.ContextMenuStrip = this.contextMenuStrip1;
            this.RxBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.RxBox.Location = new System.Drawing.Point(12, 155);
            this.RxBox.Multiline = true;
            this.RxBox.Name = "RxBox";
            this.RxBox.ReadOnly = true;
            this.RxBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RxBox.Size = new System.Drawing.Size(784, 108);
            this.RxBox.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tx";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Rx";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.clearRxToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearToolStripMenuItem.Text = "Clear Tx";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // clearRxToolStripMenuItem
            // 
            this.clearRxToolStripMenuItem.Name = "clearRxToolStripMenuItem";
            this.clearRxToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.clearRxToolStripMenuItem.Text = "Clear Rx";
            this.clearRxToolStripMenuItem.Click += new System.EventHandler(this.clearRxToolStripMenuItem_Click);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 295);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RxBox);
            this.Controls.Add(this.TxBox);
            this.Name = "LogForm";
            this.Text = "Log";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxBox;
        private System.Windows.Forms.TextBox RxBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearRxToolStripMenuItem;
    }
}