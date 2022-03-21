using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/InteractableEnemyHoverCondition")]
    public class InteractableEnemyHoverCondition : Condition
    {
        [SerializeField] private GameObjectVariable hoverEnemy;
        [SerializeField] private InteractablesContainer interactables;

        public override bool Check()
        {
            return hoverEnemy.Value && interactables.IsEnemy(hoverEnemy.Value.GetInstanceID());
        }
    }
}
