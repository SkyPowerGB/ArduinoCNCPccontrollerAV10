using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller.classes
{
    public static class StateManager
    {
        public static bool IsConnected { get; set; } = false;
        public static bool ProcessRunning {get; set; } = false;
        public static bool LockControls {get; set;   }=false;


    }
}
