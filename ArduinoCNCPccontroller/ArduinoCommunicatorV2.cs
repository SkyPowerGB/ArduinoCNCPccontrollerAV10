﻿using System;
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
        //--------------COMMANDS------------------------------------------------------------------------------------------------


        private String startUsbCommunication = "$START";

        private String endCOnnection = "$ENDCONN";

        private String beginHomeSequence = "$HOME";

        private String stopGcodeRecive = "$END";

        private String beginZcalSequence = "$ZCAL";

        private String disableES = "$DES";
        private String enableES = "$EES";

        private String disableStep = "$DST";

        private String getPositionData = "$?";

        private String getESstatus = "$E?";

        private String repeatMsg = "$RC#";

        private String repeatLastRepply = "$R";

        private String centerSequenceBegin = "$CENTER";


        private String setOrigin = "$ORIGIN";

        //-----Responses---------------------------------------------------------------------------------------------

        private String usbCommBeginReply = "CONNECTED";

        private String gcodeStreamSendNext = "NEXT";

        private String ok = "OK";

        private String stopGcodeStream = "STOP";

        private String seqenceDone = "DONE";

        private String yesA = "Y";

        private String noA = "N";

        //------- the variables    ---------------------------

        private SerialPort port;

        private RichTextBox debugTexbox;
        //******************************************************
        //CONSTRUCTORS
        public ArduinoCommunicatorV2(SerialPort port)
        {
            this.port = port;
        }

        public ArduinoCommunicatorV2(SerialPort port , RichTextBox debugBox)
        {
            this.port = port;
            this.debugTexbox = debugBox;
        }

        /************----------------------------------------****/

        public bool Connect()
        {
            try
            {
                port.Open();
                Thread.Sleep(2000);
                if (SendTestRepply(startUsbCommunication, usbCommBeginReply, 200,3)) {
                    LogLn("succes");
                    
                    return true; }
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

        public bool Disconnect() {
            port.Close();
            return true;
        }

        //*************************************************************************
        //----BASIC COMMUNICATION 
        public void SendCmd(String cmd)
        {
            port.DiscardInBuffer();
            LogLn("Sending ;"+cmd);



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
        public String SendAndWRepply(String msg, int timeout)
        {

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

            LogLn("Send and wait, recived:"+result);

            if (result.Contains(expectedRepply))
            {
                return true;
            }
            return false;


        }
        public bool SendTestRepply(String msg, String expectedRepply, int timeout,int retry)
        {
            String result = SendAndWRepply(msg,retry, timeout);

            LogLn("Send and wait, recived:" + result);

            if (result.Contains(expectedRepply))
            {
                return true;
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
        public void ProcessAnswer(String msg) { }

        bool waitingAnswer;
        public bool WaitAnswer(String ans) {
            waitingAnswer = true;
            while (waitingAnswer) {
                String answer = ReadResponseCMD();
                if (answer.Contains(ans))
                {
                    return true;
                }
                else {
                    ProcessAnswer(answer);
                }
            
            }
            return false;
        
        }

        public bool WaitAnswer(String ans, int timeout) {
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
                    while ((line=reader.ReadLine()) != null && !streaming)
                    {
                        if (i >= startLine )
                        {
                            SendGcode(line);
                            WaitAnswer(gcodeStreamSendNext);
                        }

                        i++;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public async Task<bool> StreamGcodeFileAsync(String filePath, int startline) {
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
                                bool ans = await Task.Run(() => WaitAnswer(gcodeStreamSendNext));
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

        //*------------------------------------------------------------------------------
        // GCODE STRAM CONTORL

        public bool IsGcStreamRunning() { return streaming; }

        public void StopGcStream() { streaming = false; }
    
        
        //*************************************************************************

        //PreDefined and single command

        public void Home() {
            SendCmd(beginHomeSequence);
        }

        public void Center() {
            SendCmd(centerSequenceBegin);
        }

        public void ZCal() {
            SendCmd(beginZcalSequence);
        }

        public void Origin() {
            SendCmd(setOrigin);
        }
       
        public void DisableSteppers() {
            SendCmd(disableStep);
        }

       

        public void MoveAxis(char axis , string steps, string speed) {
            String move= "G90 G1 ";
            move += axis;
            move += steps;
            move += " ";
            move += 'F';
            move += speed;

            SendGcode(move);
        }
        
        public void MoveDiagonal(char axisA, char axisB, string stepsA, string stepsB, string speed) {
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



        public void StartSpindle(int speed) {
            String move = "M3 ";
            move += "S";
            move+= speed;

            SendGcode(move);
           
        }

        public void StopSpindle() {

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
    
    
