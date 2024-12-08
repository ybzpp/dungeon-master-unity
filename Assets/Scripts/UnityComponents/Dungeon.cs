using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace DungeonMaster
{
    public class Dungeon : LocationBase
    {
        public CameraRig CameraRig => _cameraRig;
        public Camera Camera => _cameraRig.Camera;
        public List<Boy> Boys => _boys;
        [SerializeField] private List<Boy> _boys = new List<Boy>();
        public List<Boy> BadBoys => _badBoys;
        [SerializeField] private List<Boy> _badBoys = new List<Boy>();

        public Action<List<Boy>> OnChangedBoys;
        public Action<int, int> OnChangedBoysCount;

        [SerializeField] private DungeonData _data;
        [SerializeField] private CameraRig _cameraRig;
        [SerializeField] private Transform[] _boysPoints;
        [SerializeField] private Transform[] _badBoysPoints;
        private RuntimeData _runtimeData;
        private Config _config;

        private void OnEnable()
        {
            foreach (Transform t in _boysPoints) 
                t.gameObject.SetActive(false);

            foreach (Transform t in _badBoysPoints)
                t.gameObject.SetActive(false);

        }

        public override void Init(Config config, RuntimeData runtimeData)
        {
            _config = config;
            _runtimeData = runtimeData;
        }

        public void SpawnBadBoys(int level)
        {
            var badBoys = _data.GetDungeonLevelDataByLevel(level).BadBoys;
            for (int i = 0; i < badBoys.Length; i++)
            {
                Boy badBoy = Instantiate(badBoys[i], _badBoysPoints[i].position, _badBoysPoints[i].rotation);
                badBoy.Init(GameHelper.NewEntity);
                _badBoys.Add(badBoy);
            }
        }

        public void UpdateBoys()
        {
            for (int i = 0; i < Boys.Count; i++)
            {
                _boys[i].transform.parent = null;
                _boys[i].transform.position = _boysPoints[i].position;
                _boys[i].transform.rotation = _boysPoints[i].rotation;
                _boys[i].gameObject.SetActive(true);
            }
        }

        public void AddBoy(Boy boy)
        {
            _boys.Add(boy);
            OnChangedBoys?.Invoke(_boys);
            OnChangedBoysCount?.Invoke(_boys.Count, _config.MaxPartySize);
        }

        public void RemoveBoy(Boy boy, bool isParty)
        {
            if (isParty)
            {
                _boys.Remove(boy);
                OnChangedBoys?.Invoke(Boys);
                OnChangedBoysCount?.Invoke(_boys.Count, _config.MaxPartySize);
            }
            else
            {
                _badBoys.Remove(boy);
            }
        }

        public void DestroyAllBoys()
        {
            DestroyBoys(_badBoys);
            DestroyBoys(_boys);
            OnChangedBoys?.Invoke(_boys);
            OnChangedBoysCount?.Invoke(_boys.Count, _config.MaxPartySize);
        }

        public void DestroyBoys(List<Boy> boys)
        {
            if (boys.Count > 0)
            {
                for (int i = 0; i < boys.Count; i++)
                {
                    if (boys[i])
                        Destroy(boys[i].gameObject);
                }
            }
            boys.Clear();
        }

        public GameStateResult CheckGameOver()
        {
            bool allBadBoysDead = !BadBoys.Any(x => !x.IsDead);
            bool allBoysDead = !Boys.Any(x => !x.IsDead);
            if (allBadBoysDead) return GameStateResult.Win;
            if (allBoysDead) return GameStateResult.Lose;
            return GameStateResult.Continue;
        }
    }
}