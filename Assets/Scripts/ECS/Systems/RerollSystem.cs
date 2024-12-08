using Leopotam.Ecs;

namespace DungeonMaster
{
    public class RerollSystem : IEcsRunSystem
    {
        private EcsFilter<RerollEvent> _filter;
        private Config _config = null;
        private RuntimeData _runtimeData = null;
        private LocationManager _locationManager = null;
        private SignalBus _signalBus = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                var price = GameHelper.RerollPrice;
                if (GameHelper.TryBuy(price))
                {
                    _runtimeData.RerollCount++;
                    _signalBus.OnChangeRerollPrice?.Invoke(price);
                    _locationManager.Gym.Init(_config, _runtimeData); // updates boys in gym
                }

                _filter.GetEntity(item).Del<RerollEvent>();
            }
        }
    }
}