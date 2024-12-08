using Leopotam.Ecs;

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
                _locationManager.Dungeon.SpawnBadBoys(_runtimeData.DungeonLevel);
                _locationManager.Dungeon.UpdateBoys();
                _ui.Dungeon.LevelImagesUpdate(_runtimeData.DungeonLevel);
                _battleManager.StartBattle();
            }
        }
    }
}