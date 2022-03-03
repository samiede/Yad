using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Conditions/PlayerTurnCondition")]
    public class PlayerTurnCondition : Condition
    {
        [SerializeField] private BoolVariable playerTurn;
        public override bool Check()
        {
            return playerTurn.Value;
        }
    }
}
