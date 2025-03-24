using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PinterestCheckLinked.Singleton;

namespace PinterestCheckLinked.Config
{
    public class UIConfig
    {
        public static IniFile UISettingManager = new IniFile(PathSingleton.UISettingConfigPath);
        public static int Threads
        {
            get
            {
                string payload = UISettingManager.Read("threads");
                if (string.IsNullOrEmpty(payload))
                {
                    return 0;
                }
                return int.Parse(payload);
            }
            set
            {
                UISettingManager.Write("threads", value.ToString());
            }
        }
        public static int ProxyType
        {
            get
            {
                string payload = UISettingManager.Read("proxytype");
                if (string.IsNullOrEmpty(payload))
                {
                    return 0;
                }
                return int.Parse(payload);
            }
            set
            {
                UISettingManager.Write("proxytype", value.ToString());
            }
        }
        public static bool UseProxy
        {
            get
            {
                string payload = UISettingManager.Read("useproxy");
                if (string.IsNullOrEmpty(payload))
                {
                    return false;
                }
                return bool.Parse(payload);
            }
            set
            {
                UISettingManager.Write("useproxy", value.ToString());
            }
        }
        /// <summary>
        /// 1 : Checklinked
        /// <br></br>
        /// 2 : Check Die

        /// </summary>
        public static int TaskType
        {
            get
            {
                string val = UISettingManager.Read("tasktype");
                if (string.IsNullOrEmpty(val))
                {
                    return 1;
                }
                return int.Parse(val);
            }
            set
            {
                UISettingManager.Write("tasktype", value.ToString());
            }
        }
    }

}
