namespace ArduinoCNCPccontroller
{
    partial class CNC_PC_controller
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
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.PortsList = new System.Windows.Forms.ComboBox();
            this.YforwardBtn = new System.Windows.Forms.Button();
            this.XleftBtn = new System.Windows.Forms.Button();
            this.YbackBtn = new System.Windows.Forms.Button();
            this.XrightBtn = new System.Windows.Forms.Button();
            this.ZUP = new System.Windows.Forms.Button();
            this.ZDOWN = new System.Windows.Forms.Button();
            this.SpindleOnBtn = new System.Windows.Forms.Button();
            this.SpindleOffBtn = new System.Windows.Forms.Button();
            this.OpenFileD = new System.Windows.Forms.Button();
            this.RunFile = new System.Windows.Forms.Button();
            this.HomeBtn = new System.Windows.Forms.Button();
            this.ZcalBtn = new System.Windows.Forms.Button();
            this.CbSteps = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPosSwitch = new System.Windows.Forms.Button();
            this.btnMovSwitch = new System.Windows.Forms.Button();
            this.btnDebugSwitch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CbFeedRate = new System.Windows.Forms.ComboBox();
            this.XnYnBtn = new System.Windows.Forms.Button();
            this.XpYnBtn = new System.Windows.Forms.Button();
            this.CbSpindleSpd = new System.Windows.Forms.ComboBox();
            this.XpYpBtn = new System.Windows.Forms.Button();
            this.XnYpBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DisableSteppersBtn = new System.Windows.Forms.Button();
            this.SetOriginBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnFullPreview = new System.Windows.Forms.Button();
            this.FileNameLbl = new System.Windows.Forms.Label();
            this.txtRBdebugConsole = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblXpos = new System.Windows.Forms.Label();
            this.lblZpos = new System.Windows.Forms.Label();
            this.lblSpin = new System.Windows.Forms.Label();
            this.lblYpos = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.previewDrawBoard = new System.Windows.Forms.PictureBox();
            this.lblXYstepSize = new System.Windows.Forms.Label();
            this.lblWorkspaceSize = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewDrawBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Location = new System.Drawing.Point(3, 67);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(324, 38);
            this.ConnectBtn.TabIndex = 0;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.connectBtn);
            // 
            // PortsList
            // 
            this.PortsList.FormattingEnabled = true;
            this.PortsList.Location = new System.Drawing.Point(6, 37);
            this.PortsList.Name = "PortsList";
            this.PortsList.Size = new System.Drawing.Size(261, 21);
            this.PortsList.TabIndex = 1;
            this.PortsList.SelectedIndexChanged += new System.EventHandler(this.PortsList_SelectedIndexChanged);
            // 
            // YforwardBtn
            // 
            this.YforwardBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.YforwardBtn.Location = new System.Drawing.Point(116, 197);
            this.YforwardBtn.Name = "YforwardBtn";
            this.YforwardBtn.Size = new System.Drawing.Size(70, 65);
            this.YforwardBtn.TabIndex = 2;
            this.YforwardBtn.Text = "Y+";
            this.YforwardBtn.UseVisualStyleBackColor = true;
            this.YforwardBtn.Click += new System.EventHandler(this.YforwardBtn_Click);
            // 
            // XleftBtn
            // 
            this.XleftBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XleftBtn.Location = new System.Drawing.Point(27, 116);
            this.XleftBtn.Name = "XleftBtn";
            this.XleftBtn.Size = new System.Drawing.Size(70, 68);
            this.XleftBtn.TabIndex = 2;
            this.XleftBtn.Text = "X-";
            this.XleftBtn.UseVisualStyleBackColor = true;
            this.XleftBtn.Click += new System.EventHandler(this.XleftBtn_Click);
            // 
            // YbackBtn
            // 
            this.YbackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.YbackBtn.Location = new System.Drawing.Point(113, 36);
            this.YbackBtn.Name = "YbackBtn";
            this.YbackBtn.Size = new System.Drawing.Size(70, 63);
            this.YbackBtn.TabIndex = 2;
            this.YbackBtn.Text = "Y-";
            this.YbackBtn.UseVisualStyleBackColor = true;
            this.YbackBtn.Click += new System.EventHandler(this.YbackBtn_Click);
            // 
            // XrightBtn
            // 
            this.XrightBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XrightBtn.Location = new System.Drawing.Point(209, 116);
            this.XrightBtn.Name = "XrightBtn";
            this.XrightBtn.Size = new System.Drawing.Size(70, 68);
            this.XrightBtn.TabIndex = 3;
            this.XrightBtn.Text = "X+";
            this.XrightBtn.UseVisualStyleBackColor = true;
            this.XrightBtn.Click += new System.EventHandler(this.XrightBtn_Click);
            // 
            // ZUP
            // 
            this.ZUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZUP.Location = new System.Drawing.Point(296, 37);
            this.ZUP.Name = "ZUP";
            this.ZUP.Size = new System.Drawing.Size(77, 107);
            this.ZUP.TabIndex = 4;
            this.ZUP.Text = "Z+";
            this.ZUP.UseVisualStyleBackColor = true;
            this.ZUP.Click += new System.EventHandler(this.ZUP_Click);
            // 
            // ZDOWN
            // 
            this.ZDOWN.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZDOWN.Location = new System.Drawing.Point(296, 150);
            this.ZDOWN.Name = "ZDOWN";
            this.ZDOWN.Size = new System.Drawing.Size(77, 112);
            this.ZDOWN.TabIndex = 5;
            this.ZDOWN.Text = "Z-";
            this.ZDOWN.UseVisualStyleBackColor = true;
            this.ZDOWN.Click += new System.EventHandler(this.ZDOWN_Click);
            // 
            // SpindleOnBtn
            // 
            this.SpindleOnBtn.Location = new System.Drawing.Point(211, 275);
            this.SpindleOnBtn.Name = "SpindleOnBtn";
            this.SpindleOnBtn.Size = new System.Drawing.Size(70, 54);
            this.SpindleOnBtn.TabIndex = 6;
            this.SpindleOnBtn.Text = "Spindle On";
            this.SpindleOnBtn.UseVisualStyleBackColor = true;
            this.SpindleOnBtn.Click += new System.EventHandler(this.SpindleOnBtn_Click);
            // 
            // SpindleOffBtn
            // 
            this.SpindleOffBtn.Location = new System.Drawing.Point(29, 275);
            this.SpindleOffBtn.Name = "SpindleOffBtn";
            this.SpindleOffBtn.Size = new System.Drawing.Size(70, 54);
            this.SpindleOffBtn.TabIndex = 7;
            this.SpindleOffBtn.Text = "Spindle Off";
            this.SpindleOffBtn.UseVisualStyleBackColor = true;
            this.SpindleOffBtn.Click += new System.EventHandler(this.SpindleOffCbtn_Click);
            // 
            // OpenFileD
            // 
            this.OpenFileD.Location = new System.Drawing.Point(0, 24);
            this.OpenFileD.Name = "OpenFileD";
            this.OpenFileD.Size = new System.Drawing.Size(330, 39);
            this.OpenFileD.TabIndex = 9;
            this.OpenFileD.Text = "Select";
            this.OpenFileD.UseVisualStyleBackColor = true;
            this.OpenFileD.Click += new System.EventHandler(this.OpenFileD_Click);
            // 
            // RunFile
            // 
            this.RunFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.RunFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RunFile.Location = new System.Drawing.Point(6, 134);
            this.RunFile.Name = "RunFile";
            this.RunFile.Size = new System.Drawing.Size(321, 61);
            this.RunFile.TabIndex = 10;
            this.RunFile.TabStop = false;
            this.RunFile.Text = "Run";
            this.RunFile.UseVisualStyleBackColor = false;
            this.RunFile.Click += new System.EventHandler(this.RunFile_Click);
            // 
            // HomeBtn
            // 
            this.HomeBtn.BackColor = System.Drawing.Color.Gainsboro;
            this.HomeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.HomeBtn.Location = new System.Drawing.Point(400, 19);
            this.HomeBtn.Name = "HomeBtn";
            this.HomeBtn.Size = new System.Drawing.Size(107, 58);
            this.HomeBtn.TabIndex = 15;
            this.HomeBtn.Text = "HOME";
            this.HomeBtn.UseVisualStyleBackColor = false;
            this.HomeBtn.Click += new System.EventHandler(this.HomeBtn_Click);
            // 
            // ZcalBtn
            // 
            this.ZcalBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ZcalBtn.Location = new System.Drawing.Point(400, 150);
            this.ZcalBtn.Name = "ZcalBtn";
            this.ZcalBtn.Size = new System.Drawing.Size(107, 49);
            this.ZcalBtn.TabIndex = 16;
            this.ZcalBtn.Text = "Z CAL";
            this.ZcalBtn.UseVisualStyleBackColor = true;
            this.ZcalBtn.Click += new System.EventHandler(this.ZcalBtn_Click);
            // 
            // CbSteps
            // 
            this.CbSteps.FormattingEnabled = true;
            this.CbSteps.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "25",
            "50",
            "100",
            "200",
            "250",
            "500",
            "1000"});
            this.CbSteps.Location = new System.Drawing.Point(113, 123);
            this.CbSteps.Name = "CbSteps";
            this.CbSteps.Size = new System.Drawing.Size(68, 21);
            this.CbSteps.TabIndex = 17;
            this.CbSteps.Text = "1";
        
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "move mm";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPosSwitch);
            this.groupBox1.Controls.Add(this.btnMovSwitch);
            this.groupBox1.Controls.Add(this.btnDebugSwitch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CbFeedRate);
            this.groupBox1.Controls.Add(this.XnYnBtn);
            this.groupBox1.Controls.Add(this.XpYnBtn);
            this.groupBox1.Controls.Add(this.CbSpindleSpd);
            this.groupBox1.Controls.Add(this.SpindleOffBtn);
            this.groupBox1.Controls.Add(this.SpindleOnBtn);
            this.groupBox1.Controls.Add(this.XpYpBtn);
            this.groupBox1.Controls.Add(this.XnYpBtn);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DisableSteppersBtn);
            this.groupBox1.Controls.Add(this.SetOriginBtn);
            this.groupBox1.Controls.Add(this.CbSteps);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.HomeBtn);
            this.groupBox1.Controls.Add(this.YbackBtn);
            this.groupBox1.Controls.Add(this.ZcalBtn);
            this.groupBox1.Controls.Add(this.XleftBtn);
            this.groupBox1.Controls.Add(this.YforwardBtn);
            this.groupBox1.Controls.Add(this.XrightBtn);
            this.groupBox1.Controls.Add(this.ZUP);
            this.groupBox1.Controls.Add(this.ZDOWN);
            this.groupBox1.Location = new System.Drawing.Point(356, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 408);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
            // 
            // btnPosSwitch
            // 
            this.btnPosSwitch.Location = new System.Drawing.Point(176, 360);
            this.btnPosSwitch.Name = "btnPosSwitch";
            this.btnPosSwitch.Size = new System.Drawing.Size(75, 42);
            this.btnPosSwitch.TabIndex = 46;
            this.btnPosSwitch.Text = "Position ON";
            this.btnPosSwitch.UseVisualStyleBackColor = true;
            // 
            // btnMovSwitch
            // 
            this.btnMovSwitch.Location = new System.Drawing.Point(81, 360);
            this.btnMovSwitch.Name = "btnMovSwitch";
            this.btnMovSwitch.Size = new System.Drawing.Size(89, 42);
            this.btnMovSwitch.TabIndex = 45;
            this.btnMovSwitch.Text = "Movement OFF";
            this.btnMovSwitch.UseVisualStyleBackColor = true;
            // 
            // btnDebugSwitch
            // 
            this.btnDebugSwitch.Location = new System.Drawing.Point(0, 360);
            this.btnDebugSwitch.Name = "btnDebugSwitch";
            this.btnDebugSwitch.Size = new System.Drawing.Size(75, 42);
            this.btnDebugSwitch.TabIndex = 44;
            this.btnDebugSwitch.Text = "Debug ON";
            this.btnDebugSwitch.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Feedrate:";
            // 
            // CbFeedRate
            // 
            this.CbFeedRate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CbFeedRate.FormattingEnabled = true;
            this.CbFeedRate.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "150",
            "100",
            "300",
            "600",
            "1200"});
            this.CbFeedRate.Location = new System.Drawing.Point(116, 163);
            this.CbFeedRate.Name = "CbFeedRate";
            this.CbFeedRate.Size = new System.Drawing.Size(65, 21);
            this.CbFeedRate.TabIndex = 33;
            this.CbFeedRate.Text = "100";
          
            // 
            // XnYnBtn
            // 
            this.XnYnBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XnYnBtn.Location = new System.Drawing.Point(27, 37);
            this.XnYnBtn.Name = "XnYnBtn";
            this.XnYnBtn.Size = new System.Drawing.Size(70, 62);
            this.XnYnBtn.TabIndex = 32;
            this.XnYnBtn.UseVisualStyleBackColor = true;
            this.XnYnBtn.Click += new System.EventHandler(this.XnYnBtn_Click);
            // 
            // XpYnBtn
            // 
            this.XpYnBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XpYnBtn.Location = new System.Drawing.Point(209, 37);
            this.XpYnBtn.Name = "XpYnBtn";
            this.XpYnBtn.Size = new System.Drawing.Size(70, 65);
            this.XpYnBtn.TabIndex = 31;
            this.XpYnBtn.UseVisualStyleBackColor = true;
            this.XpYnBtn.Click += new System.EventHandler(this.XpYnBtn_Click);
            // 
            // CbSpindleSpd
            // 
            this.CbSpindleSpd.FormattingEnabled = true;
            this.CbSpindleSpd.Items.AddRange(new object[] {
            "100",
            "75",
            "50",
            "25",
            "0"});
            this.CbSpindleSpd.Location = new System.Drawing.Point(105, 293);
            this.CbSpindleSpd.Name = "CbSpindleSpd";
            this.CbSpindleSpd.Size = new System.Drawing.Size(100, 21);
            this.CbSpindleSpd.TabIndex = 19;
            this.CbSpindleSpd.Text = "100";
        
            // 
            // XpYpBtn
            // 
            this.XpYpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XpYpBtn.Location = new System.Drawing.Point(209, 195);
            this.XpYpBtn.Name = "XpYpBtn";
            this.XpYpBtn.Size = new System.Drawing.Size(70, 67);
            this.XpYpBtn.TabIndex = 30;
            this.XpYpBtn.UseVisualStyleBackColor = true;
            this.XpYpBtn.Click += new System.EventHandler(this.XpYpBtn_Click);
            // 
            // XnYpBtn
            // 
            this.XnYpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.XnYpBtn.Location = new System.Drawing.Point(27, 197);
            this.XnYpBtn.Name = "XnYpBtn";
            this.XnYpBtn.Size = new System.Drawing.Size(72, 65);
            this.XnYpBtn.TabIndex = 29;
            this.XnYpBtn.UseVisualStyleBackColor = true;
            this.XnYpBtn.Click += new System.EventHandler(this.XnYpBtn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SkyBlue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(400, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 51);
            this.button1.TabIndex = 28;
            this.button1.Text = "CENTER";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.CenterBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Spindle % clockwise";
            // 
            // DisableSteppersBtn
            // 
            this.DisableSteppersBtn.Location = new System.Drawing.Point(400, 303);
            this.DisableSteppersBtn.Name = "DisableSteppersBtn";
            this.DisableSteppersBtn.Size = new System.Drawing.Size(107, 70);
            this.DisableSteppersBtn.TabIndex = 23;
            this.DisableSteppersBtn.Text = "Disable steppers";
            this.DisableSteppersBtn.UseVisualStyleBackColor = true;
            this.DisableSteppersBtn.Click += new System.EventHandler(this.DisableSteppersBtn_Click);
            // 
            // SetOriginBtn
            // 
            this.SetOriginBtn.Location = new System.Drawing.Point(400, 205);
            this.SetOriginBtn.Name = "SetOriginBtn";
            this.SetOriginBtn.Size = new System.Drawing.Size(107, 57);
            this.SetOriginBtn.TabIndex = 27;
            this.SetOriginBtn.Text = "Set Origin";
            this.SetOriginBtn.UseVisualStyleBackColor = true;
            this.SetOriginBtn.Click += new System.EventHandler(this.SetOriginBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Run file:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RefreshBtn);
            this.groupBox2.Controls.Add(this.ConnectBtn);
            this.groupBox2.Controls.Add(this.PortsList);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(338, 120);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection";
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(273, 36);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(54, 21);
            this.RefreshBtn.TabIndex = 34;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPreview);
            this.groupBox3.Controls.Add(this.btnFullPreview);
            this.groupBox3.Controls.Add(this.FileNameLbl);
            this.groupBox3.Controls.Add(this.OpenFileD);
            this.groupBox3.Controls.Add(this.RunFile);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(338, 352);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Run G-code";
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPreview.Location = new System.Drawing.Point(6, 201);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(321, 39);
            this.btnPreview.TabIndex = 28;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.previewBtn_Click);
            // 
            // btnFullPreview
            // 
            this.btnFullPreview.Location = new System.Drawing.Point(6, 304);
            this.btnFullPreview.Name = "btnFullPreview";
            this.btnFullPreview.Size = new System.Drawing.Size(326, 36);
            this.btnFullPreview.TabIndex = 46;
            this.btnFullPreview.Text = "Full Preview";
            this.btnFullPreview.UseVisualStyleBackColor = true;
            // 
            // FileNameLbl
            // 
            this.FileNameLbl.AutoSize = true;
            this.FileNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FileNameLbl.Location = new System.Drawing.Point(6, 103);
            this.FileNameLbl.Name = "FileNameLbl";
            this.FileNameLbl.Size = new System.Drawing.Size(118, 25);
            this.FileNameLbl.TabIndex = 27;
            this.FileNameLbl.Text = "FileName:";
            // 
            // txtRBdebugConsole
            // 
            this.txtRBdebugConsole.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtRBdebugConsole.Location = new System.Drawing.Point(12, 541);
            this.txtRBdebugConsole.Name = "txtRBdebugConsole";
            this.txtRBdebugConsole.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtRBdebugConsole.Size = new System.Drawing.Size(864, 340);
            this.txtRBdebugConsole.TabIndex = 31;
            this.txtRBdebugConsole.Text = "";
            this.txtRBdebugConsole.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Spin";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 34;
            this.label8.Text = "Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(264, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 35;
            this.label9.Text = "Z";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblXpos);
            this.groupBox4.Controls.Add(this.lblZpos);
            this.groupBox4.Controls.Add(this.lblSpin);
            this.groupBox4.Controls.Add(this.lblYpos);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(356, 455);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(525, 64);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Machine Data:";
            // 
            // lblXpos
            // 
            this.lblXpos.AutoSize = true;
            this.lblXpos.Location = new System.Drawing.Point(24, 42);
            this.lblXpos.Name = "lblXpos";
            this.lblXpos.Size = new System.Drawing.Size(14, 13);
            this.lblXpos.TabIndex = 36;
            this.lblXpos.Text = "X";
            // 
            // lblZpos
            // 
            this.lblZpos.AutoSize = true;
            this.lblZpos.Location = new System.Drawing.Point(264, 42);
            this.lblZpos.Name = "lblZpos";
            this.lblZpos.Size = new System.Drawing.Size(14, 13);
            this.lblZpos.TabIndex = 39;
            this.lblZpos.Text = "Z";
            // 
            // lblSpin
            // 
            this.lblSpin.AutoSize = true;
            this.lblSpin.Location = new System.Drawing.Point(409, 42);
            this.lblSpin.Name = "lblSpin";
            this.lblSpin.Size = new System.Drawing.Size(28, 13);
            this.lblSpin.TabIndex = 37;
            this.lblSpin.Text = "Spin";
            // 
            // lblYpos
            // 
            this.lblYpos.AutoSize = true;
            this.lblYpos.Location = new System.Drawing.Point(136, 42);
            this.lblYpos.Name = "lblYpos";
            this.lblYpos.Size = new System.Drawing.Size(14, 13);
            this.lblYpos.TabIndex = 38;
            this.lblYpos.Text = "Y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 525);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 41;
            this.label11.Text = "Debug";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.previewDrawBoard);
            this.groupBox5.Controls.Add(this.lblXYstepSize);
            this.groupBox5.Controls.Add(this.lblWorkspaceSize);
            this.groupBox5.Location = new System.Drawing.Point(887, 42);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(446, 839);
            this.groupBox5.TabIndex = 42;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Preview";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 452);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 13);
            this.label13.TabIndex = 47;
            this.label13.Text = "Preview Z below";
            // 
            // previewDrawBoard
            // 
            this.previewDrawBoard.Location = new System.Drawing.Point(6, 37);
            this.previewDrawBoard.Name = "previewDrawBoard";
            this.previewDrawBoard.Size = new System.Drawing.Size(400, 400);
            this.previewDrawBoard.TabIndex = 45;
            this.previewDrawBoard.TabStop = false;
            // 
            // lblXYstepSize
            // 
            this.lblXYstepSize.AutoSize = true;
            this.lblXYstepSize.Location = new System.Drawing.Point(177, 20);
            this.lblXYstepSize.Name = "lblXYstepSize";
            this.lblXYstepSize.Size = new System.Drawing.Size(156, 13);
            this.lblXYstepSize.TabIndex = 44;
            this.lblXYstepSize.Text = "Xstep:0.025mm Ystep:0.025mm";
            // 
            // lblWorkspaceSize
            // 
            this.lblWorkspaceSize.AutoSize = true;
            this.lblWorkspaceSize.Location = new System.Drawing.Point(7, 20);
            this.lblWorkspaceSize.Name = "lblWorkspaceSize";
            this.lblWorkspaceSize.Size = new System.Drawing.Size(125, 13);
            this.lblWorkspaceSize.TabIndex = 43;
            this.lblWorkspaceSize.Text = "Workspace: 100x100mm";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 43;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // CNC_PC_controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 893);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtRBdebugConsole);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "CNC_PC_controller";
            this.Text = "CNC_PC_controller";
            this.Load += new System.EventHandler(this.Form1_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.previewDrawBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.ComboBox PortsList;
        private System.Windows.Forms.Button YforwardBtn;
        private System.Windows.Forms.Button XleftBtn;
        private System.Windows.Forms.Button YbackBtn;
        private System.Windows.Forms.Button XrightBtn;
        private System.Windows.Forms.Button ZUP;
        private System.Windows.Forms.Button ZDOWN;
        private System.Windows.Forms.Button SpindleOnBtn;
        private System.Windows.Forms.Button SpindleOffBtn;
        private System.Windows.Forms.Button OpenFileD;
        private System.Windows.Forms.Button RunFile;
        private System.Windows.Forms.Button HomeBtn;
        private System.Windows.Forms.Button ZcalBtn;
        private System.Windows.Forms.ComboBox CbSteps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CbSpindleSpd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button DisableSteppersBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button SetOriginBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button XnYnBtn;
        private System.Windows.Forms.Button XpYnBtn;
        private System.Windows.Forms.Button XpYpBtn;
        private System.Windows.Forms.Button XnYpBtn;
        private System.Windows.Forms.ComboBox CbFeedRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label FileNameLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblWorkspaceSize;
        private System.Windows.Forms.Label lblXYstepSize;
        private System.Windows.Forms.Button btnFullPreview;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSettings;
        public System.Windows.Forms.RichTextBox txtRBdebugConsole;
        public System.Windows.Forms.Label lblXpos;
        public System.Windows.Forms.Label lblZpos;
        public System.Windows.Forms.Label lblSpin;
        public System.Windows.Forms.Label lblYpos;
        public System.Windows.Forms.PictureBox previewDrawBoard;
        private System.Windows.Forms.Button btnPosSwitch;
        private System.Windows.Forms.Button btnMovSwitch;
        private System.Windows.Forms.Button btnDebugSwitch;
    }
}

