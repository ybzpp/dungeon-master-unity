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
        [SerializeField] private float _continueDungeonDelay = 1f;

        private List<Boy> _allBoys = new List<Boy>();
        private int _index;

        private void Start()
        {
            GameHelper.SignalBus.OnSkillComplete += SkillComplete;
        }

        private void OnDestroy()
        {
            GameHelper.SignalBus.OnSkillComplete -= SkillComplete;
        }

        public void Init()
        {
            _index = 0;
            _allBoys.Clear();
            foreach (Boy boy in BoysService.Boys) _allBoys.Add(boy);
            foreach (Boy boy in BoysService.BadBoys) _allBoys.Add(boy);
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

            var boys = BoysService.GetBoys(boy.IsParty);
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

        public void SkillComplete()
        {
            GameHelper.NewEntity.Get<CheckGameOverEvent>();
        }

        public void ContinueDungeon()
        {
            StartCoroutine(ContinueDungeonCoroutine());
        }

        public IEnumerator ContinueDungeonCoroutine()
        {
            yield return new WaitForSeconds(_continueDungeonDelay);
            ContinueBattle();
        }

        public void ContinueBattle()
        {
            _allBoys = _allBoys.Where(x => !x.IsDead).ToList();
            BattleLogic();
        }
    }
}