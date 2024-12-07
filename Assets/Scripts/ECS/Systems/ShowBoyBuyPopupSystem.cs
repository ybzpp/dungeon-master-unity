using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class ShowBoyBuyPopupSystem : IEcsRunSystem
    {
        private RuntimeData _runtimeData = null;
        private LocationManager _locationManager = null;
        private UI _ui = null;
        public void Run()
        {
            if (_runtimeData.GameState != GameState.Gym)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                if (_locationManager.Gym.TryShowGymBuyPopup(out BoyDataSO data))
                {
                    _ui.Gym.ShowGymBuyPopup(data);
                }
            }
        }
    }
}