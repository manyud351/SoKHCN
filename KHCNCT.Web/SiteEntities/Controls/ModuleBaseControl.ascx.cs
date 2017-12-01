using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KHCNCT.Data;

namespace KHCNCT.Web.SiteEntities.Controls
{
    public partial class ModuleBaseControl : BaseControl
    {
        private string _modKey;
        public string ModKey
        {
            get { return _modKey; }
            set { _modKey = value; }
        }

        //private string _defaultModKey;
        //public string DefaultModKey
        //{
        //    get
        //    { 
                
        //    }
        //}

        private string _modConfigName;
        protected string ModConfigName
        {
            get { return _modConfigName; }
            set { _modConfigName = value; }
        }

        private List<ModuleConfiguration> _Configs;
        public List<ModuleConfiguration> ModuleConfigs
        {
            get
            {
                if (_Configs == null) _Configs = db.ModuleConfigurations.Where<ModuleConfiguration>(mdc => mdc.ModKey == ModKey).ToList<ModuleConfiguration>();
                return _Configs;
            }
        }

        public ModuleConfiguration GetModuleConfig(string configName)
        { 
            ModuleConfiguration configValue = null;
            try
            {
                if (ModuleConfigs != null) configValue = ModuleConfigs.SingleOrDefault<ModuleConfiguration>(mdc => mdc.ModConfigName == configName);
            }
            catch (Exception ex)
            { 
                //
            }
            return configValue;
        }

        public string GetModuleConfigValue(string configName)
        {
            ModuleConfiguration configValue = null;
            string sConfigValue = "";
            try
            {
                if (ModuleConfigs != null) configValue = ModuleConfigs.SingleOrDefault<ModuleConfiguration>(mdc => mdc.ModConfigName == configName);
            }
            catch (Exception ex)
            {
                //
            }
            if (configValue != null) sConfigValue = configValue.Value;
            return sConfigValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}