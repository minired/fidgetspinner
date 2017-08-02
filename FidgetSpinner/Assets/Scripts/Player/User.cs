using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;
namespace Fidget.Player
{
    public class User : Singleton<User>
    {
        PlayerPrefsUtil putil;
        protected User()
        {
            putil = new PlayerPrefsUtil();
        }


        public int Exp
        {
            get
            {
                return putil.GetInt("exp");
            }
            set
            {
                putil.SetInt("exp", value);
            }
        }

        public ulong Gold
        {
            get
            {
                ulong num;
                bool res = ulong.TryParse(putil.GetString("gold"), out num);
                if (res == false)
                {
                    return 0;
                }
                return num;
            }
            set
            {
                putil.SetString("gold", value.ToString());
            }
        }


        


        public int MaxFidgetSpinnerCount
        {
            get
            {
                return putil.GetInt("maxfidgetcount");
            }
            set
            {
                putil.SetInt("maxfidgetcount", value);
            }
        }

        



        public string DeviceID
        {
            get
            {
                return SystemInfo.deviceUniqueIdentifier;
            }
        }


    }
}