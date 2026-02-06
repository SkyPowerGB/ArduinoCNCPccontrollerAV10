using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller.classes
{
    public class SettingsModel
    {
      public  int ArduSoftwareVersion { get; set; } = 12;
        public double x_resolution { get; set; } = 0.04;
        public double y_resolution { get; set; } = 0.04;
        public double z_resolution { get; set; } = 0.01;
        public double z_draw { get; set; } = 5;

        // scale to 1step=1px;
        public double scaleFactorX { get; set; } = 25;
        public double scaleFactorY { get; set; } = 25;
        public double scaleFactorZ { get; set; } = 25;

        public int machineWorkspaceMMX { get; set; } = 100;
        public int machineWorkspaceMMY { get; set; } = 100;

        public int machineWorkspaceScaledX { get; set; } = 2500;
        public int machineWorkspaceScaledY { get; set; } = 2500;


        public int lineSize { get; set; } = 1;
        public Color lineColor { get; set; } = Color.Blue;
    }
}
