using ArduinoCNCPccontroller.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller.classes {

    /*
     ACTS AS CENTRAL CLASS TO AVOID PROP DRILLING
     
     
     */
    internal static class Bootstrapper {

        
     public static   Feedback Feedback { get; set; }
     public static   ArduinoComunicatorV3 comunicatorV3 { get; set; }

     public static Commander Commander { get; set; }
     public static   CNC_PC_controller mainUI { get; set; }
     
     public static Painter painter { get; set; }
     public static FullPreview FullPreview { get; set; }

     public static JsonModelManager modelManager { get; set; }

     public static SettingsForm SettingsForm { get; set; }


    }
}
