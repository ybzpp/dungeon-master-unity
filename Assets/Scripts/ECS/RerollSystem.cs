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
                var price = 
                    _config.StartRerollPrice + 
                    _config.StartRerollPriceStep * _runtimeData.RerollCount;

                if (GameHelper.TryBuy(price))
                {
                    _runtimeData.RerollCount++;
                    _locationManager.Gym.Init(_config);
                    _signalBus.OnChangeRerollPrice?.Invoke(price);
                }

                _filter.GetEntity(item).Del<RerollEvent>();
            }
        }
    }
}