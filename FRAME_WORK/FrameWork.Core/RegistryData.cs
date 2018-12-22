using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// it's required for reading/writing into the registry:
using Microsoft.Win32;  

namespace FrameWork.Core
{
    public class RegistryData
    {
        public RegistryData()
        {
            
        }

        public string Read(string KeyName)
        {
            object result = null;

            //Select the RegistryHive you wish to read from
            RegistryKey key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "");

            //Select the path within the hive
            RegistryKey subkey = key.OpenSubKey("System\\EIP");

            //If the subkey is null, it means that the path within the hive doesn't exist
            if (subkey != null)
                //Read the key
                result = subkey.GetValue(KeyName);

            if (result == null)
                throw new ApplicationException("Error accessing System Registry. Unable to find value for \"" + KeyName + "\"");

            return result.ToString().Trim();
        }
    }
}
