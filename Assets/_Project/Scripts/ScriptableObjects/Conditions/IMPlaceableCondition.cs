using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/PlaceableCondition")]
    public class IMPlaceableCondition : Condition
    {
        [SerializeField] private GameObjectVariable placeable;
        public override bool Check()
        {
            return (bool) placeable.Value;
        }
    }
}
