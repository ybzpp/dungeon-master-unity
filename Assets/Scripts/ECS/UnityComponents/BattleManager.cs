using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Leopotam.Ecs;

namespace DungeonMaster
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private float _startBattleDelay = 1f;
        [SerializeField] private float _nextAttackDelay = 1f;

        private List<Boy> _allBoys = new List<Boy>();
        private int _index;

        private void Start()
        {
            GameHelper.SignalBus.OnSkillComplete += Next;
        }

        private void OnDestroy()
        {
            GameHelper.SignalBus.OnSkillComplete -= Next;
        }
        public void Init()
        {
            _index = 0;
            _allBoys.Clear();
            foreach (Boy boy in GameHelper.Boys) _allBoys.Add(boy);
            foreach (Boy boy in GameHelper.BadBoys) _allBoys.Add(boy);
        }

        public void StartBattle()
        {
            StartCoroutine(StartBattleCoroutine());
        }

        public IEnumerator StartBattleCoroutine()
        {
            yield return new WaitForSeconds(_startBattleDelay);

            Init();
            BattleLogic();
        }

        public void BattleLogic()
        {
            StartCoroutine(BattleLogicCoroutine());
        }

        public IEnumerator BattleLogicCoroutine()
        {
            yield return null;

            var boy = _allBoys[_index % _allBoys.Count];
            boy.Entity.Get<SelectEvent>();

            var boys = GameHelper.GetPartyBoys(boy.IsParty);
            var useRandomSkill = true;

            var lowHpBoys = boys.Where(x => x.IsLowHp).ToList();
            if (lowHpBoys.Count > 0)
            {
                if (Random.Range(0, 2) * 2 - 1 == 1)
                {
                    var healSkill = boy.Skills.FirstOrDefault(x => x.Type == SkillType.Heal);
                    if (healSkill != default)
                    {
                        healSkill.StartSkill();
                        useRandomSkill = false;
                    }
                }
            }

            if (useRandomSkill)
            {
                var skillsWithoutHeal = boy.Skills.Where(x => x.Type != SkillType.Heal).ToList();
                if (skillsWithoutHeal.Count > 0)
                {
                    var skill = skillsWithoutHeal[Random.Range(0, skillsWithoutHeal.Count)];
                    skill.StartSkill();
                }
            }

            _index++;
        }

        public void Win()
        {
            StartCoroutine(WinCoroutine());
        }

        public IEnumerator WinCoroutine()
        {
            foreach (var boy in GameHelper.Boys)
            {
                boy.Animator.SetTrigger("Dance");
                boy.Animator.SetBool("Dancing", true);
            }
            GameHelper.Dungeon.CameraRig.SelectPosition("Dance");

            yield return new WaitForSeconds(5f);

            foreach (var boy in GameHelper.Boys) boy.Animator.SetBool("Dancing", false);
            GameHelper.Dungeon.CameraRig.SelectPosition("Base");
        }

        public void Next()
        {
            StartCoroutine(NextAttackCoroutine());
        }

        public IEnumerator NextAttackCoroutine()
        {
            if (!GameHelper.IsGameOver())
            {
                yield return new WaitForSeconds(_nextAttackDelay);

                _allBoys = _allBoys.Where(x => !x.IsDead).ToList();
                BattleLogic();
                GameHelper.SignalBus.OnNext?.Invoke();
            }
        }
    }
}