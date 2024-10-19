using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ArduinoCNCPccontroller
{
    public partial class CNC_PC_controller : Form
    {
        String[] ports;
        SerialPort port;
    
        ArduinoCommunicatorV2 communicatorV2;
        bool connected = false;
        bool running = false;
        bool controlActive = true;
        String FilePath;

        public CNC_PC_controller()
        {
            InitializeComponent();
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

        private void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }
        bool isConnected = false;
        private void disableControls()
        {
            controlActive = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToControler();
            }
            else
            {
                disconnectFromControler();
                ConnectBtn.Text = "Connect";
            }
        }




        private void connectToControler()
        {
            if (isConnected)
            {
                return;
            }
           
                string selectedPort = PortsList.GetItemText(PortsList.SelectedItem);
                

                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One)
                {
                    NewLine = "\n"
                };

              
                communicatorV2 = new ArduinoCommunicatorV2(port,txtRBdebugConsole);




                bool openPort=communicatorV2.Connect();
                if (openPort) {
                    isConnected = true;


                ConnectBtn.Text = "Disconnect";
            
            } else
                {
                    isConnected = false;
                }



        
        }


        

        private void enableControls()
        {
            controlActive = true;
        }

        private void disconnectFromControler()
        {
            isConnected = false;
           
            controlActive = false;
            ConnectBtn.Text = "Connect";
            if (port != null && port.IsOpen)
            {
               communicatorV2.Disconnect();
            }
            disableControls();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
            communicatorV2.StopSpindle();
          
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

        private async void RunFile_Click(object sender, EventArgs e)
        {

            if (!communicatorV2.IsGcStreamRunning())
            {
                RunFile.BackColor = Color.Red;
                RunFile.Text = "STOP";
           

            }
            else { 
       
             
                RunFile.BackColor = Color.LightGreen;
                RunFile.Text = "Run";
                communicatorV2.StopGcStream();
                return;
            }


          
          
            if (!isConnected) { return; }
            disableControls();
         bool done=  await communicatorV2.StreamGcodeFileAsync(FilePath,0);
            if (done)
            {

                RunFile.BackColor = Color.LightGreen;
                RunFile.Text = "Run";
                enableControls();
            }
          
        }

        private void RunBtnStop() { }


        private void showErrorMsg(String msg) {
            MessageBox.Show(msg);
        }

       

        private void ShowMessageBox(string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { MessageBox.Show(message); }));
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

      
        private void SpindleOnCbtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
        }

      

        private void outputLbl_Click(object sender, EventArgs e)
        {

        }

        private void FilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void ZcalBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }

            communicatorV2.ZCal();


        }

      

        private void SendManulaBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
            
         

        }

  
   


        private void YforwardBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
            communicatorV2.MoveAxis('Y', CbSteps.SelectedItem.ToString(), CbFeedRate.SelectedItem.ToString());
        }

        private void YbackBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }

            communicatorV2.MoveAxis('Y', "-" + CbSteps.SelectedItem.ToString() , CbFeedRate.SelectedItem.ToString());
            

        }

        private void XrightBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }

            communicatorV2.MoveAxis('X', CbSteps.SelectedItem.ToString(), CbFeedRate.SelectedItem.ToString());
        }

        private void XleftBtn_Click(object sender, EventArgs e)
        {
            communicatorV2.MoveAxis('X', "-" + CbSteps.SelectedItem.ToString() , CbFeedRate.SelectedItem.ToString());

         
        }

        private void ZUP_Click(object sender, EventArgs e)
        {

            communicatorV2.MoveAxis('Z', CbSteps.SelectedItem.ToString(), CbFeedRate.SelectedItem.ToString());
        }

        private void ZDOWN_Click(object sender, EventArgs e)
        {

            communicatorV2.MoveAxis('Z', "-" + CbSteps.SelectedItem.ToString(), CbFeedRate.SelectedItem.ToString());
        }





        private void HomeBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }


            communicatorV2.Home();
        }

 
        private void SpindleOnBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
            float speed = ((float)( double.Parse(CbSpindleSpd.Text)/100) * 20000);
            communicatorV2.StartSpindle((int)speed);
            
        
        }

        private void SetOriginBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
            communicatorV2.Origin();
          
        }

        bool endstopsON = false;
        private void DisableEndstopsBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
           
        }

        private void label10_Click(object sender, EventArgs e)
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

        private void DisableSteppersBtn_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }

            communicatorV2.DisableSteppers();
        }

        private void OptionBtnP_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
        }

        private void OptionBtnB_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
        }

        private void RequestMD_Click(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
           
        
        }

        private void ResponseLblGcd_Click(object sender, EventArgs e)
        {

        }

        private void machineDataLBL_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!isConnected) { return; }
            communicatorV2.Center();
            
        }

     

      

        private void XpYpBtn_Click(object sender, EventArgs e)
        {
            communicatorV2.MoveDiagonal('X', 'Y', CbSteps.SelectedItem.ToString(), CbSteps.SelectedItem.ToString() , CbFeedRate.SelectedItem.ToString());
        }

        private void XnYpBtn_Click(object sender, EventArgs e)
        {
            communicatorV2.MoveDiagonal('X', 'Y', "-" + CbSteps.SelectedItem.ToString()  , CbSteps.SelectedItem.ToString()  , CbFeedRate.SelectedItem.ToString());
        }

        private void XnYnBtn_Click(object sender, EventArgs e) {
        
            communicatorV2.MoveDiagonal('X', 'Y', "-"+ CbSteps.SelectedItem.ToString() ,"-"+ CbSteps.SelectedItem.ToString(), CbFeedRate.SelectedItem.ToString());
        }
        private void XpYnBtn_Click(object sender, EventArgs e)
        {
            communicatorV2.MoveDiagonal('X', 'Y', CbSteps.SelectedItem.ToString() , "-" + CbSteps.SelectedItem.ToString(), (String)CbFeedRate.SelectedItem);
        }

        private void CbFeedRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CbSteps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }



}