namespace ArduinoCNCPccontroller.Forms
{
    partial class SettingsForm
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
            this.tbXres = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYres = new System.Windows.Forms.TextBox();
            this.tbZres = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSpindleRPM = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPrevXmm = new System.Windows.Forms.TextBox();
            this.tbPreviYmm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbFullPreviXmm = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbPrevClr = new System.Windows.Forms.TextBox();
            this.tbLivePrevClr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbCursorPrevWidth = new System.Windows.Forms.TextBox();
            this.tbLivePrevCursorWidth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chckMotorsOff = new System.Windows.Forms.CheckBox();
            this.chckLivePrev = new System.Windows.Forms.CheckBox();
            this.chckPos = new System.Windows.Forms.CheckBox();
            this.chckDebug = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbBaudRate = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.chckZshow = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbZlimit = new System.Windows.Forms.TextBox();
            this.tbZcolor = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbXres
            // 
            this.tbXres.Location = new System.Drawing.Point(77, 24);
            this.tbXres.Name = "tbXres";
            this.tbXres.Size = new System.Drawing.Size(83, 20);
            this.tbXres.TabIndex = 0;
            this.tbXres.TextChanged += new System.EventHandler(this.tbXres_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Motor res _x";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Motor res _y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Motor res _z";
            // 
            // tbYres
            // 
            this.tbYres.Location = new System.Drawing.Point(77, 58);
            this.tbYres.Name = "tbYres";
            this.tbYres.Size = new System.Drawing.Size(83, 20);
            this.tbYres.TabIndex = 4;
            this.tbYres.TextChanged += new System.EventHandler(this.tbYres_TextChanged);
            // 
            // tbZres
            // 
            this.tbZres.Location = new System.Drawing.Point(77, 87);
            this.tbZres.Name = "tbZres";
            this.tbZres.Size = new System.Drawing.Size(83, 20);
            this.tbZres.TabIndex = 5;
            this.tbZres.TextChanged += new System.EventHandler(this.tbZres_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbZres);
            this.groupBox1.Controls.Add(this.tbXres);
            this.groupBox1.Controls.Add(this.tbYres);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 125);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motor resolution in mm";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbSpindleRPM);
            this.groupBox2.Location = new System.Drawing.Point(223, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(188, 125);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spindle in RPM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Spindle:";
            // 
            // tbSpindleRPM
            // 
            this.tbSpindleRPM.Location = new System.Drawing.Point(6, 43);
            this.tbSpindleRPM.Name = "tbSpindleRPM";
            this.tbSpindleRPM.Size = new System.Drawing.Size(176, 20);
            this.tbSpindleRPM.TabIndex = 4;
            this.tbSpindleRPM.TextChanged += new System.EventHandler(this.tbSpindleRPM_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbPrevXmm);
            this.groupBox3.Controls.Add(this.tbPreviYmm);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 93);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "minI Preview size ,mm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "X";
            // 
            // tbPrevXmm
            // 
            this.tbPrevXmm.Location = new System.Drawing.Point(26, 24);
            this.tbPrevXmm.Name = "tbPrevXmm";
            this.tbPrevXmm.Size = new System.Drawing.Size(134, 20);
            this.tbPrevXmm.TabIndex = 0;
            this.tbPrevXmm.TextChanged += new System.EventHandler(this.tbPrevXmm_TextChanged);
            // 
            // tbPreviYmm
            // 
            this.tbPreviYmm.Location = new System.Drawing.Point(26, 58);
            this.tbPreviYmm.Name = "tbPreviYmm";
            this.tbPreviYmm.Size = new System.Drawing.Size(134, 20);
            this.tbPreviYmm.TabIndex = 4;
            this.tbPreviYmm.TextChanged += new System.EventHandler(this.tbPreviYmm_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Y";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbFullPreviXmm);
            this.groupBox4.Location = new System.Drawing.Point(223, 143);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 93);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Preview size, mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "X/Y";
            // 
            // tbFullPreviXmm
            // 
            this.tbFullPreviXmm.Location = new System.Drawing.Point(38, 24);
            this.tbFullPreviXmm.Name = "tbFullPreviXmm";
            this.tbFullPreviXmm.Size = new System.Drawing.Size(134, 20);
            this.tbFullPreviXmm.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(223, 649);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(188, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(9, 649);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(191, 38);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.tbPrevClr);
            this.groupBox5.Controls.Add(this.tbLivePrevClr);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Location = new System.Drawing.Point(12, 364);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(188, 93);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Colors";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "preview clr";
            // 
            // tbPrevClr
            // 
            this.tbPrevClr.Location = new System.Drawing.Point(89, 24);
            this.tbPrevClr.Name = "tbPrevClr";
            this.tbPrevClr.Size = new System.Drawing.Size(93, 20);
            this.tbPrevClr.TabIndex = 0;
            this.tbPrevClr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewColorSlct);
            // 
            // tbLivePrevClr
            // 
            this.tbLivePrevClr.Location = new System.Drawing.Point(89, 58);
            this.tbLivePrevClr.Name = "tbLivePrevClr";
            this.tbLivePrevClr.Size = new System.Drawing.Size(93, 20);
            this.tbLivePrevClr.TabIndex = 4;
            this.tbLivePrevClr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.livePreviewColorSlct);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "live preview clr";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.tbCursorPrevWidth);
            this.groupBox6.Controls.Add(this.tbLivePrevCursorWidth);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Location = new System.Drawing.Point(226, 364);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(188, 93);
            this.groupBox6.TabIndex = 9;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Cursor width px";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "preview ";
            // 
            // tbCursorPrevWidth
            // 
            this.tbCursorPrevWidth.Location = new System.Drawing.Point(89, 24);
            this.tbCursorPrevWidth.Name = "tbCursorPrevWidth";
            this.tbCursorPrevWidth.Size = new System.Drawing.Size(93, 20);
            this.tbCursorPrevWidth.TabIndex = 0;
            // 
            // tbLivePrevCursorWidth
            // 
            this.tbLivePrevCursorWidth.Location = new System.Drawing.Point(89, 58);
            this.tbLivePrevCursorWidth.Name = "tbLivePrevCursorWidth";
            this.tbLivePrevCursorWidth.Size = new System.Drawing.Size(93, 20);
            this.tbLivePrevCursorWidth.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "live preview ";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.chckMotorsOff);
            this.groupBox7.Controls.Add(this.chckLivePrev);
            this.groupBox7.Controls.Add(this.chckPos);
            this.groupBox7.Controls.Add(this.chckDebug);
            this.groupBox7.Location = new System.Drawing.Point(9, 571);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(402, 58);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "SetStuff";
            // 
            // chckMotorsOff
            // 
            this.chckMotorsOff.AutoSize = true;
            this.chckMotorsOff.Location = new System.Drawing.Point(294, 20);
            this.chckMotorsOff.Name = "chckMotorsOff";
            this.chckMotorsOff.Size = new System.Drawing.Size(80, 17);
            this.chckMotorsOff.TabIndex = 3;
            this.chckMotorsOff.Text = "motors OFF";
            this.chckMotorsOff.UseVisualStyleBackColor = true;
            // 
            // chckLivePrev
            // 
            this.chckLivePrev.AutoSize = true;
            this.chckLivePrev.Location = new System.Drawing.Point(181, 20);
            this.chckLivePrev.Name = "chckLivePrev";
            this.chckLivePrev.Size = new System.Drawing.Size(86, 17);
            this.chckLivePrev.TabIndex = 2;
            this.chckLivePrev.Text = "Live preview";
            this.chckLivePrev.UseVisualStyleBackColor = true;
            // 
            // chckPos
            // 
            this.chckPos.AutoSize = true;
            this.chckPos.Location = new System.Drawing.Point(92, 20);
            this.chckPos.Name = "chckPos";
            this.chckPos.Size = new System.Drawing.Size(63, 17);
            this.chckPos.TabIndex = 1;
            this.chckPos.Text = "Position";
            this.chckPos.UseVisualStyleBackColor = true;
            // 
            // chckDebug
            // 
            this.chckDebug.AutoSize = true;
            this.chckDebug.Location = new System.Drawing.Point(12, 20);
            this.chckDebug.Name = "chckDebug";
            this.chckDebug.Size = new System.Drawing.Size(58, 17);
            this.chckDebug.TabIndex = 0;
            this.chckDebug.Text = "Debug";
            this.chckDebug.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Controls.Add(this.tbBaudRate);
            this.groupBox8.Location = new System.Drawing.Point(9, 474);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(402, 91);
            this.groupBox8.TabIndex = 12;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Communication";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "baud rate:";
            // 
            // tbBaudRate
            // 
            this.tbBaudRate.Location = new System.Drawing.Point(70, 19);
            this.tbBaudRate.Name = "tbBaudRate";
            this.tbBaudRate.Size = new System.Drawing.Size(143, 20);
            this.tbBaudRate.TabIndex = 5;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.chckZshow);
            this.groupBox9.Controls.Add(this.label14);
            this.groupBox9.Controls.Add(this.tbZlimit);
            this.groupBox9.Controls.Add(this.tbZcolor);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Location = new System.Drawing.Point(12, 252);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(399, 93);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Z preview";
            // 
            // chckZshow
            // 
            this.chckZshow.AutoSize = true;
            this.chckZshow.Location = new System.Drawing.Point(211, 20);
            this.chckZshow.Name = "chckZshow";
            this.chckZshow.Size = new System.Drawing.Size(58, 17);
            this.chckZshow.TabIndex = 5;
            this.chckZshow.Text = "showZ";
            this.chckZshow.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(33, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "zLimit";
            // 
            // tbZlimit
            // 
            this.tbZlimit.Location = new System.Drawing.Point(89, 24);
            this.tbZlimit.Name = "tbZlimit";
            this.tbZlimit.Size = new System.Drawing.Size(93, 20);
            this.tbZlimit.TabIndex = 0;
            // 
            // tbZcolor
            // 
            this.tbZcolor.Location = new System.Drawing.Point(89, 58);
            this.tbZcolor.Name = "tbZcolor";
            this.tbZcolor.Size = new System.Drawing.Size(93, 20);
            this.tbZcolor.TabIndex = 4;
            this.tbZcolor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zColorSlct);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(36, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "zColor";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 711);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbXres;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbYres;
        private System.Windows.Forms.TextBox tbZres;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSpindleRPM;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPrevXmm;
        private System.Windows.Forms.TextBox tbPreviYmm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbFullPreviXmm;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbPrevClr;
        private System.Windows.Forms.TextBox tbLivePrevClr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbCursorPrevWidth;
        private System.Windows.Forms.TextBox tbLivePrevCursorWidth;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chckLivePrev;
        private System.Windows.Forms.CheckBox chckPos;
        private System.Windows.Forms.CheckBox chckDebug;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbBaudRate;
        private System.Windows.Forms.CheckBox chckMotorsOff;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox chckZshow;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbZlimit;
        private System.Windows.Forms.TextBox tbZcolor;
        private System.Windows.Forms.Label label15;
    }
}