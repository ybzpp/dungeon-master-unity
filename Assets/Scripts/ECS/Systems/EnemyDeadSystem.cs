using Leopotam.Ecs;
using System.Linq;

namespace DungeonMaster
{
    public class EnemyDeadSystem : IEcsRunSystem
    {
        private EcsFilter<BoyRef, DeadEvent> _filter;
        private Config _config = null;
        private RuntimeData _runtimeData = null;
        private LocationManager _locationManager = null;
        private SignalBus _signalBus = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                var entity = _filter.GetEntity(item);
                var boy = _filter.Get1(item).Boy;

                var isParty = entity.Has<PartyTag>();
                _locationManager.Dungeon.RemoveBoy(boy, isParty);

                if (!isParty)
                {
                    _runtimeData.Reward.Coins += _config.DropMoney;
                    _runtimeData.Reward.Exp += _config.DropMoney;
                }

                boy.Dead();
                entity.Get<DeadTag>();
                entity.Del<DeadEvent>();
            }
        }
    }
}