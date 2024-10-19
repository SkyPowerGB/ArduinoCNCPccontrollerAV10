using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller
{
    internal class DrawLines
    {
        // offsets and scale

      private  double scale;
        private double xOffset;
        private double yOffset;

        // cursor position

        private double cursorPosX=0;
        private double cursorPosY=0;

        private double machineAbsPosX = 0;
        private double machineAbsPosY = 0;



        private double xAxisStepMM = 100;
        private double yAxisStepMM = 100;


        //reading helpers

        private bool readPC = false;
        private bool ignore = false;
        String output = "";

        // G-code status variables

        private bool spindleOn = false;
        private bool spindleDirClock = true;
        private bool unitModeMM = true;
        private bool distanceModeAbs = false;
        private bool toolLengthOffset = false;
        private bool cutterCompensation = false;
        private bool exactStop = false;
        private bool feedRateModeUnitPmin = true;
        private byte motionMode = 0;  // g0 1 / g1 2 / g2 3 / g3 4 / g38.2-38.5 5-8 / g80 9
        private bool xyPlane = true;
        private bool xzPlane = false;
        private bool yzPlane = false;

        // G-code parameter status variables

        private double feedRate = 0;
        private double Ivalue = 0;
        private double Kvalue = 0;
        private double Jvalue = 0;
        private double Rvalue = 0;
        private double Svalue = 0;
        private double Xvalue = 0;
        private double Yvalue = 0;
        private double Zvalue = 0;



        //stuff thats recaclucated

        private float xAxisResoultion=100;
        private float yAxisResoultion=100;


        //*-------------------------------------------------------------------------------------*

        void readCommandLine(String gCodeLine)
        {

         
           
            resetPosVars();
          

            for ( int i = 0; i < gCodeLine.Length; i++)
            {
                if (gCodeLine[i] == ')')
                {
                    ignore = false;
                }
                else if (gCodeLine[i] == '(')
                {
                    ignore = true;
                }

                if (!ignore)
                {
                    if (validChar(gCodeLine[i]))
                    {
                        readPC = true;
                        output += gCodeLine[i];
                    }
                    else if (readPC)
                    {
                        gcodeSegmentReciver(output);
                        readPC = false;
                        output = "";
                    }
                }
            }

            if (readPC && !(output == ""))
            {
                gcodeSegmentReciver(output);
                output = "";
                readPC = false;
            }

            if (!ignore)
            {
                executeCommand();
            }

           
        }

        // Handle received G-code segment
        void gcodeSegmentReciver(String code)
        {
            char c = code[0];
            
            code.Remove(0, 1 );
            double num = Double.Parse(code);

            switch (c)
            {
                case 'G': gCom(num); break;
                case 'M': mCom(num); break;
                case 'F': feedRate = num; break;
                case 'I': Ivalue = num; break;
                case 'J': Jvalue = num; break;
                case 'K': Kvalue = num; break;
                case 'R': Rvalue = num; break;
                case 'S': Svalue = num; break;
                case 'X': Xvalue = num; break;
                case 'Y': Yvalue = num; break;
                case 'Z': Yvalue = num; break;

            }
        }

        // Reset nonModal variables
        void resetPosVars()
        {
            if (!distanceModeAbs)
            {
                Xvalue = 0;
                Yvalue = 0;
              

            }
            else
            {
                Xvalue = machineAbsPosX;
                Yvalue = machineAbsPosY;
             
            }
            Ivalue = 0;
            Kvalue = 0;
            Jvalue = 0;
            Rvalue = 0;
        }
        // set origin
        void setOrigin()
        {
            cursorPosX = 0; cursorPosY = 0; machineAbsPosX = 0; machineAbsPosY = 0;

        }

        // Valid non num Char
        /*
        bool validCharNN(char c){
           switch (c) {

            case 'G':
            case 'M':
            case 'X':
            case 'Y':
            case 'Z':
            case 'I':
            case 'J':
            case 'K':
            case 'F':
            case 'P':
            case 'D':
            case 'H':
            case 'S':
            case 'T':
            case 'L': return true;
            default: return false;

        }*/

        // Valid character check
        bool validChar(char c)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                case '-':
                case 'G':
                case 'M':
                case 'X':
                case 'Y':
                case 'Z':
                case 'I':
                case 'J':
                case 'K':
                case 'F':
                case 'P':
                case 'D':
                case 'H':
                case 'S':
                case 'T':
                case 'L': return true;
                default: return false;
            }
        }

        //maknul 0,0,0 u g90 i dodano g54 set origin
        // Handle G commands
        void gCom(double num)
        {
            switch ((int)num)
            {
                case 0: motionMode = 1; break;
                case 1: motionMode = 2; break;
                case 2: motionMode = 3; break;
                case 3: motionMode = 4; break;
           
                case 80: motionMode = 9; break;
                case 93: feedRateModeUnitPmin = false; break;
                case 94: feedRateModeUnitPmin = true; break;
                case 20: unitModeMM = false; break;
                case 21: unitModeMM = true; break;
                case 90:
                    distanceModeAbs = true;

                    break;
                case 91: distanceModeAbs = false; break;
                case 54: setOrigin(); break;
                case 17:
                    xyPlane = true;
                    xzPlane = false;
                    yzPlane = false;
                    break;
                case 18:
                    xyPlane = false;
                    xzPlane = true;
                    yzPlane = false;
                    break;
                case 19:
                    xyPlane = false;
                    xzPlane = false;
                    yzPlane = true;
                    break;
            }
        }

        // Handle M commands
        void mCom(double num)
        {
            switch ((int)num)
            {
                case 0:
                case 1: break;
                case 2:
                case 30:
                    spindleOn = false;


                  

                




                    break;
                case 3:
                    spindleOn = true;
                    spindleDirClock = true;
                    break;
                case 4: spindleDirClock = false; break;
                case 5: spindleOn = false; break;
            }
        }


        // Execute command based on current state
        void executeCommand()
        {
           


            switch (motionMode)
            {
                case 1: executeLinearMax(); break;
                case 2: executeLinear(); break;
                case 3: executeCircular(true); break;
                case 4: executeCircular(false); break;
            }


        }

        //-------Circualr motion helpers---------------------

        double outputA;
        double outputB;
        bool doFullCircle = false;
        void followCircle(double startA, double startB, double centerA, double centerB, double targetA, double targetB, double increment, double threshold, bool dirClock)
        {

            double ctrA = centerA / increment;
            double ctrB = centerB / increment;

            double ta = targetA / increment;
            double tb = targetB / increment;

            double sa = startA / increment;
            double sb = startB / increment;


            ctrA = Math.Floor(ctrA) * increment;
            ctrB = Math.Floor(ctrB) * increment;


            ta = Math.Floor(ta) * increment;
            tb = Math.Floor(tb) * increment;

            sa = Math.Floor(sa) * increment;
            sb = Math.Floor(sb) * increment;

            if (sa == ta && sb == tb)
            {
                doFullCircle = true;
            }
            else
            {
                doFullCircle = false;
            }


         



            double pstarta = sa;
            double pstartb = sb;
            double radious = getTwoPointDistance(ctrA, ctrB, ta, tb);

            int checker =(int)( (4 * radious) / increment);


            if (radious <= increment)
            {
             
                followCircleOutput(targetA, targetB);
                return;
            }

            bool runCircle = true;
            while (runCircle)
            {

                if (getTwoPointDistance(pstarta, pstartb, ta, tb) < threshold && !doFullCircle)
                {
              
                    runCircle = false;
                    break;
                }

                if (checker == 0) { break; }

                checker--;
                calcNextPoint(pstarta, pstartb, radious, ctrA, ctrB, increment, dirClock);
                pstarta = outputA;
                pstartb = outputB;

                followCircleOutput(pstarta, pstartb);
            }

            followCircleOutput(targetA, targetB);
        }

        void findCirclePoint(double axisA, double centerAxisA, double centerAxisB, double radius, double increment, bool right, bool up)
        {
            outputA = axisA;
            outputB = centerAxisB;
            if (right)
            {
                outputA += increment;
            }
            else
            {
                outputA -= increment;
            }

            while (getTwoPointDistance(outputA, outputB, centerAxisA, centerAxisB) < radius)
            {
                if (up)
                {
                    outputB += increment;
                }
                else
                {
                    outputB -= increment;
                }
            }

            // DECREASE IF ITS MORE THAN RADIOUS OUTSIDE CIRCLE
            if ((getTwoPointDistance(outputA, outputB, centerAxisA, centerAxisB) - radius) > radius)
            {
                if (up)
                {
                    outputB -= increment;
                }
                else
                {
                    outputB += increment;
                }
            }
        }

        void calcNextPoint(double startA, double startB, double radius, double centerA, double centerB, double increment, bool dirClock)
        {
            int pointPos = getPointPosOnCircle(startA, startB, centerA, centerB);
        
            // 1-> POINT 1/4     4->POINT 4/4
            //  5 0DEG , 7 180   6 90 AND 8 270
            switch (pointPos)
            {
                case 1:
                case 2:
                    if (dirClock)
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, true, true);
                    }
                    else
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, false, true);
                    }
                    break;
                case 3:
                case 4:
                    if (dirClock)
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, false, false);
                    }
                    else
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, true, false);
                    }
                    break;
                case 5:
                    if (dirClock)
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, false, false);
                    }
                    else
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, false, true);
                    }
                    break;
                case 6:
                    if (dirClock)
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, true, true);
                    }
                    else
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, false, true);
                    }
                    break;
                case 7:
                    if (dirClock)
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, true, true);
                    }
                    else
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, true, false);
                    }
                    break;
                case 8:
                    if (dirClock)
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, false, false);
                    }
                    else
                    {
                        findCirclePoint(startA, centerA, centerB, radius, increment, true, false);
                    }
                    break;
            }
        }
        int getPointPosOnCircle(double pointAaxis, double pointBaxis, double circleAaxis, double circleBaxis)
        {

            //
            if (pointBaxis == circleBaxis && pointAaxis > circleAaxis)
            {
                //POINT ON CNTER AXIS , RIGHT (0DEG)
                return 5;
            }
            else if (pointBaxis == circleBaxis && pointAaxis < circleAaxis)
            {
                //POINT ON CNTER AXIS , LEFT (180DEG)

                return 7;
            }
            else if (pointAaxis == circleAaxis && pointBaxis > circleBaxis)
            {
                //POINT ON CNTER AXIS , ABOVE (90DEG)

                return 6;
            }
            else if (pointAaxis == circleAaxis && pointBaxis < circleBaxis)
            {
                //POINT ON CNTER AXIS , BELOW (270DEG)

                return 8;
            }
            else if (pointAaxis > circleAaxis && pointBaxis > circleBaxis)
            {

                // POINT IN 1/4

                return 1;
            }
            else if (pointAaxis < circleAaxis && pointBaxis > circleBaxis)
            {

                // POINT IN 2/4

                return 2;
            }
            else if (pointAaxis < circleAaxis && pointBaxis < circleBaxis)
            {
                // POINT IN 3/4

                return 3;
            }
            else if (pointAaxis > circleAaxis && pointBaxis < circleBaxis)
            {
                // POINT IN 4/4
                return 4;
            }
            else
            {
                return 0;
            }
        }

        //--------Execution----------------------------------------------------------------------------------------------------------------------------------------------
        void executeLinear()
        {

            linearMove(Xvalue, Yvalue);
        }

        void executeLinearMax()
        {
            moveAllAxis(Xvalue, Yvalue );
        }
        //circle precision setup
        float treshHold = 0.1f;
        float increment = 0.08f;
        void executeCircular(bool dirClock)
        {

            double sx = 0;
            double sy = 0;
            double sz = 0;

            double ci = 0;
            double cj = 0;
            double ck = 0;

            ci = Ivalue;
            cj = Jvalue;
            ck = Kvalue;

            double tx = Xvalue;
            double ty = Yvalue;
         

            if (distanceModeAbs)
            {
                sx = machineAbsPosX;
                sy = machineAbsPosY;
              
            }
            ci = sx + ci;
            cj = sy + cj;
            ck = sz + ck;




            if (xyPlane)
            {

                followCircle(sx, sy, ci, cj, tx, ty, increment, treshHold, dirClock);
            }
            else if (yzPlane)
            {
                return;

                followCircle(sy, sz, cj, ck, ty, 0, increment, treshHold, dirClock);
            }
            else if (xzPlane)
            {
                return;

                followCircle(sx, sz, ci, ck, tx, 0, increment, treshHold, dirClock);
            }
        }

        void followCircleOutput(double  a, double b)
        {


            if (xyPlane)
            {
                
                linearMove(a, b);
            }
            else if (yzPlane)
            {
                return;
                linearMove(0, a);
            }
            else if (xzPlane)
            {
                return;
                linearMove(a, 0);
            }
        }

        void linearMove(double x, double y)
        {
            drawLine(cursorPosX,cursorPosY,x,y,true);


            moveAllAxis(x, y);
        }
        double calcMove(double absPos, double point)
        {
            double diff =Math.Abs(absPos - point);
            if (point < absPos)
            {
                return -diff;
            }
            else
            {
                return diff;
            }
        }

        // prep data--------------------------------------------------------------------------------------------
        void moveAllAxis(double x, double y)
        {
          

            double xt = 0, yt = 0, zt = 0;
            int xSteps, ySteps, zSteps;
            double xSpd, ySpd, zSpd;
            double xAcc, yAcc, zAcc;


            bool xDir = true, yDir = true, zDir = true;
        


            if (distanceModeAbs)
            {

                xt = calcMove(machineAbsPosX, x);
                yt = calcMove(machineAbsPosY, y);
            
            }
            else
            {
                xt = x;
                yt = y;
                
            }

            if (unitModeMM)
            {
                xSteps = (int)Math.Floor((xt * xAxisStepMM));
                ySteps = (int)Math.Floor((yt * yAxisStepMM));
            
            }
            else
            {
                xSteps = (int)Math.Floor(xt * (xAxisStepMM * 25.4));
                ySteps =(int) Math.Floor(yt * (yAxisStepMM * 25.4));
               
            }


            if (xSteps < 0)
            {
                xSteps = xSteps * -1;
                xDir = false;
            }
            if (ySteps < 0)
            {
                ySteps = ySteps * -1;
                yDir = false;
            }
           


            if (xDir)
            {
                machineAbsPosX += (xAxisResoultion * xSteps);
            }
            else
            {
                machineAbsPosX -= (xAxisResoultion * xSteps);
            }
            if (yDir)
            {
                machineAbsPosY += (yAxisResoultion * ySteps);
            }
            else
            {
                machineAbsPosY -= (yAxisResoultion * ySteps);
            }
           
         
            /*

            setMotorParameters(xSteps, xDir);
            setMotorParameters(ySteps, yDir);
         
            */
            
        }
      
        
        //helpers

 
        double getTwoPointDistance(double x1, double y1)
        {
            return getTwoPointDistance(x1, y1, 0, 0);
        }
        double getTwoPointDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }







        private void drawLine(double xPosStart, double yPosStart,double xPosTarget,double yPosTarget,bool draw)
        {

            cursorPosX = xPosTarget;
            cursorPosY = yPosTarget;



        }








    }
}
