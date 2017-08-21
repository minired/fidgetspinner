using UnityEngine;
using System.Collections;
namespace Fidget.Common
{
    public class GooglePlayAchievement
    {

        readonly string[] MapCodeArray = { "CgkIgZL_6MUVEAIQAQ", "CgkIgZL_6MUVEAIQAg", "CgkIgZL_6MUVEAIQAw", "CgkIgZL_6MUVEAIQBA", "CgkIgZL_6MUVEAIQBQ", "CgkIgZL_6MUVEAIQBg" };
        readonly string[] CharacterCodeArray = { "CgkIgZL_6MUVEAIQCQ", "CgkIgZL_6MUVEAIQCg", "CgkIgZL_6MUVEAIQCw", "CgkIgZL_6MUVEAIQDA", "CgkIgZL_6MUVEAIQDQ", "CgkIgZL_6MUVEAIQDg", "CgkIgZL_6MUVEAIQDw", "CgkIgZL_6MUVEAIQEA", "CgkIgZL_6MUVEAIQEQ" };
        readonly string[] CoinCodeArray = { "CgkIgZL_6MUVEAIQEg", "CgkIgZL_6MUVEAIQEw", "CgkIgZL_6MUVEAIQFA", "CgkIgZL_6MUVEAIQFQ", "CgkIgZL_6MUVEAIQFg", "CgkIgZL_6MUVEAIQFw", "CgkIgZL_6MUVEAIQGA", "CgkIgZL_6MUVEAIQGQ", "CgkIgZL_6MUVEAIQGg" };
        readonly string[] LevelCodeArray = { "CgkIgZL_6MUVEAIQHQ", "CgkIgZL_6MUVEAIQHw", "CgkIgZL_6MUVEAIQIA", "CgkIgZL_6MUVEAIQIQ", "CgkIgZL_6MUVEAIQIg", "CgkIgZL_6MUVEAIQIw", "CgkIgZL_6MUVEAIQJA", "CgkIgZL_6MUVEAIQJQ", "CgkIgZL_6MUVEAIQJg", "CgkIgZL_6MUVEAIQJw" };
        readonly string[] StageFloorCodeArray = { "CgkIgZL_6MUVEAIQKA", "CgkIgZL_6MUVEAIQKQ", "CgkIgZL_6MUVEAIQKg", "CgkIgZL_6MUVEAIQKw", "CgkIgZL_6MUVEAIQLA", "CgkIgZL_6MUVEAIQLQ", "CgkIgZL_6MUVEAIQOQ", "CgkIgZL_6MUVEAIQOg", "CgkIgZL_6MUVEAIQLg", "CgkIgZL_6MUVEAIQLw", "CgkIgZL_6MUVEAIQMA", "CgkIgZL_6MUVEAIQMQ", "CgkIgZL_6MUVEAIQMg", "CgkIgZL_6MUVEAIQMw" };
        readonly string[] ReviveCodeArray = { "CgkIgZL_6MUVEAIQNA", "CgkIgZL_6MUVEAIQNQ", "CgkIgZL_6MUVEAIQNg", "CgkIgZL_6MUVEAIQNw", "CgkIgZL_6MUVEAIQOA" };

        public GooglePlayAchievement()
        {

        }

        public string GetMapCode(int index)
        {
            if (MapCodeArray.Length <= index)
                return "";
            else
                return MapCodeArray[index];
        }

        public string GetCharacterCode(int index)
        {
            if (CharacterCodeArray.Length <= index)
                return "";
            else
                return CharacterCodeArray[index];
        }

        public string GetCoinCode(int index)
        {
            if (CoinCodeArray.Length <= index)
                return "";
            else
                return CoinCodeArray[index];
        }
        public string GetLevelCode(int index)
        {
            if (LevelCodeArray.Length <= index)
                return "";
            else
                return LevelCodeArray[index];
        }

        public string GetStageFloorCode(int index)
        {
            if (StageFloorCodeArray.Length <= index)
                return "";
            else
                return StageFloorCodeArray[index];
        }

        public string GetReviveCode(int index)
        {
            if (ReviveCodeArray.Length <= index)
                return "";
            else
                return ReviveCodeArray[index];
        }
    }
}