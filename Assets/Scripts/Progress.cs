using System;
using UnityEngine;

namespace DungeonMaster
{
    public static class Progress
    {
        public static int DeathCount
        {
            get => PlayerPrefs.GetInt("DeathCount", 0);
            set => PlayerPrefs.SetInt("DeathCount", value);
        }

        public static Action<int> OnChangeMoney;
        public static int Money
        {
            get => PlayerPrefs.GetInt("Money", 0);
            set
            {
                PlayerPrefs.SetInt("Money", value);
                OnChangeMoney?.Invoke(value);
            }
        }

        public static Action<int> OnChangeClickerExp;
        public static int ClickerExp
        {
            get => PlayerPrefs.GetInt("ClickerExp", 0);
            set
            {
                PlayerPrefs.SetInt("ClickerExp", value);
                OnChangeClickerExp?.Invoke(value);
            }
        }

        public static Action<int> OnChangeClickerLvl;
        public static int ClickerLvl
        {
            get => PlayerPrefs.GetInt("ClickerLvl", 1);
            set
            {
                PlayerPrefs.SetInt("ClickerLvl", value);
                OnChangeClickerLvl?.Invoke(value);
            }
        }

        public static bool IsUnlock(string id) => PlayerPrefs.GetInt(id, 0) == 0 ? false : true;
        public static void Unlock(string id) => PlayerPrefs.SetInt(id, 1);
    }
}