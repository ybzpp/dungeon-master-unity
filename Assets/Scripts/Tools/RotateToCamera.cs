using UnityEngine;

namespace DungeonMaster
{
    public class RotateToCamera : MonoBehaviour
    {
        private void Start()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}
