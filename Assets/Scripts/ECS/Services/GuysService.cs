using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonMaster
{
    public static class GuysService
    {
        public static Boy GetBoyPrefab(Boy[] boys)
        {
            var boyList = new List<Boy>();
            foreach (var b in boys)
            {
                if (Progress.IsUnlock(b.Data.Id))
                {
                    boyList.Add(b);
                }
            }
            return boyList.Count > 0 ? boyList[UnityEngine.Random.Range(0, boyList.Count)] : null;
        }
    }
}