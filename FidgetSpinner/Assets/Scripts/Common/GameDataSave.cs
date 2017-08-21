using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace Fidget.Common
{
    public class GameDataSave
    {
        readonly string header = "3702d";

        public GameDataSave()
        {

        }

        public string Header
        {
            get
            {
                return header;
            }
        }



        public bool Save(string saveVal)
        {

            try
            {
                string[] splitArr = saveVal.Split(new char[] { ':' });
                if (splitArr.Length != 9)
                    return false;

                if (splitArr[0] != Header)
                    return false;

                long gold;
                int storycard, exp, totalfloor, openCharacter, openStage;
                if (!long.TryParse(splitArr[2], out gold))
                    return false;



                if (!int.TryParse(splitArr[3], out openCharacter))
                    return false;
                if (!int.TryParse(splitArr[4], out openStage))
                    return false;

                if (!int.TryParse(splitArr[5], out storycard))
                    return false;
                if (!int.TryParse(splitArr[6], out exp))
                    return false;
                if (!int.TryParse(splitArr[7], out totalfloor))
                    return false;


                //User.Instance.AvailableCharacter = User.Instance.GetAvailableCharacterFromString(Convert.ToString(openCharacter, 2));
                //User.Instance.AvailableStage = User.Instance.GetAvailableStageFromString(Convert.ToString(openStage, 2));
                //User.Instance.StoryCard = storycard;
                //User.Instance.TotalFloor = totalfloor;
                //User.Instance.Exp = exp;
                //User.Instance.Id = splitArr[1];
                //User.Instance.DeviceID = splitArr[8];
                //User.Instance.Gold = gold;

                //ExpTable exptable = new ExpTable();
                //User.Instance.Level = exptable.GetLevel(exp);
                //User.Instance.SelectCharacter = 0;
                //User.Instance.SelectStage = 0;

                return true;
            }
            catch
            {
                return false;
            }
        }

        //1 header
        //2 id
        //3 gold -> long
        //4 opencharacter
        //5 openstage
        //6 stroycard -> int
        //7 exp -> int
        //8 totalfloor -> int
        //9 DeviceID
        public string LoadString()
        {
            //string str = Header + ":" + User.Instance.Id + ":" + User.Instance.Gold + ":" + Convert.ToInt32(User.Instance.OpenCharacter, 2).ToString() + ":" +
            //    Convert.ToInt32(User.Instance.OpenStage, 2).ToString() + ":" + User.Instance.StoryCard.ToString() + ":" + User.Instance.Exp.ToString() + ":" + User.Instance.TotalFloor.ToString() + ":" + User.Instance.DeviceID;
            string str = "";
            return str;
        }
    }
}