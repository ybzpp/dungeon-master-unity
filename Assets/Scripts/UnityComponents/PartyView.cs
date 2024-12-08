using UnityEngine;

namespace DungeonMaster
{
    public class PartyView : LocationBase 
    {
        [SerializeField] private Transform[] _spawnPoints;

        private void OnEnable()
        {
            foreach (Transform t in _spawnPoints)
                t.gameObject.SetActive(false);
        }

        public Vector3 GetPointByIndex(int index)
        {
            if (index <_spawnPoints.Length)
                return _spawnPoints[index].position;

            return Vector3.zero;
        }
    }
}
