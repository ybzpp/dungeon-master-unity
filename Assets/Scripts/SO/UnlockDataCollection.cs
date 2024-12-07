using UnityEngine;

namespace DungeonMaster
{
    [CreateAssetMenu]
    public class UnlockDataCollection : ScriptableObject
    {
        public int[] Price;
        public UnlockData[] Datas;
    }
}