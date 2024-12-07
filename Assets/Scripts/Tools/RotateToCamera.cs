using UnityEngine;

namespace DungeonMaster
{
    public class RotateToCamera : MonoBehaviour
    {
        private void Start()
        {
            switch (GameHelper.RuntimeData.GameState)
            {
                case GameState.Dungeon:
                    var camera = GameHelper.LocationManager.Dungeon.Camera;
                    transform.forward = camera.transform.forward;
                    break;
                case GameState.Start:
                    break;
                case GameState.Gym:
                    break;
                case GameState.Lobby:
                    break;
                case GameState.Shop:
                    break;
            }
        }
    }
}
