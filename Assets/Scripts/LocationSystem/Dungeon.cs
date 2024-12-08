using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace DungeonMaster
{
    public class Dungeon : MonoBehaviour
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

        private void OnEnable()
        {
            foreach (Transform t in _boysPoints) 
                t.gameObject.SetActive(false);

            foreach (Transform t in _badBoysPoints)
                t.gameObject.SetActive(false);
        }

        public void Init(int level)
        {
            var badBoys = _data.GetDungeonLevelDataByLevel(level).BadBoys;
            for (int i = 0; i < badBoys.Length; i++)
            {
                Boy badBoy = Instantiate(badBoys[i], _badBoysPoints[i].position, _badBoysPoints[i].rotation);
                badBoy.Init(GameHelper.NewEntity);
                _badBoys.Add(badBoy);
            }

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
            OnChangedBoysCount?.Invoke(_boys.Count, GameHelper.Config.MaxPartySize);
        }

        public void RemoveBoy(Boy boy, bool isParty)
        {
            if (isParty)
            {
                _boys.Remove(boy);
                OnChangedBoys?.Invoke(Boys);
                OnChangedBoysCount?.Invoke(_boys.Count, GameHelper.Config.MaxPartySize);
            }
            else
            {
                _badBoys.Remove(boy);
            }
        }

        public void DestroyAllBoys()
        {
            if (_badBoys.Count > 0)
            {
                for (int i = 0; i < _badBoys.Count; i++)
                {
                    if (_badBoys[i])
                        Destroy(_badBoys[i].gameObject);
                }
            }
            _badBoys.Clear();

            if (_boys.Count > 0)
            {
                for (int i = 0; i < _boys.Count; i++)
                {
                    if (_boys[i])
                        Destroy(_boys[i].gameObject);
                }
            }
            _boys.Clear();
            OnChangedBoys?.Invoke(_boys);
            OnChangedBoysCount?.Invoke(_boys.Count, GameHelper.Config.MaxPartySize);
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