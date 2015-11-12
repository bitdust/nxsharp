using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Management;
using System.Resources;


namespace gui
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            NetworkInterfaceAvaliable.List();            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Cfg.Load();
            Application.Run(new Form1());
            RefComm.StopAuthThread();
            return;
        }
        
    }
    public class Cfg
    {
        public static string username;
        public static string password;
        public static string device;
        public static bool store = false;
        public static bool auto = false;
        public static int mode = 0;
        public static string versionHEX; // 十六进制表示的版本号
        public static string H3C_key; // H3C加密密钥
        public static void Load()
        {
            //加载配置文件
            try
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                map.ExeConfigFilename = Application.StartupPath + "/NXSharp.exe.Config";
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map,ConfigurationUserLevel.None);
                AppSettingsSection app = config.AppSettings;
                Cfg.username = app.Settings["username"].Value;
                Cfg.password = app.Settings["password"].Value;
                Cfg.device = app.Settings["device"].Value;
                Cfg.store = Convert.ToBoolean(app.Settings["store"].Value);
                Cfg.auto = Convert.ToBoolean(app.Settings["auto"].Value);
                Cfg.mode = Convert.ToInt32(app.Settings["mode"].Value);
                Cfg.versionHEX = app.Settings["versionHEX"].Value;
                Cfg.H3C_key = app.Settings["H3C_key"].Value;
                return;
            }
            catch (Exception)
            {
                //避免配置文件丢失
                MessageBox.Show("没有找到配置文件！");
                return;
            }
        }
        public static void Commit()
        {
            //提交配置文件修改
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = Application.StartupPath + "/NXSharp.exe.Config";
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            AppSettingsSection app = config.AppSettings;
            app.Settings["username"].Value = username;
            app.Settings["password"].Value = password;
            app.Settings["device"].Value = device;
            app.Settings["store"].Value = store.ToString();
            app.Settings["auto"].Value = auto.ToString();
            app.Settings["mode"].Value = mode.ToString();
            app.Settings["versionHEX"].Value = versionHEX;
            app.Settings["H3C_key"].Value = H3C_key;
            config.Save();
            return;
        }
        public static string VersionParser(string HEXversion)
        {
            // 将16进制版本号转换为实际值
            char[] CHARversion = new char[16];
            string[] str = new string[16];
            for (int i = 0; i < HEXversion.Length / 2; i = i + 1)
            {
                str[i] = HEXversion.Substring(i * 2, 2);
                CHARversion[i] = (char)Convert.ToInt32(str[i], 16);
            }
            return new string(CHARversion);
        }
    }
    public class NetworkInterfaceAvaliable
    {
        public static Dictionary<string, string> adapters_dict = new Dictionary<string, string>();
        public static void List()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                //if (adapter.NetworkInterfaceType.ToString() == "Ethernet")
                //{
                NetworkInterfaceAvaliable.adapters_dict.Add(adapter.Description, adapter.Id);
                //}
            }
        }
        public static void RefreshDHCP(string device_id)
        {
            //需要手动添加对 System.Management 的引用
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();
            foreach (ManagementObject objMO in objMOC)
            {
                //Need to determine which adapter here with some kind of if() statement
                if (objMO["SettingID"].ToString() == device_id)
                {
                    objMO.InvokeMethod("ReleaseDHCPLease", null, null);
                    objMO.InvokeMethod("RenewDHCPLease", null, null);
                }
            }
        }
    }

    public class RefComm
    {
        [DllImport("xclient.dll", EntryPoint = "StartAuthThread", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void StartAuthThread(string username, string password, string device, string version, string H3C_key, int mode_config);
        [DllImport("xclient.dll", EntryPoint = "StopAuthThread", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void StopAuthThread();
        [DllImport("xclient.dll", EntryPoint = "ReadLog", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetLogBuffer();
        public static string ReadLog()
        {
            return Marshal.PtrToStringAnsi(GetLogBuffer());
        }

    }
}
