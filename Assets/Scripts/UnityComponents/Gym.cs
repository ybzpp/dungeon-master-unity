using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonMaster
{
    public class Gym : LocationBase
    {
        public Transform RandomSpawnPoint => _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        public int BoyInGymCount => Mathf.Clamp(Progress.DeathCount + 1, 1, _config.MaxBoyInGym);
        public Camera Camera => _camera;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform[] _spawnPoints;

        private List<Transform> _spawnPointsList;
        private List<Boy> _boysList = new List<Boy>();
        private Boy _tempBoy;

        private Config _config;
        private RuntimeData _runtimeData;

        public override void Init(Config config, RuntimeData runtimeData)
        {
            if (!Application.isPlaying) 
                return;

            _config = config;
            _runtimeData = runtimeData;

            Clear();

            _spawnPointsList = _spawnPoints.ToList();
            for (int i = 0; i < BoyInGymCount; i++)
            {
                var boyPrefab = _config.BoyDataCollection.randomData.Prefab;
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

                    _tempBoy = boy;
                    data = boy.Data;
                    return true;
                }
            }   

            return false;
        }

        public void AddBoyToParty()
        {
            if (!_tempBoy)
            {
                Debug.Log("boy not find!");
                return;
            }

            GameHelper.NewEntity.Get<AddBoyToPartyEvent>().Boy = _tempBoy;

            _boysList.Remove(_tempBoy);
            _tempBoy = null;
        }
    }
}
