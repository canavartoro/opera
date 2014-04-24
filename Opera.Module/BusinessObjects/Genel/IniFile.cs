using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Mikrobar
{
    public sealed class IniFile
    {
        private string path = "C:\\";
        private string section = "CONFIG";

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public string Section
        {
            get { return section; }
            set { section = value; }
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        public IniFile() { }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section"></param>
        /// Section name
        /// <param name="Key"></param>
        /// Key Name
        /// <param name="Value"></param>
        /// Value Name
        public void Write(string dSection, string Key, string Value)
        {
            WritePrivateProfileString(dSection, Key, Value, this.path);
        }

        public void Write(string Key, string Value)
        {
            WritePrivateProfileString(section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string Read(string dSection, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(dSection, Key, "", temp, 255, this.path);
            return temp.ToString();

        }

        public string Read(string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, Key, "", temp, 255, this.path);
            return temp.ToString();

        }

        public string Read(string dSection, string Key, string dvalue)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(dSection, Key, "", temp, 255, this.path);
            if (i == 0)
            {
                Write(dSection, Key, dvalue);
                return dvalue;
            }
            return temp.ToString();

        }
    }
}
