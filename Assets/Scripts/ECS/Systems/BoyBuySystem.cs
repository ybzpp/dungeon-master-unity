using Leopotam.Ecs;
using System;

namespace DungeonMaster
{
    public class BoyBuySystem : IEcsRunSystem
    {
        private EcsFilter<BoyBuyEvent> _filter;
        private LocationManager _locationManager = null;
        private Config _config = null;
        public void Run()
        {

            foreach (var item in _filter)
            {
                var price = _filter.Get1(item).Price;
                if (_locationManager.Dungeon.Boys.Count < _config.MaxPartySize)
                {
                    if (GameHelper.TryBuy(price))
                    {
                        _locationManager.Gym.AddBoyToParty();
                        _filter.Get1(item).Popup.Hide();
                    }
                }
                _filter.GetEntity(item).Del<BoyBuyEvent>();
            }
        }
    }
}