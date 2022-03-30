using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{        
    [CreateAssetMenu(menuName = "Conditions/FriendlyInteractableSkillCondition")]
    public class FriendlyInteractableSkillCondition : Condition
    {

        [SerializeField] private GameObjectVariable friendlyInteractable;
        [SerializeField] private SkillVariable currentSkill;
        public override bool Check()
        {
            return friendlyInteractable.Value && currentSkill.Value;
        }
        
    }
}