using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace DungeonMaster
{
    public class HealthView : View
    {
        [SerializeField] private TextMeshPro _healthText;

        public void UpdateHealth(int value)
        {
            _healthText.text = $"{TextHelper.PowerText} {value}";
        }

        public void RotateToCamera(Vector3 dir)
        {
            _healthText.transform.forward = dir;
        }

        public override void Init(EcsEntity entity)
        {
            base.Init(entity);
            entity.Get<HealthViewRef>().HealthView = this;
        }
    }
}
