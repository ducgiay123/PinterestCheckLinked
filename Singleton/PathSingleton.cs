using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeViaFBTool.Singleton
{
    public class PathSingleton
    {
        public static string ConfigPath
        {
            get
            {
                return Environment.CurrentDirectory + "\\config";
            }
        }
        public static string UISettingConfigPath
        {
            get
            {
                return Path.Combine(ConfigPath, "UISettingsConfig.ini");
            }
        }
    }
}
