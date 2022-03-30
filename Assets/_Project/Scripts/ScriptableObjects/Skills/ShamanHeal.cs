using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName ="Skills/ShamanHeal")]
    public class ShamanHeal : BaseSkill
    {
        [Range(0, 200)]
        [SerializeField] private float healAmount;
        public override void Execute(Unit caster, GameObject target)
        {
            base.Execute(caster, target);
            var targetInteractable = interactables.Get(target.GetInstanceID());
            if (targetEffect) Instantiate(targetEffect, target.transform.position, Quaternion.identity);
            targetInteractable.RestoreHealth(healAmount);
        }
    }
}
