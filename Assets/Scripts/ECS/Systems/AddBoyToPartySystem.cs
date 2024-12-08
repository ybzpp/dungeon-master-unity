using Leopotam.Ecs;
using System;

namespace DungeonMaster
{
    public class AddBoyToPartySystem : IEcsRunSystem
    {
        private EcsFilter<AddBoyToPartyEvent> _filter;
        private LocationManager _locationManager = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                var boy = _filter.Get1(item).Boy;
                boy.Entity.Get<PartyTag>();
                boy.transform.position = _locationManager.Party.GetPointByIndex(_locationManager.Dungeon.Boys.Count);
                _locationManager.Dungeon.AddBoy(boy);

                _filter.GetEntity(item).Del<AddBoyToPartyEvent>();
            }
        }
    }
}