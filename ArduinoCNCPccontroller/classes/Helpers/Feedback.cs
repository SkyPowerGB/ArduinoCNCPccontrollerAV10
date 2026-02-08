using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;



namespace ArduinoCNCPccontroller.classes {
    internal class Feedback {
        private CNC_PC_controller controllerForm;
        private JsonModelManager manager;

        public Feedback(CNC_PC_controller controller,JsonModelManager manager) {
            this.controllerForm = controller;
            this.manager = manager;
        }

        public Feedback() {
            this.manager = new JsonModelManager();
        }

        public Feedback(JsonModelManager manager) {
            this.manager = manager;
        }

        public void LogLn(string text) {
            Console.WriteLine(text);

            if(controllerForm == null)
                return;

            text += Environment.NewLine;

            if(controllerForm.txtRBdebugConsole.InvokeRequired) {
                controllerForm.txtRBdebugConsole.Invoke(
                    new Action(() => controllerForm.txtRBdebugConsole.AppendText(text))
                );
            }
            else {
                controllerForm.txtRBdebugConsole.AppendText(text);
            }
        }

        public void ShowError(string message) {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public void Inform(string message) {
            MessageBox.Show(
                message,
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        public bool ConfirmWindow(string message) {
            DialogResult result = MessageBox.Show(
                message,
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            return result == DialogResult.Yes;
        }
    }
}