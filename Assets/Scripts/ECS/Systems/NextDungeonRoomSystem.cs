using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class NextDungeonRoomSystem : IEcsRunSystem
    {
        private EcsFilter<NextDungeonRoomEvent> _filter;
        private LocationManager _locationManager = null;
        private BattleManager _battleManager = null;
        private RuntimeData _runtimeData = null;
        private UI _ui = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                _runtimeData.DungeonLevel++;
                _locationManager.Dungeon.Init(_runtimeData.DungeonLevel);
                _ui.Dungeon.LevelImagesUpdate(_runtimeData.DungeonLevel);

                _battleManager.StartBattle(); //TODO: add waiter;
            }
        }
    }
}