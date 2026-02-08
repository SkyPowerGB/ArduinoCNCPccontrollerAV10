using ArduinoCNCPccontroller.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoCNCPccontroller.Forms
{
    public partial class SettingsForm : Form
    {
        JsonModelManager modelManager;
        public SettingsForm()
        {
            modelManager = new JsonModelManager();
            InitializeComponent();

            loadSettings();

        }
        private void loadSettings()
        {

            tbXres.Text = modelManager.Settings.x_resolution.ToString();
            tbYres.Text = modelManager.Settings.y_resolution.ToString();
            tbZres.Text = modelManager.Settings.z_resolution.ToString();
            tbSpindleRPM.Text = modelManager.Settings.spindleRPM.ToString();


            tbPrevXmm.Text = modelManager.Settings.miniPreviewXmm.ToString();
            tbPreviYmm.Text = modelManager.Settings.miniPreviewYmm.ToString();

            tbFullPreviXmm.Text = modelManager.Settings.fullPreviewWorkspcMMxy.ToString();

            tbZlimit.Text = modelManager.Settings.z_draw.ToString();
            tbZcolor.BackColor = modelManager.Settings.zColor;
            chckZshow.Checked = modelManager.Settings.z_show;



            tbPrevClr.BackColor = modelManager.Settings.lineColor;
            tbLivePrevClr.BackColor = modelManager.Settings.liveDrawColor;
            tbCursorPrevWidth.Text = modelManager.Settings.lineSize.ToString();

            tbLivePrevCursorWidth.Text = modelManager.Settings.lineSizeLive.ToString();
            tbCursorPrevWidth.Text = modelManager.Settings.lineSize.ToString();




            tbBaudRate.Text = modelManager.Settings.baudRate.ToString();

            chckDebug.Checked = modelManager.Settings.Debug;
            chckPos.Checked = modelManager.Settings.Pos;
            chckMotorsOff.Checked = modelManager.Settings.motorsOff;
            chckLivePrev.Checked = modelManager.Settings.livePreview;


        }
        public void ShowError(string message)
        {
            MessageBox.Show(
          message,
          "Error",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error);

        }
        private int getInt(string value)
        {
            try { 
            int v= int.Parse(value);

            return v;
            }catch(Exception E)
            {
                return 0;
            }
        }
        private double GetDouble(string value)
        {
            value = value.Replace(',', '.');

            double.TryParse(
                value,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out double result
            );

            return result;
        }
        public void Inform(string message)
        {
            MessageBox.Show(
    message, // message
    "Information",                             // title
    MessageBoxButtons.OK,                       // just OK button
    MessageBoxIcon.Information                 // info icon
);
        }
        public bool ConfirmWindow(string message)
        {
            DialogResult result = MessageBox.Show(
         message,   // message
         "Confirmation",               // title
         MessageBoxButtons.YesNo,      // buttons
         MessageBoxIcon.Question       // icon
     );

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void tbXres_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbYres_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbZres_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbSpindleRPM_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPrevXmm_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPreviYmm_TextChanged(object sender, EventArgs e)
        {

        }

     

       

        private void previewColorSlct(object sender, MouseEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbPrevClr.BackColor = dialog.Color;
            }
        }

        private void livePreviewColorSlct(object sender, MouseEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbLivePrevClr.BackColor = dialog.Color;
            }

        }

        private void zColorSlct(object sender, MouseEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            var result= dialog.ShowDialog();
            if (result == DialogResult.OK) { 
                tbZcolor.BackColor = dialog.Color;
                }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            modelManager.Settings.x_resolution=GetDouble(tbXres.Text);
            modelManager.Settings.y_resolution=GetDouble(tbYres.Text);
            modelManager.Settings.z_resolution=GetDouble(tbZres.Text);
            modelManager.Settings.spindleRPM=getInt(tbSpindleRPM.Text);


    
      
            modelManager.Settings.miniPreviewXmm=getInt(tbPrevXmm.Text);
            modelManager.Settings.miniPreviewYmm=getInt(tbPreviYmm.Text);

            modelManager.Settings.fullPreviewWorkspcMMxy=getInt(tbFullPreviXmm.Text);


           
            modelManager.Settings.z_draw=GetDouble(tbZlimit.Text);
            modelManager.Settings.zColor=tbZcolor.BackColor;
            modelManager.Settings.z_show= chckZshow.Checked;



           
            modelManager.Settings.lineColor= tbPrevClr.BackColor;
            modelManager.Settings.liveDrawColor=tbLivePrevClr.BackColor;

           
            modelManager.Settings.lineSize=getInt(tbCursorPrevWidth.Text);

            modelManager.Settings.lineSizeLive=getInt( tbLivePrevCursorWidth.Text );
            modelManager.Settings.lineSize=getInt( tbCursorPrevWidth.Text);




      
            modelManager.Settings.baudRate=getInt(tbBaudRate.Text);

            modelManager.Settings.Debug= chckDebug.Checked ;
            modelManager.Settings.Pos= chckPos.Checked ;
            modelManager.Settings.motorsOff= chckMotorsOff.Checked ;
            modelManager.Settings.livePreview= chckLivePrev.Checked ;




            modelManager.Save();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
