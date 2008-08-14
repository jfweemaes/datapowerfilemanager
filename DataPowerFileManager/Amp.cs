using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataPowerFileManager.WebReference;
using DataPowerFileManager.com.prolifics.dpowerxi50;

namespace DataPowerFileManager
{
    class Amp
    {
        private static appmgmtprotocol _amp = new appmgmtprotocol();

        static Amp()
        {
        }

        public static appmgmtprotocol GetAppInstance()
        {
            return _amp;
        }
    }
}
