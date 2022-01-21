using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AccessZoom_try
{
    public class AppConfiguration
    {
        //가져오기
        public static string GetAppConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        //수정 
        public static void SetAppConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection cfgCollection = config.AppSettings.Settings;
            try
            {
                cfgCollection.Remove(key);
                cfgCollection.Add(key, value);

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch { }
        }
        //추가
        public static void AddAppConfig(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection cfgCollection = config.AppSettings.Settings;

            cfgCollection.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }
        //삭제
        public static void RemoveAppConfig(string key)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection cfgCollection = config.AppSettings.Settings;

            try
            {
                cfgCollection.Remove(key);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
            }
            catch { }
        }
        //검색 
        public static bool IsExistAppConfig(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
                return true;
            else
                return false;
            
        }
    }
}
