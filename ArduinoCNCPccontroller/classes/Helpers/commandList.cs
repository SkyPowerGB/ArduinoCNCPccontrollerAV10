using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller.Enums
{
    public static class CommandList
    {

        public static String startUsbCommunication = "$START";

        public static String endConnection = "$ENDCONN";

        public static String beginHomeSequence = "$HOME";
        public static String beginZcalSequence = "$ZCAL";
        public static String centerSequenceBegin = "$CENTER";

        public static String disableES = "$DES";

        public static String enableES = "$EES";

        public static String disableStep = "$DST";

        public static String getPositionData = "$?";

        public static String getESstatus = "$E?";

        public static String repeatMsg = "$RC#";

        public static String repeatLastRepply = "$R";

   
        public static String setOrigin = "$ORIGIN";

        public static String activateDebug = "$DEBUG";
        public static String deactivateDebug = "$DEBUG_OFF";
        public static String debugState = "$D?";
       


        public static String activateMotors = "$MOV_ON";
        public static String deactivateMotors = "$MOV_OFF";
        public static String motorsState = "$M?";

        public static String posOff = "$POS_OFF";
        public static String posOn = "$POS";
        public static String posState = "$P?";

        public static String drawOff = "$DRAW_OFF";
        public static String drawOn = "$DRAW";
        public static String drawState = "$W?";

        //**************************************************************

        public static String usbCommBeginReply = "$CONNECTED#";

        public static String gcodeStreamSendNext = "$NEXT#";

        public static String ok = "$OK#";

        public static String stopGcodeStream = "$STOP#";

        public static String seqenceDone = "$DONE#";

        public static String yesA = "$Y#";

        public static String noA = "$N#";

        public static String repeatLast = "$R#";
        public static String errorMsg = "$ERR#";


    }
}
