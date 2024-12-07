using System;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMaster
{
    public static class UnlockerService
    {
        public static List<UnlockData> GetAvailableUnlocks()
        {
            var list = new List<UnlockData>();
            foreach (UnlockData data in GameHelper.Config.Unlocks)
            {
                if (!Progress.IsUnlock(data.id))
                {
                    list.Add(data);
                }
            }
            return list;
        }

        public static int GetUnlockPrice()
        {
            var collection = GameHelper.Config.UnlockDataCollection;
            var length = GetAvailableUnlocks().Count;
            return collection.Price[collection.Datas.Length - length];
        }
    }

    [Serializable]
    public class UnlockData
    {
        public string id;
        public string description;
        public Sprite ico;
    }
}