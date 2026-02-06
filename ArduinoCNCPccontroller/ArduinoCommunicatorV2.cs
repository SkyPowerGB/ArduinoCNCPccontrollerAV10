using ArduinoCNCPccontroller.classes;
using ArduinoCNCPccontroller.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ArduinoCNCPccontroller
{
    internal class ArduinoCommunicatorV2
    {

        //------- the variables    ---------------------------

        private SerialPort port;

        private RichTextBox debugTexbox;
        private CNC_PC_controller UI;
        private JsonModelManager modelManager;


        //******************************************************
        //CONSTRUCTORS
        public ArduinoCommunicatorV2(SerialPort port)
        {
            this.port = port;
        }

        public ArduinoCommunicatorV2(SerialPort port, RichTextBox debugBox, CNC_PC_controller UI)
        {
            this.port = port;
            this.debugTexbox = debugBox;
            this.UI = UI;
            this.modelManager = new JsonModelManager();


        }

        /************----------------------------------------****/

        public bool Connect()
        {
            try
            {
                port.Open();
                Thread.Sleep(2000);
                if (SendTestRepply(CommandList.startUsbCommunication, CommandList.usbCommBeginReply, 200, 3))
                {
                    LogLn("succes");

                    return true;
                }
                else
                {
                    port.Close();
                    return false;
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }

        public bool Disconnect()
        {
            port.Close();
            return true;
        }

        //*************************************************************************
        //----BASIC COMMUNICATION 
        public void SendCmd(String cmd)
        {
            port.DiscardInBuffer();
            LogLn("Sending ;" + cmd);



            port.WriteLine(cmd);

        }
        public String ReadResponseCMD()
        {
            LogLn("GetResult ");
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
                            LogLn("Recive answer cmd:" + s);
                            s.Split();

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
            LogLn("GetResult w timeout");

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
                                        LogLn("read cmd answer with timeout:" + response.ToString());
                                        return response.ToString().Trim();
                                    }
                                    response.Append(c);
                                }
                            }
                            LogLn("Timeout");

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

        public String ReadResponseCMD(int timeout, string target)
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
                            sw.Restart();
                            while (sw.ElapsedMilliseconds < timeout)
                            {
                                if (port.BytesToRead > 0)
                                {
                                    c = (char)port.ReadChar();
                                    if (c == '#')
                                    {

                                        String r = response.ToString().Trim();
                                        LogLn("read cmd " + r);


                                    }

                                    response.Append(c);
                                }
                            }




                        }
                    }

                    return response.ToString();
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

        public String SendAndWRepply(String msg, String target, int retry, int timeout)
        {
            LogLn("sending" + msg);

            String reply = "";
            for (int i = 0; i < retry; i++)
            {
                SendCmd(msg);
                reply = ReadResponseCMD(timeout, target);
                if (reply != "")
                {
                    return reply;
                }

            }
            return reply;
        }
        public String SendAndWRepply(String msg, int timeout)
        {
            LogLn("sending" + msg);

            String reply = "";

            SendCmd(msg);
            reply = ReadResponseCMD(timeout);
            if (reply != "")
            {
                return reply;
            }


            return reply;
        }
        public bool SendTestRepply(String msg, String expectedRepply, int timeout)
        {
            String result = SendAndWRepply(msg, timeout);

            LogLn("Send and wait, recived:" + result);

            if (result.Contains(expectedRepply))
            {
                return true;
            }
            return false;


        }


        public bool SendTestRepply(String msg, String expectedRepply, int timeout, int retry)
        {
        

            if (SendAndWait(msg,expectedRepply,timeout,retry))
            {
                return true;
            }
            return false;

        }


        // Updated SENDING AND RECIVING END.

        public bool SendAndWait(String cmd, String target, int timeout, int retry)
        {
            LogLn("send and wait for: " +target);

            String r;
            StringBuilder response = new StringBuilder();
            response.Clear();
            Stopwatch sw = new Stopwatch();


            for (int i = 0; i < retry; i++)
            {
                SendCmd(cmd);
                sw.Start();

                while (sw.ElapsedMilliseconds < timeout)
                {

                    if (port.BytesToRead > 0)
                    {
                        char c = (char)port.ReadChar();

                        if (c == '$')
                        {
                            response.Append(c);

                            sw.Restart();
                            while (sw.ElapsedMilliseconds < timeout)
                            {
                                if (port.BytesToRead > 0)
                                {
                                    c = (char)port.ReadChar();
                                    response.Append(c);

                                    if (c == '#')
                                    {
                                        break;
                                    }

                                }


                            }
                            sw.Restart();

                        }
                    }
                  


                   

                }

                LogLn("recived: " + response);
                if (response.ToString().Trim().Contains(target))
                {
                    ProcessAnswer(response.ToString());
                    
                    return true;
                }
                else
                {
                    ProcessAnswer(response.ToString());
                    
                }
                response.Clear();

            }
            return false;
        }





        // send G-Code proper
        private void SendGcode(String gcode)
        {

            SendCmd(PackGcode(gcode));

        }

        //*************************************************************************
        //----
        public void ProcessAnswer(String msg)
        {
            bool debug = false;
            bool debugd = false;
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine(msg);
            for (int i = 0; i < msg.Length; i++) {

                switch (msg[i]) {

                    case '[':
                     
                        debug = true;
                        continue;
                    case ']':
                        LogLn("Debug: "+stringBuilder.ToString());
                        stringBuilder.Clear();
                        debug = false;
                        break;




                
                }

                if (debug) {
                    if (msg[i] != 'D'&& msg[i]!=':')
                    {
                        stringBuilder.Append(msg[i]);
                    
                    }
                 
                }

            
            }


        }

        bool waitingAnswer;
        public bool WaitAnswer(String ans)
        {
            waitingAnswer = true;
            while (waitingAnswer)
            {
                String answer = ReadResponseCMD();
                if (answer.Contains(ans))
                {
                    return true;
                }
                else
                {
                    ProcessAnswer(answer);
                }

            }
            return false;

        }

        public bool WaitAnswer(String ans, int timeout)
        {
            waitingAnswer = true;
            while (waitingAnswer)
            {
                String answer = "";
                answer = ReadResponseCMD(timeout);
                if (answer.Contains(ans))
                {
                    return true;
                }
                else
                {
                    ProcessAnswer(answer);
                }

            }
            return false;


        }


        /**//********************************************************************/
        // prep gcode 
        private String PackGcode(String gcode)
        {
            String output = "$<";
            output += gcode;
            output += ">#";
            return output;

        }


        //*************************************************************************
        // Gcode Stream;

        private bool streaming = false;
        public void StreamGcodeFile(String filePath, int startLine)
        {
            streaming = true;
            bool pause = false;

            int i = 1;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    String line;
                    while ((line = reader.ReadLine()) != null && !streaming)
                    {
                        if (i >= startLine)
                        {
                            SendGcode(line);
                            WaitAnswer(CommandList.gcodeStreamSendNext);
                        }

                        i++;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task<bool> StreamGcodeFileAsync(String filePath, int startline)
        {
            LogLn("Strart GC stream");
            if (!streaming)
            {
                streaming = true;
            }
            bool pause = false;

            int i = 1;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    LogLn("Strart GC stream ready!");
                    String line;
                    while ((line = reader.ReadLine()) != null && streaming)
                    {
                        LogLn(" GC stream readLine");
                        if (i >= startline)
                        {
                            SendGcode(line);
                            while (true)
                            {
                                bool ans = await Task.Run(() => WaitAnswer(CommandList.gcodeStreamSendNext));
                                if (ans) { break; }
                            }
                        }

                        i++;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return true;

        }


        public async Task<bool> StreamGcodeFileAsyncV2(String filePath, int startline, bool debugD,
            bool drawToUi)
        {
            if (UI == null)
            {

                LogLn("UI not defined");

                return true;
            }

            LogLn("Strart GC stream");
            if (!streaming)
            {
                streaming = true;
            }
            bool pause = false;

            int i = 1;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    LogLn("Strart GC stream ready!");
                    String line;
                    while ((line = reader.ReadLine()) != null && streaming)
                    {
                        LogLn(" GC stream readLine");
                        if (i >= startline)
                        {
                            SendGcode(line);
                            while (true)
                            {

                                bool ans = await Task.Run(() => WaitAnswer(CommandList.gcodeStreamSendNext));
                                if (ans) { break; }
                            }
                        }

                        i++;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }


        }


        public void drawToUi()
        {



        }

        //*------------------------------------------------------------------------------
        // GCODE STRAM CONTORL

        public bool IsGcStreamRunning() { return streaming; }

        public void StopGcStream() { streaming = false; }


        //*************************************************************************

        //PreDefined and single command

        public void Home()
        {
            SendCmd(CommandList.beginHomeSequence);
        }

        public void Center()
        {
            SendCmd(CommandList.centerSequenceBegin);
        }

        public void ZCal()
        {
            SendCmd(CommandList.beginZcalSequence);
        }

        public void Origin()
        {
            SendCmd(CommandList.setOrigin);
        }

        public void DisableSteppers()
        {
            SendCmd(CommandList.disableStep);
        }

        public void StartDebugD()
        {
         
        }
        public void StopDebugD()
        {
          
        }

        public void MoveAxis(char axis, string steps, string speed)
        {
            String move = "G90 G1 ";
            move += axis;
            move += steps;
            move += " ";
            move += 'F';
            move += speed;

            SendGcode(move);
        }

        public void MoveDiagonal(char axisA, char axisB, string stepsA, string stepsB, string speed)
        {
            String move = "G90 G1 ";
            move += axisA;
            move += stepsA;
            move += " ";

            move += axisB;
            move += stepsB;
            move += " ";

            move += 'F';
            move += speed;

            SendGcode(move);
        }



        public void StartSpindle(int speed)
        {
            String move = "M3 ";
            move += "S";
            move += speed;

            SendGcode(move);

        }

        public void StopSpindle()
        {

            SendGcode("M5");
        }




        //***********************************************************************************************
        // SPECIAL FOR DEBUG / SHOW




        //*--------------------------------------------------------------------------------------------------------------

        public void LogLn(string text)
        {
            Console.WriteLine(text);


            if (debugTexbox != null)
            {

                text += Environment.NewLine;

                if (debugTexbox.InvokeRequired)
                {
                    debugTexbox.Invoke(new Action(() => debugTexbox.AppendText(text)));
                }
                else
                {
                    debugTexbox.AppendText(text);
                }
            }
        }

        public void Log(String text)
        {
            Console.Write(text);
            if (debugTexbox != null)
            {
                debugTexbox.Text += text;

            }
        }
    }





}


