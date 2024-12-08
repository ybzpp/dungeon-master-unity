using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonMaster
{
    public static class GameHelper
    {
        public static Config Config => Service<Config>.Get();
        public static EcsEntity NewEntity => Service<EcsWorld>.Get().NewEntity();
        public static SignalBus SignalBus => Service<SignalBus>.Get();
        public static RuntimeData RuntimeData => Service<RuntimeData>.Get();
        public static BattleManager BattleManager => Service<BattleManager>.Get();
        public static LocationManager LocationManager => Service<LocationManager>.Get();
        public static Dungeon Dungeon => LocationManager.Dungeon;
        public static List<Boy> Boys => Dungeon.Boys;
        public static List<Boy> BadBoys => Dungeon.BadBoys;
        public static int RerollPrice => Config.StartRerollPrice + Config.StartRerollPriceStep * RuntimeData.RerollCount;

        public static void ChangeGameState(GameState state)
        {
            NewEntity.Get<ChangeGameStateEvent>().state = state;
        }

        public static List<Boy> GetBoys(bool isParty)
        {
            return isParty ? Boys : BadBoys;
        }

        public static List<Boy> GetAliveBoys(bool isParty)
        {
            var boys = isParty ? Boys : BadBoys;
            return boys.Where(boy => !boy.IsDead).ToList();
        }

        public static Boy GetRandomBoy(bool isParty)
        {
            var boys = GetBoys(isParty);
            return boys[UnityEngine.Random.Range(0, boys.Count)];
        }

        public static bool CanBuy(int value) => value <= Progress.Money;

        public static bool TryBuy(int value)
        {
            if (!CanBuy(value)) 
                return false;

            Buy(value);
            return true;
        }

        public static void Buy(int value)
        {
            if (!CanBuy(value))
                throw new InvalidOperationException("Not enough money to spend");

            Progress.Money -= value;
        }
    }
}