using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace DungeonMaster
{
    public class AoeBaseAttack : BaseTimeSkill
    {
        public int Damage => (int)(Root.Power * _damageMultiplier);

        [SerializeField] private float _damageMultiplier = 3f;
        [SerializeField] private GameObject _fxPrefab;

        private List<GameObject> _fxs = new List<GameObject>();
        private List<Boy> _targetBoys = new List<Boy>();

        public override void StartSkill()
        {
            base.StartSkill();
            _targetBoys = GameHelper.GetPartyBoys(!Root.IsParty);
            StartCoroutine(SkillAnimation());
        }

        public override void TakeDamege()
        {
            base.TakeDamege();
            for (int i = 0; i < _targetBoys.Count; i++)
            {
                _targetBoys[i].Entity.Get<DamageEvent>() = new DamageEvent()
                {
                    Value = Damage,
                    Position = _targetBoys[i].transform.position
                };
            }
        }

        public void SpawnFx()
        {
            for (int i = 0; i < _targetBoys.Count; i++)
            {
                _fxs.Add(Instantiate(_fxPrefab, _targetBoys[i].transform.position, Quaternion.identity));
            }
        }

        public void DestroyFx()
        {
            for (int i = 0; i < _fxs.Count; i++)
            {
                Destroy(_fxs[i].gameObject);
            }
        }

        public IEnumerator SkillAnimation()
        {
            Root.Animator.SetBool(ActionAnimationBool, true);

            yield return new WaitForSeconds(TimeToSpawnFx);

            SpawnFx();
            if (Freeze) Root.Animator.speed = 0;

            yield return new WaitForSeconds(TimeToAction);

            TakeDamege();
            if (Freeze) Root.Animator.speed = 1;

            yield return new WaitForSeconds(TimeToEndAnimation);

            DestroyFx();
            Root.Animator.SetBool(ActionAnimationBool, false);
            GameHelper.SignalBus.OnSkillComplete?.Invoke();
        }
    }
}
