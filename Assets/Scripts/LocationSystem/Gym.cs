using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonMaster
{
    public class Gym : MonoBehaviour
    {
        public Transform RandomSpawnPoint => _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform[] _spawnPoints;

        private List<Transform> _spawnPointsList;
        private List<Boy> _boysList = new List<Boy>();
        private Boy _boy;

        public void Init()
        {
            if (!Application.isPlaying) return;
            

            Clear();

            _spawnPointsList = _spawnPoints.ToList();
            for (int i = 0; i < GameHelper.BoyInGymCount; i++)
            {
                var boyPrefab = GameHelper.Config.BoyDataCollection.randomData.Prefab;
                var point = GetPoint();
                var boy = Instantiate(boyPrefab, point.position, point.rotation);
                boy.Init(GameHelper.NewEntity);
                _boysList.Add(boy);
            }
        } 

        public void Clear()
        {
            if (_boysList.Count > 0)
            {
                for (int i = 0; i < _boysList.Count; i++)
                {
                    if (_boysList[i]) Destroy(_boysList[i].gameObject);
                }
            }
            _boysList.Clear();
        }

        private void OnEnable()
        {
            foreach (Transform t in _spawnPoints)
                t.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            Clear();
        }

        public Transform GetPoint()
        {
            if (_spawnPointsList.Count > 0)
            {
                var randomIndex = Random.Range(0, _spawnPointsList.Count);
                var point = _spawnPointsList[randomIndex];
                _spawnPointsList.RemoveAt(randomIndex);
                return point;
            }

            return RandomSpawnPoint;
        }

        public bool TryShowGymBuyPopup(out BoyDataSO data)
        {
            data = null;
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent(out Boy boy))
                {
                    if (boy.IsParty)
                        return false;

                    _boy = boy;
                    data = boy.Data;
                    return true;
                }
            }   

            return false;
        }

        public void AddBoyToParty()
        {
            if (!_boy)
            {
                Debug.Log("boy not find!");
                return;
            }

            GameHelper.AddBoyToParty(_boy);
            _boysList.Remove(_boy);
            _boy = null;
        }
    }
}
