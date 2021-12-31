using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace GymeeDestkopApp.Services {

    class Configuration {
        public string branch;
        public int camera_width;
        public int camera_height;
        public int camera_fps;
        public bool greyscale;
        public string drive_creds;
        public string record_folder;
        public string drive_folder_id;
    }

    //this is a singleton.
    //yeah
    class ConfigurationService {
        Configuration cfg;
        private ConfigurationService() {
            var fileContent = File.ReadAllText("GymeeConfig.json");
            cfg = JsonConvert.DeserializeObject<Configuration>(fileContent);
        }

        private static ConfigurationService instance;

        public static ConfigurationService GetConfiguration() {
            if(instance == null) {
                instance = new ConfigurationService();
            }

            return instance;
        }

        public string Branch => cfg.branch;
        public int Width => cfg.camera_width;
        public int Height => cfg.camera_height;
        public int Fps => cfg.camera_fps;
        public bool IsGreyscale => cfg.greyscale;
        public string DriveCreds => cfg.drive_creds;
        public string RecordFolder => cfg.record_folder;
        public string DriveFolderId => cfg.drive_folder_id;
    }
}
