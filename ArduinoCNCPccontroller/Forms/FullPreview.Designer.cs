namespace ArduinoCNCPccontroller.Forms
{
    partial class FullPreview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.lblMachineData = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.SuspendLayout();
            // 
            // drawBox
            // 
            this.drawBox.Location = new System.Drawing.Point(12, 63);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(1221, 719);
            this.drawBox.TabIndex = 0;
            this.drawBox.TabStop = false;
            // 
            // lblMachineData
            // 
            this.lblMachineData.AutoSize = true;
            this.lblMachineData.Location = new System.Drawing.Point(26, 21);
            this.lblMachineData.Name = "lblMachineData";
            this.lblMachineData.Size = new System.Drawing.Size(35, 13);
            this.lblMachineData.TabIndex = 1;
            this.lblMachineData.Text = "label1";
            // 
            // FullPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1245, 794);
            this.Controls.Add(this.lblMachineData);
            this.Controls.Add(this.drawBox);
            this.Name = "FullPreview";
            this.Text = "FullPreview";
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox drawBox;
        private System.Windows.Forms.Label lblMachineData;
    }
}