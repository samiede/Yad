using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/InteractableNoEnemyHoverCondition")]
    public class InteractableNoEnemyHoverCondition : Condition
    {
        [SerializeField] private GameObjectVariable hoverEnemy;
        public override bool Check()
        {
            return !hoverEnemy.Value;
        }
    }
}
