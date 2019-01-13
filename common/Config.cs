using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spoonson.Common
{
    /// <summary>
    /// 配置参数管理
    /// </summary>
    class Config
    {
        private static Newtonsoft.Json.Linq.JObject m_config = null;
        
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="jsonfile"></param>
        public static void LoadConfigsFromFile(string jsonfile)
        {
            m_config = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(jsonfile));
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="jsonstring"></param>
        public static void LoadConfigs(string jsonstring)
        {
            m_config = Newtonsoft.Json.Linq.JObject.Parse(jsonstring);
        }

        public static void Load()
        {
            string configPath = System.IO.Path.Combine(Environment.CurrentDirectory, "config.json");
            if (System.IO.File.Exists(configPath))
            {
                LoadConfigsFromFile(configPath);
            }
            else
            {
                Spoonson.Common.Config.LoadConfigs(Spoonson.Apps.SuperTerm.Properties.Resources.config);
            }
        
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="jsonfile"></param>
        public static void SaveConfigs(string jsonfile)
        {
            if (m_config != null)
            {
                System.IO.File.WriteAllText(jsonfile, m_config.ToString(), Encoding.UTF8);
            }
        }

        public static void Save()
        {
            SaveConfigs(System.IO.Path.Combine(Environment.CurrentDirectory, "config.json"));
        }

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Newtonsoft.Json.Linq.JToken GetValue(string key)
        {
            if (m_config != null)
            {
                return m_config[key];
            }
            return null;
        }
    }
}
