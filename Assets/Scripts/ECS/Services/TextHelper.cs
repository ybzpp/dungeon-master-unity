using TMPro;
using UnityEngine;

namespace DungeonMaster
{
    public static class TextHelper
    {
        public static Color RedColor => GameHelper.Config.ColorPalette.Red;
        public static Color GoldColor => GameHelper.Config.ColorPalette.Gold;
        public static string CoinsText = "<sprite=0>";
        public static string PowerText = "<sprite=1>";
        public static string ExpText = "<sprite=2>";

        public static string MoneyTextConvert(int value)
        {
            return $"{value}{CoinsText}";
        }

        public static string StatsTextConvert(int value)
        {
            return $"{value}{PowerText}";
        }

        public static string RewardTextConvert(Reward reward)
        {
            return $"REWARD: " +
                $"\n{reward.Coins}{CoinsText} " +
                $"\n{reward.Exp}{ExpText}";
        }
    }
}