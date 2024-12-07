using System;
using UnityEngine;

namespace DungeonMaster
{
    public enum SkillType
    {
        Attack,
        Heal,
        Buff
    }

    public class BaseTimeSkill : BaseSkill
    {
        public float TimeToSpawnFx = 1f;
        public float TimeToAction = .7f;
        public float TimeToEndAnimation = .5f;
        public bool Freeze;
    }

    public class BaseSkill : MonoBehaviour
    {
        public Boy Root => _root;
        private Boy _root;
        public SkillType Type => _type;
        [SerializeField] private SkillType _type;

        public readonly string ActionAnimationBool = "Action";
        [SerializeField] private AnimationClip _actionAnimation;

        private void Start()
        {
            _root = transform.parent.GetComponent<Boy>();
        }

        public virtual void StartSkill()
        {
            var animator = _root.Animator;
            AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
            animatorOverrideController["Action"] = _actionAnimation;
            animator.runtimeAnimatorController = animatorOverrideController;

            Debug.Log($"StartSkill:{gameObject.name} attackBoy:{_root.name}");
        }

        public virtual void TakeDamege() { }
    }
}