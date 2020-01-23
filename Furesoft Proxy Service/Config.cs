using DiscUtils.Registry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Furesoft.Proxy
{
    public static class Config
    {
        public static RegistryKey AppConfig;
        public static RegistryHive Hive;
        public static RegistryKey Querys;

        public static string[] GetAllQuerys()
        {
            var res = new List<string>();

            foreach (var name in Querys.GetValueNames())
            {
                res.Add(Querys.GetValue(name).ToString());
            }

            return res.ToArray();
        }

        public static void Init()
        {
            if (File.Exists(ConfigFileName))
            {
                Hive = new RegistryHive(new FileStream(ConfigFileName, FileMode.OpenOrCreate));
            }
            else
            {
                Hive = RegistryHive.Create(ConfigFileName);
            }

            //init keys
            Querys = Hive?.Root.CreateSubKey("Querys");
            AppConfig = Hive?.Root.CreateSubKey("AppConfig");

            Querys?.SetValue("frontpage", "content on \"furesoft.proxy\" display \"<h1> Furesoft Proxy is active</h1>\"");
        }

        public static void Reset()
        {
            File.Delete(ConfigFileName);
        }

        private static string ConfigFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "proxy.regfs");
    }
}