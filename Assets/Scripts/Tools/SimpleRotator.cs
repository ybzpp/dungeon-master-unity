using UnityEngine;

namespace DungeonMaster
{
    public class SimpleRotator : MonoBehaviour
    {
        public Vector3 Speed;

        private void Update()
        {
            transform.Rotate(Speed * Time.deltaTime);
        }
    }
}
