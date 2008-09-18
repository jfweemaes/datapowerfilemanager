using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPowerFileManager
{
    class GlobalDataStore
    {
        private static GlobalDataStore _instance = new GlobalDataStore();
        
        //Used for Login DataPower Form
        private string _strDataPowerHost = "";
        private string _strDataPowerPort = "";
        private string _strDataPowerUserName = "";
        private string _strDataPowerPassword = "";

        //Used for Encryption/Decryption Suite
        private string _strPlainText = "";    // original plaintext
        private string _strCipherText = "";                 // encrypted text
        private string _strPassPhrase = "Pas5pr@se";        // can be any string
        private string _strInitVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes        

        static GlobalDataStore()
        {
        }       

        public static GlobalDataStore GetInstance()
        {
            return _instance;
        }

        public string strDataPowerHost { get { return _strDataPowerHost; } set { _strDataPowerHost = value; } }
        public string strDataPowerPort { get { return _strDataPowerPort; } set { _strDataPowerPort = value; } }
        public string strDataPowerUserName { get { return _strDataPowerUserName; } set { _strDataPowerUserName = value; } }
        public string strDataPowerPassword { get { return _strDataPowerPassword; } set { _strDataPowerPassword = value; } }
        public string strPlainText { get { return _strPlainText; } set { _strPlainText = value; } }
        public string strCipherText { get { return _strCipherText; } set { _strCipherText = value; } }
        public string strPassPhrase { get { return _strPassPhrase; } set { _strPassPhrase = value; } }
        public string strInitVector { get { return _strInitVector; } set { _strInitVector = value; } }
        
    }
}
