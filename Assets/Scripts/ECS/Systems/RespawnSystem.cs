using Leopotam.Ecs;

namespace DungeonMaster
{
    public class RespawnSystem : IEcsRunSystem
    {
        private EcsFilter<RespawnEvent> _respawnfilter;
        private EcsFilter<BoyRef, PartyTag> _filter;

        public void Run()
        {
            foreach (var r in _respawnfilter)
            {
                foreach (var item in _filter)
                {
                    _filter.Get1(item).Boy.Respawn();
                }

                _filter.GetEntity(r).Del<RespawnEvent>();
            }
        }
    }
}