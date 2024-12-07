using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class SelectSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter<SelectRef> _moveSelectfilter;
        private EcsFilter<BoyRef, SelectTag> _targetBoyFilter;
        private EcsFilter<SelectEvent> _filter;

        private Config _config = null;
        private EcsWorld _world = null;

        public void Init()
        {
            var select = Object.Instantiate(_config.SelectPrefab);
            select.gameObject.SetActive(false);
            _world.NewEntity().Get<SelectRef>().Value = select;
        }

        public void Run()
        {
            foreach (var item in _filter)
            {
                foreach (var r in _targetBoyFilter)
                {
                    _targetBoyFilter.GetEntity(r).Del<SelectTag>();
                }

                var entity = _filter.GetEntity(item);
                entity.Get<SelectTag>();
                entity.Del<SelectEvent>();
            }

            foreach (var item in _moveSelectfilter)
            {
                var t = _moveSelectfilter.Get1(item).Value;


                if (_targetBoyFilter.GetEntitiesCount() == 0)
                {
                    if (t.gameObject.activeInHierarchy)
                    {
                        t.gameObject.SetActive(false);
                    }
                }

                foreach (var r in _targetBoyFilter)
                {
                    t.position = _targetBoyFilter.Get1(r).Boy.transform.position;
                    t.gameObject.SetActive(true);
                }
            }
        }
    }
}