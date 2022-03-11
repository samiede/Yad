using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/InteractablesCondition")]
    public class InteractablesCondition : Condition
    {
        [SerializeField] private GameObjectVariable interactable;
        public override bool Check()
        {
            return interactable.Value;
        }
    }
}
