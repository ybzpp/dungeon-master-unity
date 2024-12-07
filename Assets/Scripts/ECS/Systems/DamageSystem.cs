using Leopotam.Ecs;

namespace DungeonMaster
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, DamageEvent> _filter;
        private EcsFilter<BoyRef, DamageEvent> _boyFilter;
        private EcsFilter<DamageEvent> _damageFilter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                _filter.Get1(item).Health.Damage(_filter.Get2(item).Value);
            }

            foreach (var item in _boyFilter)
            {
                _boyFilter.Get1(item).Boy.Damage(_boyFilter.Get2(item).Value);
            }

            foreach (var item in _damageFilter)
            {
                GameHelper.DamageFx(_boyFilter.Get2(item).Value, _boyFilter.Get2(item).Position);
            }
        }
    }
}