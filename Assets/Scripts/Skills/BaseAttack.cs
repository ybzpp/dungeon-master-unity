using Leopotam.Ecs;
using System;
using System.Collections;
using UnityEngine;

namespace DungeonMaster
{
    public class BaseAttack : BaseSkill
    {
        public int Damage => Root.Power;

        [SerializeField] private GameObject _fxPrefab;
        [SerializeField] private float _attackTime = .1f;
        [SerializeField] private float _freezeTime = .1f;
        [SerializeField] private float _timeToTarget = .1f;
        [SerializeField] private float _timeToBack = .1f;
        [SerializeField] private float _offsetZ = 1.5f;

        private Boy _targetBoy;
        private Vector3 _startRotation;

        public override void StartSkill()
        {
            base.StartSkill();
            _targetBoy = BoysService.GetRandomBoy(!Root.IsParty);
            _startRotation = Root.transform.eulerAngles;
            StartCoroutine(SkillAnimation());
        }

        public IEnumerator SkillAnimation()
        {
            Root.Animator.SetBool(ActionAnimationBool, true);
            var start = Root.transform.position;
            var dir = _targetBoy.transform.position - start;
            dir.Normalize();
            var end = _targetBoy.transform.position - dir * _offsetZ;
            var time = 0f;

            while (time < _timeToTarget)
            {
                time += Time.deltaTime;
                Root.transform.position = Vector3.Lerp(start, end, time / _timeToTarget);
                Root.transform.eulerAngles = Vector3.Lerp(Root.transform.eulerAngles, Quaternion.LookRotation(dir).eulerAngles, time / _timeToBack);
                yield return null;
            }
            Root.transform.position = end;

            time = 0;
            Time.timeScale = 0;
            while (time < _freezeTime)
            {
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            Time.timeScale = 1;

            var fx = Instantiate(_fxPrefab, _targetBoy.transform.position, Quaternion.identity);
            TakeDamege();
            yield return new WaitForSeconds(_attackTime);
            Root.Animator.SetBool(ActionAnimationBool, false);

            time = 0;
            while (time < _timeToBack)
            {
                time += Time.deltaTime;
                Root.transform.position = Vector3.Lerp(end, start, time / _timeToBack);
                Root.transform.eulerAngles = Vector3.Lerp(Root.transform.eulerAngles, _startRotation, time / _timeToBack);
                yield return null;
            }
            Root.transform.position = start;
            Destroy(fx.gameObject, 1f);

            Debug.Log("OnAttackComplete");
            GameHelper.SignalBus.OnSkillComplete?.Invoke();
        }

        public override void TakeDamege()
        {
            base.TakeDamege();
            _targetBoy.Entity.Get<DamageEvent>() = new DamageEvent()
            {
                Value = this.Damage,
                Position = _targetBoy.transform.position
            };
        }
    }
}