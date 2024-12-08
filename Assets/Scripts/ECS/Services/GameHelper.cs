using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace DungeonMaster
{
    public static class GameHelper
    {
        public static Config Config => Service<Config>.Get();
        public static SignalBus SignalBus => Service<SignalBus>.Get();
        public static EcsEntity NewEntity => Service<EcsWorld>.Get().NewEntity();
        public static int RerollPrice => Config.StartRerollPrice + Config.StartRerollPriceStep * Service<RuntimeData>.Get().RerollCount;

        public static void ChangeGameState(GameState state)
        {
            NewEntity.Get<ChangeGameStateEvent>().state = state;
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
                Debug.LogError("not enough money");

            Progress.Money -= value;
        }
    }
}