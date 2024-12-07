using Leopotam.Ecs;

namespace DungeonMaster
{
    public class DestroyAllBoysSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyAllBoysEvent> _filter;
        private LocationManager _locationManager;

        public void Run()
        {
            foreach (var item in _filter)
            {
                _locationManager.Dungeon.DestroyAllBoys();
                _filter.GetEntity(item).Del<DestroyAllBoysEvent>();
            }
        }
    }
}