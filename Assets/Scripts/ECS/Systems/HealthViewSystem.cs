using Leopotam.Ecs;

namespace DungeonMaster
{
    public class HealthViewSystem : IEcsRunSystem
    {
        private EcsFilter<BoyRef, HealthViewRef> _filter;
        private RuntimeData _runtimeData = null;
        private LocationManager _locationManager = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                switch (_runtimeData.GameState)
                {
                    case GameState.Lobby:
                        break;
                    case GameState.Gym:
                        break;
                    case GameState.Dungeon:
                        var dir = _locationManager.Dungeon.Camera.transform.forward;
                        var view = _filter.Get2(item).HealthView;
                        view.RotateToCamera(dir);

                        var isPatry = _filter.GetEntity(item).Has<PartyTag>();
                        var startPos = _filter.Get1(item).Boy.transform.position;
                        var offset = _filter.Get1(item).Boy.transform.forward;
                        view.transform.position = startPos + (isPatry ? -offset : offset);
                        break;
                }
            }
        }
    }
}