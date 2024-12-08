using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMaster
{
    public class AoeHeal : BaseTimeSkill
    {
        [SerializeField] private ParticleSystem _aoeHealFx;
        [SerializeField] private GameObject _healFxPrefab;

        private List<Boy> _targetBoys = new List<Boy>();
        private List<GameObject> _fxs = new List<GameObject>();

        public override void StartSkill()
        {
            base.StartSkill();
            _targetBoys = GameHelper.GetBoys(Root.IsParty);
            StartCoroutine(SkillAnimation());
        }

        public IEnumerator SkillAnimation()
        {
            Root.Animator.SetBool(ActionAnimationBool, true);

            yield return new WaitForSeconds(TimeToSpawnFx);

            var value = Root.Power;
            foreach (var boy in _targetBoys)
            {
                boy.Heal(value);
                var fx = Instantiate(_healFxPrefab, boy.transform.position, Quaternion.identity);
                _fxs.Add(fx);
            }
            _aoeHealFx.Play();

            yield return new WaitForSeconds(TimeToEndAnimation);

            for (int i = 0; i < _fxs.Count; i++)
            {
                Destroy(_fxs[i].gameObject);
            }

            Root.Animator.SetBool(ActionAnimationBool, false);
            GameHelper.SignalBus.OnSkillComplete?.Invoke();
        }
    }
}

