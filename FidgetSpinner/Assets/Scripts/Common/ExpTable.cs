using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fidget.Common
{

    public struct LevelInfo
    {
        public int minlevel;
        public int maxlevel;
        public int exp;
        public LevelInfo(int minlevel, int maxlevel, int exp)
        {
            this.minlevel = minlevel;
            this.maxlevel = maxlevel;
            this.exp = exp;
        }
    }

    public class ExpTable
    {
        List<LevelInfo> levelinfoList = new List<LevelInfo>();
        public ExpTable()
        {
            levelinfoList.Clear();
            levelinfoList.Add(new LevelInfo(1, 10, 5000));
            levelinfoList.Add(new LevelInfo(11, 20, 5000));
            levelinfoList.Add(new LevelInfo(21, 30, 7000));
            levelinfoList.Add(new LevelInfo(31, 40, 7000));
            levelinfoList.Add(new LevelInfo(41, 50, 7000));
            levelinfoList.Add(new LevelInfo(51, 60, 9000));
            levelinfoList.Add(new LevelInfo(61, 70, 9000));
            levelinfoList.Add(new LevelInfo(71, 80, 9000));
            levelinfoList.Add(new LevelInfo(81, 90, 10000));
            levelinfoList.Add(new LevelInfo(91, 100, 10000));
            levelinfoList.Add(new LevelInfo(101, 200, 15000));
            levelinfoList.Add(new LevelInfo(201, 300, 20000));
            levelinfoList.Add(new LevelInfo(301, 400, 25000));
            levelinfoList.Add(new LevelInfo(401, 500, 30000));
            levelinfoList.Add(new LevelInfo(501, 600, 35000));
            levelinfoList.Add(new LevelInfo(601, 700, 40000));
            levelinfoList.Add(new LevelInfo(701, 800, 45000));
            levelinfoList.Add(new LevelInfo(801, 900, 50000));
            levelinfoList.Add(new LevelInfo(901, 999, 70000));
        }

        public readonly int minLevel = 1;
        public readonly int maxLevel = 999;


        public int GetLevel(int exp)
        {
            int tempExp = 0;
            for (int i = 1; i <= 999; ++i)
            {
                foreach (LevelInfo info in levelinfoList)
                {
                    if (info.minlevel <= i && i <= info.maxlevel)
                    {
                        tempExp = info.exp;
                        break;
                    }
                }
                if (tempExp < 5000)
                    tempExp = 65000;


                exp -= tempExp;

                if (exp < 0)
                    return i;
            }

            return maxLevel;
        }

        public float GetLevelRate(int level, int exp)
        {
            if (level >= maxLevel || level < minLevel)
                return 1.0f;

            int extraExp = exp;
            int totalExp = 1;
            float result = 1.0f;
            try
            {
                for (int i = 1; i < level; ++i)
                {
                    foreach (LevelInfo info in levelinfoList)
                    {
                        if (info.minlevel <= i && i <= info.maxlevel)
                        {
                            extraExp -= info.exp;
                            break;
                        }
                    }
                }

                foreach (LevelInfo info in levelinfoList)
                {
                    if (info.minlevel <= level && level <= info.maxlevel)
                    {
                        totalExp = info.exp;
                        break;
                    }
                }

                result = (float)extraExp / (float)totalExp;
            }
            catch
            {
                result = 1.0f;
            }
            return result;


        }




    }
}