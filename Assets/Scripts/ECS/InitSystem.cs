using Leopotam.Ecs;

namespace DungeonMaster
{
    public class InitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private LocationManager _locationManager = null;
        private EcsWorld _world = null;
        private Config _config = null;
        private UI _ui = null;

        public void Init()
        {
            _locationManager.Dungeon.OnChangedBoys += _ui.Party.UpdatePartyImages;
            _world.NewEntity().Get<ChangeGameStateEvent>().state = GameState.Lobby;

            foreach (var item in _config.StartUnlocks)
            {
                Progress.Unlock(item);
            }
        }

        public void Destroy()
        {
            _locationManager.Dungeon.OnChangedBoys -= _ui.Party.UpdatePartyImages;
        }
    }
}