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
                var rerollPrice = 
                    _config.StartRerollPrice + 
                    _config.StartRerollPriceStep * _runtimeData.RerollCount;

                if (GameHelper.TryBuy(rerollPrice))
                {
                    _runtimeData.RerollCount++;
                    _locationManager.Gym.Init();
                    _signalBus.OnChangeRerollPrice?.Invoke(rerollPrice);
                }

                _filter.GetEntity(item).Del<RerollEvent>();
            }
        }
    }
}