using UnityEngine;

namespace DungeonMaster
{
    [CreateAssetMenu]
    public class DungeonData : ScriptableObject
    {
        public DungeonLevelData[] DungeonLevelData;

        public DungeonLevelData GetDungeonLevelDataByLevel(int level)
        {
            return DungeonLevelData[level % DungeonLevelData.Length];
        }
    }

    [System.Serializable]
    public struct DungeonLevelData
    {
        public Boy[] BadBoys;
    }
}