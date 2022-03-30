using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public abstract class BaseSkill : ScriptableObject
    {
        [Header("Visuals")] 
        [SerializeField] protected GameObject targetEffect;
        [SerializeField] protected GameObject castEffect;
        [SerializeField] protected AnimationClip animationClip;
        [SerializeField] protected AudioClip castSoundEffect;
        [SerializeField] protected Texture skillImage;
        public Texture SkillImage => skillImage;
        public string skillName;

        [Header("Runtime Variables")] 
        public int cost;
        public int numUsages;
        
        [Header("Target Variables")] 
        public bool targetSelf;
        [SerializeField] protected InteractablesContainer interactables;
        public BasePlaceable.Faction targetFaction;

        private Animator _animator;
        

        public virtual void Execute(Unit caster, GameObject target)
        {
            _animator = caster.Animator;
            OverrideAnimation();
            _animator.SetTrigger("Skill");
            if (castEffect) Instantiate(castEffect, caster.skillEffectSpawnPoint, Quaternion.identity);
            if (castSoundEffect) caster.AudioSource.PlayOneShot(castSoundEffect);

            
        }

        private void OverrideAnimation()
        {
            var animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController)
            {
                ["DefaultSpell"] = animationClip
            };
            _animator.runtimeAnimatorController = animatorOverrideController;

        }


    }
}
