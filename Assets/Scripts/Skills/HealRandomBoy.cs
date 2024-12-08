using System.Collections;
using System.Linq;
using UnityEngine;

namespace DungeonMaster
{
    public class HealRandomBoy : BaseTimeSkill
    {
        [SerializeField] private ParticleSystem _healFx;

        private Boy _targetBoy;

        public override void StartSkill()
        {
            base.StartSkill();

            var boys = BoysService.GetBoys(Root.IsParty);
            boys = boys.Where(x => !x.IsFullHp).ToList();
            if (boys.Count > 0)
            {
                _targetBoy = boys[UnityEngine.Random.Range(0, boys.Count)];
                StartCoroutine(SkillAnimation());
            }
            else
            {
                Debug.Log("Boy is not FullHp not find");
                GameHelper.SignalBus.OnSkillComplete?.Invoke();
            }
        }


        public IEnumerator SkillAnimation()
        {
            Root.Animator.SetBool(ActionAnimationBool, true);

            yield return new WaitForSeconds(TimeToSpawnFx);

            _targetBoy.Heal(Root.Power);
            _healFx.transform.position = _targetBoy.transform.position;
            _healFx.Play();

            yield return new WaitForSeconds(TimeToEndAnimation);

            Root.Animator.SetBool(ActionAnimationBool, false);
            _targetBoy = null;
            GameHelper.SignalBus.OnSkillComplete?.Invoke();
        }
    }
}

