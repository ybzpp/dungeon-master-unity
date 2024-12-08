using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;

namespace DungeonMaster
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, DamageEvent> _filter;
        private EcsFilter<BoyRef, DamageEvent> _boyFilter;
        private EcsFilter<DamageEvent> _damageFilter;
        private LocationManager _locationManager;

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
                var pos = _boyFilter.Get2(item).Position;
                var damage = _boyFilter.Get2(item).Value;
                var view = Service<ObjectPoolController>.Get()
                    .SpawnFromPool("DamageFx", pos, Quaternion.identity)
                    .GetComponent<DamageView>();

                if (view)
                {
                    var dir = _locationManager.Dungeon.Camera.transform.forward;
                    view.Init(damage, dir);
                }
            }
        }
    }
}