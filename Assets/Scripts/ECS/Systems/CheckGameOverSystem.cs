using Leopotam.Ecs;

namespace DungeonMaster
{
    public class CheckGameOverSystem : IEcsRunSystem
    {
        private EcsFilter<CheckGameOverEvent> _filter;
        private LocationManager _locationManager = null;
        private EcsWorld _world = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                var result = _locationManager.Dungeon.CheckGameOver();
                switch (result)
                {
                    case GameStateResult.Win:
                        _world.NewEntity().Get<WinEvent>();
                        break;
                    case GameStateResult.Lose:
                        _world.NewEntity().Get<LoseEvent>();
                        break;
                    case GameStateResult.Continue:
                        _world.NewEntity().Get<ContinueDungeonEvent>();
                        break;
                    default:
                        break;
                }

                _filter.GetEntity(item).Del<CheckGameOverEvent>();
            }
        }
    }
}