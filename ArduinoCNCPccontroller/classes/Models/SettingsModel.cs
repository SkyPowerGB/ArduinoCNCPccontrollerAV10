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


        // resolution....
        public double x_resolution { get; set; } = 0.04;
        public double y_resolution { get; set; } = 0.04;
        public double z_resolution { get; set; } = 0.01;

        // draw when Z is 
        public double z_draw { get; set; } = 5;
        public bool z_show {  get; set; }=false;

      // machine virtual workspace  :mini preview (mistakeInName
        public int miniPreviewXmm { get; set; } = 100;
        public int miniPreviewYmm { get; set; } = 100;

     // machine real workspace
       public int fullPreviewWorkspcMMxy {  get; set; } = 200;
    

        public int lineSize { get; set; } = 1;
        public int lineSizeLive { get; set;}=1;
        public Color lineColor { get; set; } = Color.Blue;
        public Color liveDrawColor { get; set; } = Color.Red;

        public Color zColor {  get; set; }= Color.Gray;
        
        public bool Debug { get; set; }=false;
        public bool Pos { get; set; } = true;
        public bool livePreview {  get; set; } = false;

        public bool motorsOff {  get; set; } = false;

        public int spindleRPM { get; set; } = 20000;
        public int baudRate { get; set; } = 9600;

        public string oldPort {  get; set; }
    }
}
