using ArduinoCNCPccontroller.classes;
using ArduinoCNCPccontroller.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ArduinoCNCPccontroller
{
    public partial class CNC_PC_controller : Form
    {
        String[] ports;
        SerialPort port;


        Point cursor;
        Bitmap canvas;


        JsonModelManager modelManager;
        ArduinoComunicatorV3 communicatorV3;
        Feedback feedback;
        
        
        bool connected = false;
        bool running = false;
        bool lockControls = false;
        bool sequenceRunning=false;
        String FilePath;


        bool isConnected = false;
        public CNC_PC_controller()
        {

            InitializeComponent();

            cursor = new Point();
            cursor.X = previewDrawBoard.Width / 2;
            cursor.Y = previewDrawBoard.Height / 2;
            canvas = new Bitmap(previewDrawBoard.Width, previewDrawBoard.Height);

            modelManager = new JsonModelManager();
            feedback= new Feedback();
            communicatorV3 = new ArduinoComunicatorV3(null,this);


            RefreshData();

            txtRBdebugConsole.Text += "canvas width" + previewDrawBoard.Width + "\n";


            disableControls();
            getAvailableComPorts();
            foreach (string port in ports)
            {
                PortsList.Items.Add(port);
                if (ports[0] != null)
                {
                    PortsList.SelectedItem = ports[0];
                }
            }
        }

        public void LogLn(string text)
        {
            
            Console.WriteLine(text);




            text += Environment.NewLine;

            if (txtRBdebugConsole.InvokeRequired)
            {
                txtRBdebugConsole.Invoke(new Action(() => txtRBdebugConsole.AppendText(text)));
            }
            else
            {
                txtRBdebugConsole.AppendText(text);
            }
        }
       
        public void ShowError(string message)
        {
            MessageBox.Show(
          message,       
          "Error",                       
          MessageBoxButtons.OK,
          MessageBoxIcon.Error);

        }
        public void Inform(string message)
        {
            MessageBox.Show(
    message, // message
    "Information",                             // title
    MessageBoxButtons.OK,                       // just OK button
    MessageBoxIcon.Information                 // info icon
);
        }
        public bool ConfirmWindow(string message)
        {
            DialogResult result = MessageBox.Show(
         message,   // message
         "Confirmation",               // title
         MessageBoxButtons.YesNo,      // buttons
         MessageBoxIcon.Question       // icon
     );

            if (result == DialogResult.Yes)
            {
               return true;
            }
            else
            {
               return false;
            }
       
        
        }
       
        
        public void RefreshData()
        {
            modelManager.Load();
            string workspaceSizeLbl = "Workspace:";
            workspaceSizeLbl += modelManager.Settings.miniPreviewXmm;
            workspaceSizeLbl += "mm x ";
            workspaceSizeLbl += modelManager.Settings.miniPreviewYmm;
            workspaceSizeLbl += "mm";
            lblWorkspaceSize.Text = workspaceSizeLbl;

            string steps = "  XstepSize: ";
            steps += modelManager.Settings.x_resolution + "mm ";
            steps += "YstepSize: ";
            steps += modelManager.Settings.y_resolution + "mm ";

            lblXYstepSize.Text = steps;

            var draw = modelManager.Settings.z_draw;
          


        }

        private void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void disableControls()
        {
            lockControls = true;
        }
        private void connectBtn(object sender, EventArgs e)
        {
            LogLn(isConnected.ToString());
            if (!isConnected)
            {

                  connectToControler();
                enableControls();
                
          
            }
            else
            {

                port.Close();
                isConnected = false;
                disableControls();
                connectBtnTxtUpdate();
            }
        }

        private void connectBtnTxtUpdate()
        {
            if (isConnected)
            {
                ConnectBtn.Text = "Disconnect";
            }
            else
            {
                ConnectBtn.Text = "Connect";
            }
        }
        private async Task connectToControler()
        {
            LogLn("CONNECTING");

            string selectedPort = PortsList.GetItemText(PortsList.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One)
            {
                NewLine = "\n"
            };

            port.Open();

           
            await Task.Delay(1000);
           
           
            port.DiscardInBuffer();
            port.DiscardOutBuffer();

            communicatorV3 = new ArduinoComunicatorV3(port, this);

            
            isConnected = await communicatorV3.TestConnection();
            LogLn("connected"+isConnected);
            connectBtnTxtUpdate();

        }

        private void enableControls()
        {
            lockControls = false;
        }

 
    


        private void OpenFileD_Click(object sender, EventArgs e)
        {






            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "G-code files (*.gcode;*.nc)|*.gcode;*.nc|All files (*.*)|*.*";
            fileDialog.Title = "Select a G-code file";


            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FilePath = fileDialog.FileName;
                String name = Path.GetFileName(FilePath);
                FileNameLbl.Text = name;

            }
        }

        private void UpdateRunBtnStuff()
        {
            if (running)
            {
                RunFile.BackColor = Color.Red;
                RunFile.Text = "STOP";
              
            }
            else
            {
                RunFile.BackColor = Color.LightGreen;
                RunFile.Text = "Run";
                
            }
        }

        private async void RunFile_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                ShowError("NOT CONNECTED");
                return;
            }
            if (String.IsNullOrEmpty(FilePath))
            {
                ShowError("NO FILE  SELECTED");
                return;
            }
          
           

            if (running)
            {
                communicatorV3.StopGcodeStream = true;
                UpdateRunBtnStuff();
                running = false;
                lockControls = false;
                return;
            }
            LogLn("preping to run gcode : make sure machine is set and calibrated");
            if (!ConfirmWindow("Do you want run file: " + FileNameLbl.Text))
            {
                return;
            }
            running = true;
            lockControls = true;
            UpdateRunBtnStuff();
            bool done = await communicatorV3.RunGcode(FilePath, true, false, true, false);
            if (done)
            {
                running = false;
                if (communicatorV3.isErrorGcStream)
                {
                    ShowError(communicatorV3.gcStreamMsg);
                }
                else
                {
                    Inform(communicatorV3.gcStreamMsg);
                }
                    UpdateRunBtnStuff();
            }

        }


        private async void previewBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                ShowError("NOT CONNECTED");
                return;
            }
            if (String.IsNullOrEmpty(FilePath))
            {
                ShowError("NO FILE  SELECTED");
                return;
            }

            
     

            if (running)
            {
                communicatorV3.StopGcodeStream = true;
                UpdateRunBtnStuff();
                running = false;
                lockControls = false;
                return;
            }
            running = true;
            lockControls = true;
            UpdateRunBtnStuff();
            bool done = await communicatorV3.RunGcode(FilePath, true, false, false,true);
            if (done)
            {
                running = false;
                if (communicatorV3.isErrorGcStream)
                {
                    ShowError(communicatorV3.gcStreamMsg);
                }
                else
                {
                    Inform(communicatorV3.gcStreamMsg);
                    communicatorV3.resetCursor();
                }
                UpdateRunBtnStuff();
            }
        }


        private void RunBtnStop() { }


  
 
        private void Form1_Load(object sender, EventArgs e)
        {

        }


    
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            getAvailableComPorts();
            PortsList.Items.Clear();

            getAvailableComPorts();
            foreach (string port in ports)
            {
                PortsList.Items.Add(port);
                if (ports[0] != null)
                {
                    PortsList.SelectedItem = ports[0];
                }
            }
        }

        private void PortsList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



   
        // STEPPERS-------------------------------------------------------------------------------------------------
        private void DisableSteppersBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }


        }

    
        //-**************** Move BTNS----------------------------------------------------------------------
        private async Task PerfromMove(string axis)
        {

            if (!isConnected)
            {
                return;
            }
            if (lockControls)
            {
                return;
            }
            double mm = 0;
            double feedrate = 0;

            var mmCb = CbSteps.Text;
            var feedrateCb = CbFeedRate.Text;
            try
            {
                double.TryParse(mmCb, out mm);
                double.TryParse(feedrateCb, out feedrate);
            }
            catch (Exception ex)
            {
                return;
            }

            disableControls();
            await communicatorV3.PerformMove(axis, mm, feedrate);
            enableControls();
        }
        private void YforwardBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("Y");

        }
        private void YbackBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("-Y");




        }
        private void XrightBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("X");

        }
        private void XleftBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("-X");


        }
        private void ZUP_Click(object sender, EventArgs e)
        {
            PerfromMove("Z");

        }
        private void ZDOWN_Click(object sender, EventArgs e)
        {
            PerfromMove("-Z");

        }
        private void XpYpBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("XY");
        }
        private void XnYpBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("-XY");
        }
        private void XnYnBtn_Click(object sender, EventArgs e) {
            PerfromMove("-X-Y");

        }
        private void XpYnBtn_Click(object sender, EventArgs e)
        {
            PerfromMove("X-Y");
        }
        //-------------------------------------------------------------------------------------------------
        //------- SPINDLE ------------------------------------------------------------------
        private async Task SpindleChangeRpm()
        {
            if (!isConnected)
            {
                ShowError("not connected");
                return;
            }
            if (lockControls)
            {
                ShowError("Locked");
                return;
            }

            var pSpeedT = CbSpindleSpd.Text;
            int speed = 0;
            double speedP = 0;
            try
            {
                 speedP = Double.Parse(pSpeedT);
            }
            catch (Exception ex)
            {
                return;
            }
            speed = (int)(speedP / 0.01) * 20000;

            disableControls();
            await communicatorV3.SpindleSet(speed);
            enableControls();
        }
        private async Task spinOff()
        {
            if (!isConnected)
            {
                ShowError("not connected");
                return;
            }
            if (lockControls)
            {
                ShowError("Locked");
                return;
            }
            disableControls();
            await communicatorV3.StopSpindle();
            enableControls();
        }
    
        // btn events
        private void SpindleOnBtn_Click(object sender, EventArgs e)
        {

            SpindleChangeRpm();
        }
        private void SpindleOffCbtn_Click(object sender, EventArgs e)
        {
            spinOff();
        }

        // ROUTINES-------------------------------------------------------------------------------------------------
        private async Task HomeRoutine()
        {
            if (!isConnected) { ShowError("Not connected"); return; }
            if (lockControls) { ShowError("Locked"); return; }
            disableControls();
            sequenceRunning = true;
            await communicatorV3.HomeAsync();
            enableControls();


        }
        private async Task ZCalRoutine()
        {
            if (!isConnected) { ShowError("Not connected"); return; }
            if (lockControls) { ShowError("Locked"); return; }
            disableControls();
            sequenceRunning = true;
            await communicatorV3.ZcalAsync();
            enableControls();
        }
        private async Task CenterRoutine()
        {
            if (!isConnected) { ShowError("Not connected"); return; }
            if (lockControls) { ShowError("Locked"); return; }
            disableControls();
            sequenceRunning = true;
            await communicatorV3.CenterAsync();
            enableControls();

        }
        private void HomeBtn_Click(object sender, EventArgs e)
        {

            HomeRoutine();

        }
        private void SetOriginBtn_Click(object sender, EventArgs e)
        {
            if (lockControls)
            {
                return;
            }
            if (!isConnected)
            {
                return;
            }
            communicatorV3.SetOrigin();


        }
        private void ZcalBtn_Click(object sender, EventArgs e)
        {
            ZCalRoutine();

        }
        private void CenterBtn_Click(object sender, EventArgs e)
        {
            CenterRoutine();


        }

       //-------------------------------------------------------------------------------------------------
    
        public void drawLine(Point pointA, Point pointB, Color color, int width)
        {


            if (previewDrawBoard.InvokeRequired)
            {

                previewDrawBoard.BeginInvoke((MethodInvoker)(() =>
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                    {
                        g.DrawLine(new Pen(color, width), pointA, pointB);
                    }
                    previewDrawBoard.Image = canvas;
                    previewDrawBoard.Invalidate();

                }));
                return;

            } else

                using (Graphics g = Graphics.FromImage(canvas))
                {
                    g.DrawLine(new Pen(color, width), pointA, pointB);
                }
            previewDrawBoard.Invalidate();
        
        }
        public void ClearCanvas()
        {
            if (previewDrawBoard.InvokeRequired)
            {
                previewDrawBoard.Invoke((MethodInvoker)(() => ClearCanvas()));
                return;
            }

            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }

            previewDrawBoard.Invalidate();
            cursor = new Point(previewDrawBoard.Width / 2, previewDrawBoard.Height / 2);
        }
        public int getCanvasWidth()
        {
            return canvas.Width;
        }
        public int getCanvasHeight()
        {
             return canvas.Height; }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        
    }



}