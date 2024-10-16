using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArduinoCNCPccontroller
{
    internal class ArduinoCommunicator
    {
        //--------------COMMANDS------------------------------------------------------------------------------------------------


        public String startUsbCommunication = "$START";

        public String startGcodeStream = "$BEGIN";

        public String sendSingleGc = "$BEGINS";

        public String beginHomeSequence = "$HOME";

        public String stopGcodeRecive = "$END";

        public String beginZcalSequence = "$ZCAL";

        public String disableES = "$DES";
        public String enableES = "$EES";

        public String disableStep = "$DST";

        public String getPositionData = "$?";

        public String getESstatus = "$E?";

        public String repeatMsg = "$RC#";

        public String repeatLastRepply = "$R";

        public String centerSequenceBegin = "$CENTER";




        //-----Responses---------------------------------------------------------------------------------------------

        public String usbCommBeginReply = "CONNECTED";

        public String gcodeStreamSendNext = "NEXT";

        public String ok = "OK";

        public String stopGcodeStream = "STOP";

        public String seqenceDone = "DONE";

        public String yesA = "Y";
        public String noA= "N";




        // ports and form parts


        private SerialPort port;

        private System.Windows.Forms.Label debugOutput;

        private System.Windows.Forms.Label responseOutput;
        private System.Windows.Forms.Label sentOutput;


        public ArduinoCommunicator(SerialPort port)
        {
            this.port = port;
        }

        public ArduinoCommunicator(SerialPort port, System.Windows.Forms.Label debugOutput)
        {
            this.port = port;
            this.debugOutput = debugOutput;
        }

        public ArduinoCommunicator(SerialPort port, System.Windows.Forms.Label debugOutput, System.Windows.Forms.Label responseOutput, System.Windows.Forms.Label sentOutput)
        {
            this.port = port;
            this.debugOutput = debugOutput;
            this.sentOutput = sentOutput;
            this.responseOutput = responseOutput;
        }
        public ArduinoCommunicator(SerialPort port, System.Windows.Forms.Label responseOutput, System.Windows.Forms.Label sentOutput)
        {
            this.port = port;

            this.sentOutput = sentOutput;
            this.responseOutput = responseOutput;
        }

        public void disconnect()
        {

            port.Close();
        }
        public void SendCmd(String cmd)
        {
            port.DiscardInBuffer();
            Console.WriteLine("Sending:" + cmd);
            ShowSent(cmd);

            
            port.WriteLine(cmd);

        }

        public void SendCmd(String cmd, System.Windows.Forms.Label output)
        {
            port.DiscardInBuffer();

            Console.WriteLine("Sending:" + cmd);
            SendCmd(cmd);
            output.Text = cmd;
        }

        public String ReadResponseCMD()
        {
            String s = "";
            char c = ' ';
            int i = 1;
            while (true)
            {
                c = (char)port.ReadChar();

                if (c == '$')
                {
                    while (true)
                    {

                        c = (char)port.ReadChar();
                        if (c != '#')
                        {
                            s += c;
                        }
                        else
                        {
                            Console.WriteLine("read cmd:" + s);
                            s.Split();
                            ShowResponse(s);
                            return s.Trim();
                        }

                    }

                    i++;
                    if (i > 1000000000)
                    {
                        break;
                    }

                }

            }




        }

        public String ReadResponseCMD(int timeout)
        {
            StringBuilder response = new StringBuilder();
            response.Clear();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                while (sw.ElapsedMilliseconds < timeout)
                {
                    if (port.BytesToRead > 0)
                    {
                        char c = (char)port.ReadChar();
                        if (c == '$')
                        {
                            while (sw.ElapsedMilliseconds < timeout)
                            {
                                if (port.BytesToRead > 0)
                                {
                                    c = (char)port.ReadChar();
                                    if (c == '#')
                                    {
                                        Console.WriteLine("read cmd:" + response.ToString());
                                        return response.ToString().Trim();
                                    }
                                    response.Append(c);
                                }
                            }
                            Console.WriteLine("timeout  $#");
                            ShowResponse(response.ToString());
                            return response.ToString();
                        }
                    }
                }
                Console.WriteLine("timeout");
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("Read timeout: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading from port: " + ex.Message);
            }

            return response.ToString();
        }

        public String ReadStatusData()
        {


            String s = "";
            char c = ' ';
            while (true)
            {
                c = (char)port.ReadChar();

                if (c == '<')
                {
                    while (true)
                    {

                        c = (char)port.ReadChar();
                        if (c != '>')
                        {
                            s += c;
                        }
                        else
                        {
                            return s;
                        }

                    }



                }

            }

        }

        public String ReadStatusData(int timeout)
        {

            String s = "";
            char c = ' ';
            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                while (sw.ElapsedMilliseconds < timeout)
                {
                    c = (char)port.ReadChar();

                    if (c == '<')
                    {
                        while (sw.ElapsedMilliseconds < timeout)
                        {

                            c = (char)port.ReadChar();
                            if (c != '>')
                            {
                                s += c;
                            }
                            else
                            {
                                return s;
                            }



                        }

                    }
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("Read timeout: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading from port: " + ex.Message);
            }
            return s;
        }


        public String ReadStatusDataS()
        {
            SendCmd("$?");
            return ReadStatusData();
        }


        public String ReadStatusDataS(int timeout)
        {
            SendCmd("$?");
            return ReadStatusData(timeout);

        }


        public String SendAndWRepply(String msg, int retry, int timeout)
        {
          
            String reply = "";
            for (int i = 0; i < retry; i++)
            {
                SendCmd(msg);
                reply = ReadResponseCMD(timeout);
                if (reply != "")
                {
                    return reply;
                }
                
            }
            return reply;
        }




     public void ExecuteSingleLine(String line)
        {


            bool wait = true;
            bool stop = false;


            while (true || !stop)
            {
                SendCmd("$BEGIN");
                String s = ReadResponseCMD(100);
                if (s == "") { stop = true; }
                Console.WriteLine("responce:" + s);
                if (s.Contains("STOP"))
                {
                    stop = true;
                }
                if (s.Contains("OK") || s.Contains("NEXT"))
                {

                    break;

                }
            }
            Console.WriteLine("saljem komandu:" + line);

            SendCmd(line);
            while (true || !stop)
            {


                String s = ReadResponseCMD();
                Console.WriteLine("responce:" + s);
                if (s.Contains("STOP"))
                {
                    stop = true;
                }

                if (s.Contains("NEXT") || s.Contains("STOP"))
                {

                    break;

                }




            }
            if (!stop)
            {
                SendCmd("$END");

            }
            Console.WriteLine("Done");



        }


     public void ExecuteSingleLine(String line, int timeout, System.Windows.Forms.Label output, System.Windows.Forms.Label cmdOut)
        {
            SendCmd(startGcodeStream);
            String s = ReadResponseCMD(timeout);
            if (s.Contains(ok))
            {
                SendCmd(line, cmdOut);

                while (true)
                {
                    String response = ReadResponseCMD(10000000);
                    if (response.Contains(gcodeStreamSendNext))
                    {
                        output.Text = ReadStatusData(timeout);
                        SendCmd(stopGcodeRecive);


                        return;

                    }
                    else if (response.Contains(stopGcodeStream))
                    {
                        return;

                    }
                }

            }



        }
       


        public bool StartGCmode()
        {
            SendCmd("$BEGIN");
            String cmd = ReadResponseCMD();
            if (cmd.Contains("OK"))
            {
                return true;
            }
            return false;
        }

        public async Task SendCmdAsync(string cmd)
        {
            Console.WriteLine("Sending: " + cmd);
            await Task.Run(() => port.WriteLine(cmd));
        }

        public bool ConnectB(int retry)
        {
          

            for (int i = 0; i < retry; i++)
            {
                SendCmd("$START");

                Console.WriteLine("try:" + i);
                String response = ReadResponseCMD(100);
                if (response.Contains("CONNECTED"))
                {
                    return true;
                }

            }

            return false;
        }

        public void SendManual(String msg, int retry, int timeout, System.Windows.Forms.Label output)
        {

            output.Text = SendAndWRepply(msg, retry, timeout);
        }



        //-------Stream gcode------------------------------------------------------------------------------------------------------------------

        public bool Streaming = false;
        public bool StreamGcodeFile(String filePath, System.Windows.Forms.Label inputLbl, System.Windows.Forms.Label responseLbl)
        {

            Streaming = true;
            port.DiscardInBuffer();
            String begin = SendAndWRepply(startGcodeStream, 3, 100);
            responseLbl.Text = begin;
            if (!begin.Contains(ok))
            {
                LogLn("failed to start gcode stream");

                return false;
            }

            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            inputLbl.Text = line;
                            SendCmd(line);
                            bool wait = true;
                            while (wait)
                            {
                                String response = ReadResponseCMD(3600000);
                                responseLbl.Text = response;
                                if (response.Contains(gcodeStreamSendNext))
                                {
                                    wait = false;

                                    LogLn("line executed!");
                                }
                                if (response.Contains(stopGcodeStream))
                                {
                                    wait = false;
                                    LogLn("gcode stream stopped: pressed endstop or stop btn");
                                    Streaming = false;
                                    return false;
                                }



                            }






                        }
                    }
                }
                catch (Exception e)
                {

                }






            }



            return false;
        }

        public async Task<int> StreamGcodeFileAsync(string filePath, System.Windows.Forms.Label inputLbl,
                   System.Windows.Forms.Label responseLbl, System.Windows.Forms.Label machinePos)
        {
            port.DiscardInBuffer();
            Streaming = true;

            try
            {
                string begin = await Task.Run(() => SendAndWRepply(startGcodeStream, 3, 100));
                responseLbl.Text = begin;

                if (!begin.Contains(ok))
                {
                    LogLn("Failed to start G-code stream");
                    return 1;
                }

                if (!File.Exists(filePath))
                {
                    LogLn("File not found: " + filePath);
                    return 3;
                }

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        inputLbl.Invoke(new Action(() => inputLbl.Text = line));

                        await Task.Run(() => SendCmd(line));

                        bool wait = true;

                        while (wait)
                        {
                            string response = await Task.Run(() => ReadResponseCMD(3600000));
                            LogLn("Response = " + response);

                            if (response.Contains(gcodeStreamSendNext))
                            {
                                wait = false;
                                LogLn("Line executed!");
                            }
                            else if (response.Contains(stopGcodeStream)|| response.Contains("STPBTN"))
                            {
                                LogLn("G-code stream stopped: pressed endstop or stop button");
                                Streaming = false;
                                return 2;
                            }
                            else if (response.Contains(seqenceDone))
                            {
                                Streaming = false;
                                return 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogLn("Error during G-code streaming: " + ex.Message);
                return 4;
            }
            finally
            {
                Streaming = false;
            }

            return 0;
        }
        public bool MoveAxis(char axis, String distance)
        {
            String msg = SendAndWRepply(sendSingleGc, 3, 100);
            if (msg.Contains(ok))
            {
                msg = SendAndWRepply("G0 " + axis + distance, 1, 600000);

                if (msg.Contains(seqenceDone)) { return true; }

            }

            return false;
        }
        
        //----------- move axis normal-*-----------*---------------------------*-

        public bool MoveXp(String distance) { return MoveAxis('X', distance); }
        public bool MoveYp(String distance) { return MoveAxis('Y', distance); ; }
        public bool MoveZp(String distance) { return MoveAxis('Z', distance); ; }

        public bool MoveXn(String distance) { return MoveAxis('X', '-'+distance); ; }
        public bool MoveYn(String distance) { return MoveAxis('Y', '-'+distance); ; }
        public bool MoveZn(String distance) { return MoveAxis('Z', '-'+distance); ; }



        //--------MOVE AXIS ASYNC-------------------------------------------------------------------------------------------------------------





        //*------------- disble endstops -----------------------------------------------------------------------------------------------------

        public bool DisableES() {
            String response = SendAndWRepply(disableES, 3,100);
            if (response.Contains(ok))
            {
                return true;
            }
            return false;
        }

        public bool EnableES()
        {
            String response = SendAndWRepply(enableES, 3, 100);
            if (response.Contains(ok))
            {
                return true;
            }
            return false;
        }

        public bool AreESenabled() {
            String response = SendAndWRepply(getESstatus,3,100);
            if (response.Contains(yesA)) { return true; }
       
            return false;
        }

        //------------ SHOW DATA-------------------------------------------------------------------

        public void LogLn(String line)
        {
            Console.WriteLine(line);
            if (debugOutput != null)
            {
                debugOutput.Text += line;
            }
        }
        public void RefreshResponse() {
            if (sentOutput != null)
            {
                sentOutput.Text = "";
            }


            if (responseOutput != null)
            {
                responseOutput.Text = "";
            }

        }
        public void ShowResponse(String response)
        {
            if (responseOutput != null)
            {
                RefreshResponse();
                responseOutput.Text = response;
            }
        }
        public void ShowSent(String sent)
        {
            if (sentOutput != null)
            {
               
                sentOutput.Text = sent;
            }
        }

     


        //--------------------------------------------------------------------------------------------------------------------------------------------




    }


}
