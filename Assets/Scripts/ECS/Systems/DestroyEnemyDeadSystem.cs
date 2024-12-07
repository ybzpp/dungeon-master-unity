using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class DestroyEnemyDeadSystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEnemyDeadEvent> _filter;
        private EcsFilter<BoyRef, DeadTag> _boysfilter;

        public void Run()
        {
            foreach (var item in _filter)
            {
                foreach(var b in _boysfilter)
                {
                    GameObject.Destroy(_boysfilter.Get1(b).Boy.gameObject);
                }

                _filter.GetEntity(item).Del<DestroyEnemyDeadEvent>();
            }
        }
    }
}