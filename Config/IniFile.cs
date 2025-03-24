using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ChangeViaFBTool.Config
{
    public class IniFile   // revision 11
    {
        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            //if (!Directory.Exists(Path))
            //{
            //    Directory.CreateDirectory(Path);
            //}

            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
        public static Dictionary<string, string> ConvertQueueToDictionary(ConcurrentQueue<string> queue)
        {
            Dictionary<string, string> emailPasswordDict = new Dictionary<string, string>();

            while (queue.TryDequeue(out string item)) // Dequeue safely
            {
                string[] parts = item.Split('|');
                if (parts.Length == 2) // Ensure valid format
                {
                    emailPasswordDict[parts[0]] = parts[1]; // Store email as key, password as value
                }
            }

            return emailPasswordDict;
        }
    }
}
