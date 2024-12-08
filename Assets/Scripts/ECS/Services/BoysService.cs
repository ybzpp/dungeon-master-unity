using LeopotamGroup.Globals;
using System.Collections.Generic;
using System.Linq;

namespace DungeonMaster
{
    public static class BoysService
    {
        public static Dungeon Dungeon => Service<LocationManager>.Get().Dungeon;
        public static List<Boy> Boys => Dungeon.Boys;
        public static List<Boy> BadBoys => Dungeon.BadBoys;

        public static List<Boy> GetBoys(bool isParty)
        {
            return isParty ? Boys : BadBoys;
        }

        public static Boy GetRandomBoy(bool isParty)
        {
            var boys = GetBoys(isParty);
            return boys[UnityEngine.Random.Range(0, boys.Count)];
        }

        public static List<Boy> GetAliveBoys(bool isParty)
        {
            var boys = isParty ? Boys : BadBoys;
            return boys.Where(boy => !boy.IsDead).ToList();
        }
    }
}