using ArduinoCNCPccontroller.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCNCPccontroller.classes
{
    internal class JsonModelManager
    {
        private readonly string appFolder;
        private readonly string dataFile;

        

        public SettingsModel Settings { get; private set; }


        public JsonModelManager()
        {
            appFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "CNCpcControler",
                "Settings"
            );
            Directory.CreateDirectory(appFolder);

            dataFile = Path.Combine(appFolder, "settings.json");

            Load();
        }

        public void Load()
        {
            if (!File.Exists(dataFile))
            {
                Settings = new SettingsModel();
                return;
            }

            string json = File.ReadAllText(dataFile);
            Settings = JsonConvert.DeserializeObject<SettingsModel>(json) ?? new SettingsModel();
        }
        public void Save()
        {
            string json = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            string temp = dataFile + ".tmp";

            File.WriteAllText(temp, json);

            
            if (File.Exists(dataFile))
            {
                File.Replace(temp, dataFile, null);
            }
            else
            {
                File.Move(temp, dataFile);
            }
        }



    }



}
