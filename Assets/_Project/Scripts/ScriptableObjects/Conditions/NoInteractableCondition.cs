using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/NoInteractablesCondition")]
    public class NoInteractableCondition : Condition
    {
        [SerializeField] private GameObjectVariable friendlyInteractable;
        [SerializeField] private GameObjectVariable enemyInteractable;
        public override bool Check()
        {
            return !friendlyInteractable.Value && !enemyInteractable.Value;
        }
    }
}
