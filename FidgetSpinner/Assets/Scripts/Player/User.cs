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



        public int EquipIndex
        {
            get
            {
                return putil.GetInt("equipindex");
            }
            set
            {
                putil.SetInt("equipindex", value);
            }
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

        public ulong Coin
        {
            get
            {
                ulong num;
                bool res = ulong.TryParse(putil.GetString("coin"), out num);
                if (res == false)
                {
                    return 0;
                }
                return num;
            }
            set
            {
                putil.SetString("coin", value.ToString());
            }
        }


        public int Score
        {
            get
            {
                return putil.GetInt("score");
            }
            set
            {
                putil.SetInt("score", value);
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