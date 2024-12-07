using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace DungeonMaster
{
    public class Boy : View
    {
        public bool IsFullHp => Entity.Get<HealthComponent>().Health.IsFullHp;
        public bool IsLowHp => Entity.Get<HealthComponent>().Health.IsLowHp;
        public bool IsDead => Entity.Has<DeadTag>() || Entity.Has<DeadEvent>();
        public bool IsParty => Entity.Has<PartyTag>();

        public Animator Animator => _animator;
        [SerializeField] private Animator _animator;
        public BoyDataSO Data => _data;
        [SerializeField] private BoyDataSO _data;
        public BaseSkill RandomSkill => _skills[Random.Range(0, _skills.Length)];
        public BaseSkill[] Skills => _skills;
        [SerializeField] private BaseSkill[] _skills;
        [SerializeField] private HealthView _healthView;
        public int Power => _data.PowerLevels[_level];
        private int _level;

        public override void Init(EcsEntity entity)
        {
            base.Init(entity);
            FindRefs();

            entity.Get<BoyRef>().Boy = this;

            var health = Power;
            if (_healthView)
            {
                _healthView.Init(entity);
                _healthView.UpdateHealth(health);
            }

            entity.Get<HealthComponent>() = new HealthComponent()
            {
                Health = new Health(health, entity),
                View = _healthView
            };
        }

        private void OnDestroy()
        {
            if (_healthView)
            {
                if (_healthView.Entity.IsAlive())
                    _healthView.Entity.Destroy();

                Destroy(_healthView.gameObject); 
            }
        }

        public void Damage(int value)
        {
            if (!IsDead)
            {
                _animator.SetTrigger("Damage");
            }
        }

        public void Heal(int value)
        {
        }

        public void Dead()
        {
            _animator.SetTrigger("Dead");
            Destroy(gameObject, 3f);
        }

        public void Respawn()
        {
        }

        public void FindRefs()
        {
            if (_skills == null || _skills.Length == 0)
            {
                _skills = GetComponentsInChildren<BaseSkill>();
            }

            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }

            if (_healthView == null)
            {
                _healthView = Instantiate(GameHelper.Config.HealthPrefab,transform);
            }
        }
    }
}

