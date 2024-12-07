using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class ChangeGameStateSystem : IEcsRunSystem
    {
        private EcsFilter<ChangeGameStateEvent> _filter;
        private RuntimeData _runtimeData = null;
        private LocationManager _locationManager = null;
        private UI _ui = null;
        public void Run()
        {
            foreach (var item in _filter)
            {
                var state = _filter.Get1(item).state;
                _runtimeData.GameState = state;
                _ui.ChangeGameState(state);
                _locationManager.ChangeGameState(state);
            }
        }
    }
}