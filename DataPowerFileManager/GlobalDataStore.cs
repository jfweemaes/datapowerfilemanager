using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPowerFileManager
{
    class GlobalDataStore
    {
        private static GlobalDataStore _instance = new GlobalDataStore();
               
        private string _strDataPowerHost = "";
        private string _strDataPowerPort = "";
        private string _strDataPowerUserName = "";
        private string _strDataPowerPassword = "";


        static GlobalDataStore()
        {
        }       

        public static GlobalDataStore GetInstance()
        {
            return _instance;
        }

        public string strDataPowerHost
        {
            get
            {
                return _strDataPowerHost;
            }
            set
            {
                _strDataPowerHost = value;
            }
        }

        public string strDataPowerPort
        {
            get
            {
                return _strDataPowerPort;
            }
            set
            {
                _strDataPowerPort = value;
            }
        }

        public string strDataPowerUserName
        {
            get
            {
                return _strDataPowerUserName;
            }
            set
            {
                _strDataPowerUserName = value;
            }
        }

        public string strDataPowerPassword
        {
            get
            {
                return _strDataPowerPassword;
            }
            set
            {
                _strDataPowerPassword = value;
            }
        }

    }
}
