using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class ResetProgressSystem : IEcsRunSystem
    {
        private EcsFilter<ResetProgressEvent> _filter;
        private RuntimeData _runtimeData = null;
        private UI _ui = null;
        public void Run()
        {
            foreach (var item in _filter)
            {
                _runtimeData.DungeonLevel = 0;
                _runtimeData.Reward = new Reward();
            }
        }
    }
}