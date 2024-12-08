using UnityEngine;

namespace DungeonMaster
{
    public class Lobby : MonoBehaviour
    {
        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
    }
}