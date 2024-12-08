﻿using Leopotam.Ecs;

namespace DungeonMaster
{
    public class StartDungeonSystem : IEcsRunSystem
    {
        private EcsFilter<StartDungeonEvent> _filter;
        private LocationManager _locationManager = null;
        private EcsWorld _world = null;
        public void Run()
        {
            foreach (var item in _filter)
            {
                if (_locationManager.Dungeon.Boys.Count > 0)
                {
                    _world.NewEntity().Get<ResetProgressEvent>();
                    GameHelper.ChangeGameState(GameState.Dungeon);
                }
            }
        }
    }
}