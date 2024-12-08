using Leopotam.Ecs;

namespace DungeonMaster
{
    public class HealSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, HealEvent> _filter;
        private EcsFilter<BoyRef, HealEvent> _boyFilter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                _filter.Get1(item).Health.Heal(_filter.Get2(item).Value);
            }

            foreach (var item in _boyFilter)
            {
                _boyFilter.Get1(item).Boy.Heal(_boyFilter.Get2(item).Value);
            }
        }
    }
}