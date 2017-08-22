using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fidget.Common;

namespace Fidget.Player
{
    public class User : Singleton<User>
    {
        PlayerPrefsUtil putil;

        bool isSound = true;

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

        public int ScoreGameSpin
        {
            get
            {
                return putil.GetInt("scoregamespin");
            }
            set
            {
                putil.SetInt("scoregamespin", value);
            }
        }


        public int HighScore
        {
            get
            {
                return putil.GetInt("highscore");
            }
            set
            {
                putil.SetInt("highscore", value);
            }
        }

        public string GoogleId
        {
            get
            {
                return putil.GetString("GoogleId");
            }
            set
            {
                putil.SetString("GoogleId", value);
            }
        }


        public int HighScoreLoop
        {
            get
            {
                return putil.GetInt("highscoreloop");
            }
            set
            {
                putil.SetInt("highscoreloop", value);
            }
        }

        public int HighScoreGameSpin
        {
            get
            {
                return putil.GetInt("highscoregamespin");
            }
            set
            {
                putil.SetInt("highscoregamespin", value);
            }
        }


        public int GetFidgetSpinnerLevel(int index)
        {
            return putil.GetInt("fidgetspinnerlevel" + index.ToString());
        }

        public void SetFidgetSpinnerLevel(int index, int val)
        {
            putil.SetInt("fidgetspinnerlevel" + index.ToString(), val);
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



        public bool Alarm
        {
            get
            {
                if (putil.GetInt("alarm") < 1)
                    return false;
                else
                    return true;
            }
            set
            {
                if(value)
                    putil.SetInt("alarm", 1);
                else
                    putil.SetInt("alarm", 0);
            }
        }

        public bool Vibration
        {
            get
            {
                if (putil.GetInt("vibration") < 1)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                    putil.SetInt("vibration", 1);
                else
                    putil.SetInt("vibration", 0);
            }
        }

        public bool Sound
        {
            get
            {
                if (putil.GetInt("sound") < 1)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                {
                    putil.SetInt("sound", 1);
                    isSound = true;
                }
                else
                {
                    putil.SetInt("sound", 0);
                    isSound = false;
                }
                
            }
        }


        public bool IsSound
        {
            get
            {
                return isSound;
            }
            set
            {
                isSound = value;
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