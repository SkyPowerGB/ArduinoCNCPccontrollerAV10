using ArduinoCNCPccontroller.Enums;
using ArduinoCNCPccontroller.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ArduinoCNCPccontroller.classes
{

    //BUILT FOR ARDUINO CNC SOFTWARE V0.0.12U AND ABOVE 
    public class ArduinoComunicatorV3
    {
        private SerialPort port;
        private CNC_PC_controller controllerForm;
        private JsonModelManager modelManager;
        private FullPreview FullPreview;

        public ArduinoComunicatorV3(SerialPort port, CNC_PC_controller controllerForm)
        {
            if(port == null) {
                return;
            }
            this.port = port;
            port.DataReceived += SerialHandler;
            this.controllerForm = controllerForm;
            modelManager = new JsonModelManager();
            if (!port.IsOpen)
            {
                port.Open();
            }
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


        public bool StopGcodeStream { get; set; } = false;

        // BASE FUNCTION 
        bool stopRecived = false;


        public async Task<string> SendCommandAsync(
         string command,
         string expect = "",
         string negResponse = "",
         int timeoutMs = 1000)
        {
            expectedResponse = expect;
            negativeResponse = negResponse;
            responseReceived = false;
            responseResult = "";

            if (!port.IsOpen) port.Open();
          
            port.WriteLine(command);

            bool signaled = await Task.Run(() => responseSignal.WaitOne(timeoutMs));


            expectedResponse = "";
            negativeResponse = "";

            if (!signaled)
            {
                LogLn($"Timeout for command: {command}");
                return null;
            }
            LogLn("got: "+responseResult);

            return responseResult;
        }




        private TaskCompletionSource<string> lastReplyTcs = null;
        private string expectedReply = null;
        private AutoResetEvent nextSignal = new AutoResetEvent(false);
        // Flagovi za prekid / negativni odgovor
        private string lastReceivedLine = "";
        private string expectedResponse = "";
        private string negativeResponse = "";
        private bool responseReceived = false;
        private string responseResult = "";


        private AutoResetEvent responseSignal = new AutoResetEvent(false);
        private StringBuilder serialBuffer = new StringBuilder();
        private void SerialHandler(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int bytes = port.BytesToRead;
                if (bytes == 0) return;

                byte[] buffer = new byte[bytes];
                port.Read(buffer, 0, bytes);
                string data = Encoding.ASCII.GetString(buffer);
                serialBuffer.Append(data);

                // Provjeri kompletne linije (\n)
                string content = serialBuffer.ToString();

                int newLine;
                while ((newLine = content.IndexOf('\n')) >= 0)
                {
                    string line = content.Substring(0, newLine).Trim();
                    content = content.Substring(newLine + 1);


                    HandleLine(line);
                }

                serialBuffer.Clear();
                serialBuffer.Append(content);
            }
            catch (IOException)
            {
                LogLn("IO EXCEPTION");
            }
            catch (Exception ex)
            {
                LogLn("SerialHandler Exception: " + ex.Message);
            }
        }

        private void HandleLine(string line)
        {
            LogLn("recived response: " + line);

            if (string.IsNullOrWhiteSpace(line)) return;

            if (line.StartsWith("[D:")) LogLn(line);
            else if (line.StartsWith("[P:")) ShowPosition(line);
            else if (line.StartsWith("[W:")) DrawPosition(line);


            if (!string.IsNullOrEmpty(expectedResponse))
            {
             
                if (line.Contains(expectedResponse) ||
                    (!string.IsNullOrEmpty(negativeResponse) && line.Contains(negativeResponse)) ||
                    line.Contains(CommandList.stopGcodeStream))
                {
                    responseResult = line.Contains(CommandList.stopGcodeStream) ? CommandList.stopGcodeStream : line;

                    responseSignal.Set();
                }
            }
        }



        /*
         Perform command
         */

        public async Task<bool> PerformComm(string cmd, string response, int timeoutMs = 5000)
        {
            stopRecived = false;
            LogLn("sending: " + cmd);

            string result = await SendCommandAsync(cmd, response, CommandList.stopGcodeStream, timeoutMs);

            if (result == null)
            {
                LogLn("Timeout!");
                return false;
            }

            if (result == CommandList.stopGcodeStream)
            {
                stopRecived = true;
                LogLn("STOP received!");
                return false;
            }

            if (result.Contains(response))
            {
                return true;
            }

            return false;
        }






        // HANDLE POSITION AND DRAWING---------------------------------------------------------
        private void ShowPosition(string cmd)
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

        // board Px per mm for preview in controlelr

        private double MMpxX = 0;
        private double MMpxY = 0;

        private double CursorX= 0;
        private double CursorY= 0;

        // Px per mm for full preview
        private double fullBPxMM = 0;
        private double fullBPyMM = 0;

        // function to draw when cmd [W: arrives
        private void DrawPosition(string cmd)
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
                value = value.Replace(',', '.');
            

                switch (key)
                {
                    case "X": double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out X); break;
                    case "Y": double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out Y); break;
                    case "Z": double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out Z); break;

                }
               
            }

        
            double xres = modelManager.Settings.x_resolution;

            double x_mm = X * modelManager.Settings.x_resolution; // steps → mm
            double y_mm = Y * modelManager.Settings.y_resolution;

            double xpx = x_mm * MMpxX; // mm → pixels
            double ypx = y_mm * MMpxY; // mm → pixels

          
            double xpxB = CursorX + xpx;
            double ypxB = CursorY - ypx; // invert if Y axis down

            Point PointA = new Point((int)(CursorX), (int)(CursorY));
            Point PointB = new Point((int)(xpxB),(int)(ypxB));



            CursorX = xpxB;
            CursorY = ypxB;

            controllerForm.drawLine(PointA,PointB, modelManager.Settings.lineColor, modelManager.Settings.lineSize);

       

        }
        public void resetCursor()
        {
            CursorX = (controllerForm.getCanvasWidth() / 2);
            CursorY = (controllerForm.getCanvasHeight() / 2);
        }
        private void calcDrawingVars()
        {
            MMpxX = (controllerForm.getCanvasWidth() / (modelManager.Settings.miniPreviewXmm));
            MMpxY = (controllerForm.getCanvasHeight() / (modelManager.Settings.miniPreviewYmm));
            CursorX = (controllerForm.getCanvasWidth() / 2);
            CursorY = (controllerForm.getCanvasHeight() / 2);
        }

        // GCODE RELATED---------------------------------------------------------------

        // prep gcode must be in format $<GCODE>#
        public string gcodePack(string line)
        {
            string output = "$<";
            output += line + ">#";
            return output;
        }


        // sends gcde.. it autopacks the code
        public async Task<bool> SendGcode(string line)
        {
            string cmd = gcodePack(line);

            return
                  await PerformComm(cmd, CommandList.gcodeStreamSendNext, TIMEOUT);


        }


        /*
        Gcode run is done by sending line by line then wait for $NEXT# ,
        optinal params are for sending feedback or setting up only previwe using
       motors off (motors off turns mototrs ,plus machine doesnt reespond to stop nor endstops) 



        */


        // SHOULD ALWAYS RETURN TRUE SO UI KNOWS IT FINISHED
        public String gcStreamMsg = "";
        public bool isErrorGcStream = false;
        
        public async Task<bool> RunGcode(
            string filePath,
            bool preview = false,
            bool debug = false,
            bool position = true,
            bool movementOff = false)
        {
            gcStreamMsg = "";
            isErrorGcStream = false;
            if (debug)
            {
                await PerformComm(CommandList.activateDebug, CommandList.ok);
            }
            else
            {
                await PerformComm(CommandList.deactivateDebug, CommandList.ok);
            }
            if (position)
            {
                await PerformComm(CommandList.posOn, CommandList.ok);
            }
            else
            {
                await PerformComm(CommandList.posOff, CommandList.ok);
            }
            if (preview)
            {
                await PerformComm(CommandList.drawOn, CommandList.ok);
                calcDrawingVars();

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




            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    String line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if(StopGcodeStream)
                        {
                            resetCursor();
                            StopGcodeStream = false;
                            break;
                        }


                        bool ans = await SendGcode(line);
                        if (StopGcodeStream)
                        {
                            resetCursor();
                            StopGcodeStream = false;
                            break;
                        }
                        if (!ans)
                        {
                            LogLn("Gcode stream exit timeout / stop error");
                            gcStreamMsg = "Gcode stream exit timeout / stop error";
                            isErrorGcStream = true;
                            return true;
                        }


                    }




                }

            }
            catch (Exception e) { }


            gcStreamMsg = "Finished running!";

            return true;
        }

        // PRE-IMPLEMENTED COMMANDS-------------------------------


        // QUICK COMMANDS SEND-ANSWER
        public async Task<bool> TestConnection(int timeoutMs = 5000)
        {
            return await PerformComm(CommandList.startUsbCommunication, CommandList.usbCommBeginReply, timeoutMs);
        }

        public async Task<bool> SetOrigin(int timeout = 5000)
        {
            return await PerformComm(CommandList.setOrigin, CommandList.ok, timeout);
        }

        public async Task<bool> DisableSteppers(int timeout = 5000)
        {
            return await PerformComm(CommandList.disableStep, CommandList.ok, timeout);
        }



           // timeout for long operations like gcode and sequences
        const int TIMEOUT = 360000000;
        //*
        // Sequences  SEND-SEQ DONE
        //*/
        public async Task<bool> HomeAsync(int timeout = TIMEOUT)
        {

             await PerformComm(CommandList.beginHomeSequence, CommandList.seqenceDone, timeout);
            return true;
        }

        public async Task<bool> ZcalAsync(int timeout = TIMEOUT)
        {
            return await PerformComm(CommandList.beginZcalSequence, CommandList.seqenceDone, timeout);
        }

        public async Task<bool> CenterAsync(int timeout = TIMEOUT)
        {
            return await PerformComm(CommandList.centerSequenceBegin, CommandList.seqenceDone, timeout);


        }


        //** PREDEFINED MOVEMENT
        ///
        // might depend on machine direction
        public async Task<bool> PerformMove(string axis, double mm, double? feedrate)
        {
            await MotorsOn();
            string line = "G90 ";
            if (feedrate.HasValue)
            {
                line += "G1 ";
            }
            else
            {
                line += "G0 ";
            }



            switch (axis)
            {
                case "X":
                    line +="X"+ modelManager.Settings.x_resolution * mm+" " ;

                    break;
                case "Y":
                    line += "Y" + modelManager.Settings.y_resolution * mm + " ";
                    break;

                case "Z":
                    line += "Z" + modelManager.Settings.z_resolution * mm + " ";
                    break;

                case "XY":
                    line += "X" + modelManager.Settings.x_resolution * mm + " "+ "Y" + modelManager.Settings.y_resolution * mm+" ";
                    break;
                case "-X":
                    line += "X-" + modelManager.Settings.x_resolution * mm + " ";
                    break;
                case "-Y":
                    line += "Y-" + modelManager.Settings.x_resolution * mm + " ";
                    break;
                case "-Z":
                    line += "-Z" + modelManager.Settings.z_resolution * mm + " ";
                    break;

                case "-XY":
                    line += "-X" + modelManager.Settings.x_resolution * mm +
                        " " + "Y" + modelManager.Settings.y_resolution * mm + " ";
                    break;
                case "X-Y":
                    line += "X" + modelManager.Settings.x_resolution * mm +
                        " " + "-Y" + modelManager.Settings.y_resolution * mm + " ";
                    break;
                case "-X-Y":
                    line += "-X" + modelManager.Settings.x_resolution * mm +
                        " " + "-Y" + modelManager.Settings.y_resolution * mm + " ";
                    break;
                default:
                    return true;
            }


            if (feedrate.HasValue) {
                line += "F" + feedrate;
            }

          return   await SendGcode(line);

        }

        public async Task<bool> SpindleSet(int speed)
        {
            await MotorsOn();
            String move = "M3 ";
            move += "S";
            move += speed;

            return await SendGcode(move);
        }
        public async Task<bool> StopSpindle()
        {
          return await SendGcode("M5");

        }
        // flag sets
        public async Task<bool> MotorsOff(int timeout=5000) {
           return await PerformComm(CommandList.deactivateMotors,CommandList.ok,timeout);
        }
        public async Task<bool> MotorsOn(int timeout = 5000) {
            return await PerformComm(CommandList.activateMotors, CommandList.ok, timeout);
        }

        public async Task<bool> DebugOn(int timeout = 5000) {
            return await PerformComm(CommandList.activateDebug, CommandList.ok, timeout);
        }

        public async Task<bool> DebugOff(int timeout = 5000) {
            return await PerformComm(CommandList.deactivateDebug, CommandList.ok, timeout);
        }

        public async Task<bool> PositionOn(int timeout = 5000) {
            return await PerformComm(CommandList.posOn, CommandList.ok, timeout);
        }

        public async Task<bool> PositionOff(int timeout = 5000) {
            return await PerformComm(CommandList.posOff, CommandList.ok, timeout);
        }
    }
}

