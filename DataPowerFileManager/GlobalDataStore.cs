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

    }
}
