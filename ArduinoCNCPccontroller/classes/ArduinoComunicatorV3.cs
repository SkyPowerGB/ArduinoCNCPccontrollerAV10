using ArduinoCNCPccontroller.Enums;
using ArduinoCNCPccontroller.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller.classes
{
    public class ArduinoComunicatorV3
    {
        private SerialPort port;
       private CNC_PC_controller controllerForm;
        private JsonModelManager modelManager;
        private FullPreview FullPreview;

        public ArduinoComunicatorV3(SerialPort port, CNC_PC_controller controllerForm)
        {
            this.port = port;
            this.controllerForm = controllerForm;
              modelManager=new JsonModelManager();
        }
        public void LogLn(string text)
        {
            Console.WriteLine(text);


            

                text += Environment.NewLine;

                if (controllerForm.txtRBdebugConsole.InvokeRequired)
                {
                controllerForm.txtRBdebugConsole.Invoke(new Action(() => controllerForm.txtRBdebugConsole.AppendText(text)));
                }
                else
                {
                controllerForm.txtRBdebugConsole.AppendText(text);
                }
            
        }


        public async Task<bool> ConnectAsync(int timeoutMs=5000 )
        {
           return await PerformComm(CommandList.startUsbCommunication,CommandList.usbCommBeginReply,timeoutMs);
        }


        public async Task<bool> PerformComm(string cmd, string response, int timeoutMs = 5000)
        {

            if (!port.IsOpen)
                port.Open();

            LogLn("Sending: "+cmd);

            var tcs = new TaskCompletionSource<bool>();

            void Handler(object sender, SerialDataReceivedEventArgs e)
            {
                while (port.BytesToRead > 0)
                {
                    string line = port.ReadLine().Trim();

                    if (line.Contains(response))
                    {
                        LogLn("Desired response Aquired");
                        tcs.TrySetResult(true);
                    }
                    else
                    {

                        if (line.StartsWith("[D:"))
                            LogLn(line.Substring(3));

                        else if (line.StartsWith("[P:"))
                        {
                            Task.Run(()=>ShowPosition(line));
                        }else if (line.StartsWith("[W:")) {

                            Task.Run(() => DrawPosition(line));

                        }
                    }
                }
            }

            port.DataReceived += Handler;

            try
            {
                port.WriteLine(cmd);


                var task = tcs.Task;
                if (await Task.WhenAny(task, Task.Delay(timeoutMs)) == task)
                {
                    return task.Result;
                }
                else
                {
                    LogLn(" timeout!");
                    return false;
                }
            }
            finally
            {
                port.DataReceived -= Handler;
            }





        }

        
        public async Task ShowPosition(string cmd)
        {
            double X = 0, Y = 0, Z = 0, S = 0;


            string s = cmd;
            if (s.StartsWith("[P:")) s = s.Substring(3);
            if (s.EndsWith("]")) s = s.Substring(0, s.Length - 1);

        
            var elements = s.Split(';');

            foreach (var element in elements)
            {
                var data = element.Split(':');
                if (data.Length != 2) continue;

                string key = data[0].Trim();
                string value = data[1].Trim();

                switch (key)
                {
                    case "X": double.TryParse(value, out X); break;
                    case "Y": double.TryParse(value, out Y); break;
                    case "Z": double.TryParse(value, out Z); break;
                    case "S": double.TryParse(value, out S); break;
                }
            }

         
            controllerForm.Invoke(new Action(() =>
            {
                controllerForm.lblXpos.Text = X.ToString();
                controllerForm.lblYpos.Text = Y.ToString();
                controllerForm.lblZpos.Text = Z.ToString();

            }));
        }

        public string gcodePack(string line)
        {
            string output = "$<";
            output += line + ">#";
            return output;
        }

        private double boardXmm=0;
        private double boardYmm=0;

        private double fullBPxMM = 0;
        private double fullBPyMM = 0;

        public async Task<bool> RunGcode(string filePath,bool preview = false,bool debug=false,bool position=true,bool movementOff=false)
        {

            if (debug)
            {
              await  PerformComm(CommandList.activateDebug,CommandList.ok);
            }
            else
            {
                await PerformComm(CommandList.deactivateDebug, CommandList.ok);
            }
            if (position)
            {
                await PerformComm(CommandList.posOn,CommandList.ok);
            }
            else
            {
                await PerformComm(CommandList.posOff, CommandList.ok);
            }
            if (preview)
            {
                await PerformComm(CommandList.drawOn, CommandList.ok);
                boardXmm = controllerForm.getCanvasWidth()/ modelManager.Settings.machineWorkspaceMMX ;
                boardYmm=controllerForm.getCanvasHeight()/ modelManager.Settings.machineWorkspaceMMY;
                controllerForm.ClearCanvas();
            }
            else
            {
                await PerformComm(CommandList.drawOff, CommandList.ok);
            }
            if (movementOff)
            {
                await PerformComm(CommandList.deactivateMotors, CommandList.ok);
            }
            else
            {
                await PerformComm(CommandList.activateMotors, CommandList.ok);
            }









                return true;
        }

   
        public async Task DrawPosition(string cmd)
        {
            double X = 0, Y = 0, Z = 0;


            string s = cmd;
            if (s.StartsWith("[W:")) s = s.Substring(3);
            if (s.EndsWith("]")) s = s.Substring(0, s.Length - 1);


            var elements = s.Split(';');

            foreach (var element in elements)
            {
                var data = element.Split(':');
                if (data.Length != 2) continue;

                string key = data[0].Trim();
                string value = data[1].Trim();

                switch (key)
                {
                    case "X": double.TryParse(value, out X); break;
                    case "Y": double.TryParse(value, out Y); break;
                    case "Z": double.TryParse(value, out Z); break;
                  
                }
            }


            Point P = new Point();

            P.X = (int)(X * modelManager.Settings.x_resolution * boardXmm);
            P.Y=(int)(Y*modelManager.Settings.y_resolution * boardYmm);

            controllerForm.drawLine(P, modelManager.Settings.lineColor, modelManager.Settings.lineSize);


        }


    }
}
