using Leopotam.Ecs;
using UnityEngine;

namespace DungeonMaster
{
    public class Health
    {
        public bool IsFullHp => _currentHp == _maxHp;
        public bool IsLowHp => _currentHp <= _maxHp / 2f;

        private int _currentHp;
        private int _maxHp;
        private EcsEntity _entity;

        public Health(int maxHp, EcsEntity entity)
        {
            _maxHp = maxHp;
            _currentHp = maxHp;
            _entity = entity;
        }

        public void Damage(int value)
        {
            _currentHp = Mathf.Clamp(_currentHp - value, 0, _currentHp);
            if (_currentHp == 0)
            {
                _entity.Get<DeadEvent>();
            }
        }

        public void Heal(int value)
        {
            _currentHp = Mathf.Clamp(_currentHp + value, 0, _maxHp);
        }
    }
}

