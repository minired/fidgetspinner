using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Fidget.Common
{
    public class PlayMission : Singleton<PlayMission>
    {
        readonly int MaxSaveCount = 30;
        readonly string[] MissionAddress = { "MapMission", "CharacterMission", "CoinMission", "LevelMission", "StageFloorMission", "ReviveMission" };
        readonly string[] MissionSuccessAddress = { "MapMissionSuccess", "CharacterMissionSuccess", "CoinMissionSuccess", "LevelMissionSuccess", "StageFloorMissionSuccess", "ReviveMissionSuccess" };
        protected PlayMission()
        {
        }


        public bool[] GetMapMissionSuccess()
        {
            return GetArrayFromSavedInt(MissionSuccessAddress[0]);
        }
        public void SetMapMissionSuccess(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionSuccessAddress[0]);
        }


        public bool[] GetCharacterMissionSuccess()
        {
            return GetArrayFromSavedInt(MissionSuccessAddress[1]);
        }
        public void SetCharacterMissionSuccess(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionSuccessAddress[1]);
        }

        public bool[] GetCoinMissionSuccess()
        {
            return GetArrayFromSavedInt(MissionSuccessAddress[2]);
        }
        public void SetCoinMissionSuccess(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionSuccessAddress[2]);
        }


        public bool[] GetLevelMissionSuccess()
        {
            return GetArrayFromSavedInt(MissionSuccessAddress[3]);
        }
        public void SetLevelMissionSuccess(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionSuccessAddress[3]);
        }


        public bool[] GetStageFloorMissionSuccess()
        {
            return GetArrayFromSavedInt(MissionSuccessAddress[4]);
        }
        public void SetStageFloorMissionSuccess(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionSuccessAddress[4]);
        }


        public bool[] GetReviveMissionSuccess()
        {
            return GetArrayFromSavedInt(MissionSuccessAddress[5]);
        }
        public void SetReviveMissionSuccess(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionSuccessAddress[5]);
        }




        public bool[] GetMapMission()
        {
            return GetArrayFromSavedInt(MissionAddress[0]);
        }
        public void SetMapMission(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionAddress[0]);
        }


        public bool[] GetCharacterMission()
        {
            return GetArrayFromSavedInt(MissionAddress[1]);
        }
        public void SetCharacterMission(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionAddress[1]);
        }

        public bool[] GetCoinMission()
        {
            return GetArrayFromSavedInt(MissionAddress[2]);
        }
        public void SetCoinMission(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionAddress[2]);
        }


        public bool[] GetLevelMission()
        {
            return GetArrayFromSavedInt(MissionAddress[3]);
        }
        public void SetLevelMission(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionAddress[3]);
        }


        public bool[] GetStageFloorMission()
        {
            return GetArrayFromSavedInt(MissionAddress[4]);
        }
        public void SetStageFloorMission(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionAddress[4]);
        }


        public bool[] GetReviveMission()
        {
            return GetArrayFromSavedInt(MissionAddress[5]);
        }
        public void SetReviveMission(int index, bool check)
        {
            SetOneBitFromSavedInt(index, check, MissionAddress[5]);
        }



        void SetOneBitFromSavedInt(int index, bool check, string address)
        {
            if (index >= MaxSaveCount)
                return;
            try
            {
                bool[] arr = GetArrayFromSavedInt(address);
                arr[index] = check;
                string binaryValue = MakeBinaryStringFromArray(arr);
                SaveIntFromBinaryString(address, binaryValue);
            }
            catch
            {

            }
        }

        bool[] GetArrayFromSavedInt(string address)
        {
            bool[] arr = new bool[30];

            try
            {
                //int data = ZPlayerPrefs.GetInt(address);
                int data = 0;
                string temp = Convert.ToString(data, 2).PadLeft(30, '0');
                for (int i = 0; i < 30; ++i)
                {
                    if (temp[i] == '1')
                        arr[i] = true;
                    else
                        arr[i] = false;
                }
            }
            catch
            {

            }

            return arr;
        }

        string MakeBinaryStringFromArray(bool[] arr)
        {
            string result = "";
            foreach (bool data in arr)
            {
                if (data)
                    result += "1";
                else
                    result += "0";
            }
            return result;
        }
        void SaveIntFromBinaryString(string address, string binaryValue)
        {
            int resultInt = Convert.ToInt32(binaryValue, 2);
            //ZPlayerPrefs.SetInt(address, resultInt);
        }


        public void CheckCoinMission(long money)
        {
            int checkIndex = -1;
            if (money >= 500000)
            {
                checkIndex = 8;
            }
            else if (money >= 300000)
            {
                checkIndex = 7;
            }
            else if (money >= 100000)
            {
                checkIndex = 6;
            }
            else if (money >= 50000)
            {
                checkIndex = 5;
            }
            else if (money >= 30000)
            {
                checkIndex = 4;
            }
            else if (money >= 10000)
            {
                checkIndex = 3;
            }
            else if (money >= 5000)
            {
                checkIndex = 2;
            }
            else if (money >= 3000)
            {
                checkIndex = 1;
            }
            else if (money >= 1000)
            {
                checkIndex = 0;
            }
            if (checkIndex > -1)
                CheckCoinMissionFromIndex(checkIndex);
        }

        void CheckCoinMissionFromIndex(int index)
        {
            try
            {
                bool[] arr = GetCoinMissionSuccess();
                for (int i = 0; i <= index; ++i)
                {
                    if (arr[i] == false)
                    {
                        SetCoinMission(i, true);
                    }
                }
            }
            catch
            {

            }
        }

        public void CheckLevelMission(int level)
        {
            int checkIndex = -1;
            if (level >= 500)
            {
                checkIndex = 9;
            }
            else if (level >= 450)
            {
                checkIndex = 8;
            }
            else if (level >= 400)
            {
                checkIndex = 7;
            }
            else if (level >= 350)
            {
                checkIndex = 6;
            }
            else if (level >= 300)
            {
                checkIndex = 5;
            }
            else if (level >= 250)
            {
                checkIndex = 4;
            }
            else if (level >= 200)
            {
                checkIndex = 3;
            }
            else if (level >= 150)
            {
                checkIndex = 2;
            }
            else if (level >= 100)
            {
                checkIndex = 1;
            }
            else if (level >= 50)
            {
                checkIndex = 0;
            }

            if (checkIndex > -1)
                CheckLevelMissionFromIndex(checkIndex);
        }

        void CheckLevelMissionFromIndex(int index)
        {
            try
            {
                bool[] arr = GetLevelMissionSuccess();
                for (int i = 0; i <= index; ++i)
                {
                    if (arr[i] == false)
                    {
                        SetLevelMission(i, true);
                    }
                }
            }
            catch
            {

            }
        }



        public void CheckStageFloorMission(int floor, int stageIndex)
        {
            int checkIndex = -1;
            if (floor >= 200)
            {
                checkIndex = (stageIndex * 2) + 1;
            }
            else if (floor >= 100)
            {
                checkIndex = (stageIndex * 2);
            }
            if (checkIndex > -1)
                CheckStageFloorFromIndex(checkIndex);
        }


        void CheckStageFloorFromIndex(int index)
        {
            try
            {
                bool[] arr = GetStageFloorMissionSuccess();
                if (arr[index] == false)
                {
                    SetStageFloorMission(index, true);
                }
                if (index % 2 == 1 && arr[index - 1] == false)
                {
                    SetStageFloorMission(index - 1, true);
                }
            }
            catch
            {

            }
        }






        public void CheckTotalReviveMission(int reviveCount)
        {
            int checkIndex = -1;
            if (reviveCount >= 1000)
            {
                checkIndex = 4;
            }
            else if (reviveCount >= 500)
            {
                checkIndex = 3;
            }
            else if (reviveCount >= 100)
            {
                checkIndex = 2;
            }
            else if (reviveCount >= 50)
            {
                checkIndex = 1;
            }
            else if (reviveCount >= 10)
            {
                checkIndex = 0;
            }

            if (checkIndex > -1)
                CheckTotalReviveFromIndex(checkIndex);
        }

        void CheckTotalReviveFromIndex(int index)
        {
            try
            {
                bool[] arr = GetReviveMissionSuccess();
                for (int i = 0; i <= index; ++i)
                {
                    if (arr[i] == false)
                    {
                        SetReviveMission(i, true);
                    }
                }
            }
            catch
            {

            }
        }

    }
}