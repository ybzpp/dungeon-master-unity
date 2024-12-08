using UnityEngine;

namespace DungeonMaster
{
    public class Lobby : LocationBase
    {
        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
    }
}