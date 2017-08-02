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


        public int Gem
        {
            get
            {
                return putil.GetInt("gem");
            }
            set
            {
                putil.SetInt("gem", value);
            }
        }


        public int MaxBirdCount
        {
            get
            {
                return putil.GetInt("maxbirdcount");
            }
            set
            {
                putil.SetInt("maxbirdcount", value);
            }
        }


        public int PurchasableBirdLevel
        {
            get
            {
                return putil.GetInt("purchasablebirdlevel");
            }
            set
            {
                putil.SetInt("purchasablebirdlevel", value);
            }
        }



        public int GetOwnBirdCount(int index)
        {
            string code = "birdcount" + index.ToString();
            int count = putil.GetInt(code);
            if (count < 0)
            {
                putil.SetInt(code, 0);
                return 0;
            }

            return count;
        }

        public void SetOwnBirdCount(int index, int count)
        {
            string code = "birdcount" + index.ToString();
            putil.SetInt(code, count);
        }






        public int GetPossessionSlotType(int index)
        {
            string code = "slottype" + index.ToString();
            int type = putil.GetInt(code);
            if (type < 0)
            {
                putil.SetInt(code, 0);
                return 0;
            }

            return type;
        }

        public void SetPossessionSlotType(int index, int typeId)
        {
            string code = "slottype" + index.ToString();
            putil.SetInt(code, typeId);
        }


        public int GetPossessionSlotExp(int index)
        {
            string code = "slotexp" + index.ToString();
            int type = putil.GetInt(code);
            if (type < 0)
            {
                putil.SetInt(code, 0);
                return 0;
            }

            return type;
        }


        public void SetPossessionSlotExp(int index, int exp)
        {
            string code = "slotexp" + index.ToString();
            putil.SetInt(code, exp);
        }


        public int PosseissionSlotCount
        {
            get
            {
                return putil.GetInt("slotcount");
            }
            set
            {
                putil.SetInt("slotcount", value);
            }
        }


        public int EggCount
        {
            get
            {
                return putil.GetInt("eggcount");
            }
            set
            {
                putil.SetInt("eggcount", value);
            }
        }



        public int FeverRegenTimeLevel
        {
            get
            {
                return putil.GetInt("feverregentimelevel");
            }
            set
            {
                putil.SetInt("feverregentimelevel", value);
            }
        }


        public int FeverMaxTimeLevel
        {
            get
            {
                return putil.GetInt("fevermaxtimelevel");
            }
            set
            {
                putil.SetInt("fevermaxtimelevel", value);
            }
        }

        public int EggRateLevel
        {
            get
            {
                return putil.GetInt("eggratelevel");
            }
            set
            {
                putil.SetInt("eggratelevel", value);
            }
        }


        public int EggMaxLevel
        {
            get
            {
                return putil.GetInt("eggmaxlevel");
            }
            set
            {
                putil.SetInt("eggmaxlevel", value);
            }
        }


        public int LeaveMaxTimeLevel
        {
            get
            {
                return putil.GetInt("leavemaxtimelevel");
            }
            set
            {
                putil.SetInt("leavemaxtimelevel", value);
            }
        }


        public int LeaveLevel
        {
            get
            {
                return putil.GetInt("leavelevel");
            }
            set
            {
                putil.SetInt("leavelevel", value);
            }
        }


        public string LastRewardTime
        {
            get
            {
                return putil.GetString("lastrewardtime");
            }
            set
            {
                putil.SetString("lastrewardtime", value);
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