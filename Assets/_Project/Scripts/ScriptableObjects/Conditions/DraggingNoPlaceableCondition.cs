using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/DraggingNoPlaceableCondition")]
    public class DraggingNoPlaceableCondition : Condition
    {
        [SerializeField] private BoolVariable dragging;
        [SerializeField] private GameObjectVariable placeable;
        public override bool Check()
        {
            return dragging.Value && placeable.Value == null;
        }
    }
}
