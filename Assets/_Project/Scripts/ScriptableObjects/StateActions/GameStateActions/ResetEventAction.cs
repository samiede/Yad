using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class ResetEventAction : StateAction
    {
        [SerializeField] private string buttonText;
        [SerializeField] private GenericGameEvent onEnterTurn;
        [SerializeField] private RunStats stats;
        
        public override ActionResult Execute(float d, Object manager)
        {
            stats.turnButtonText = buttonText;
            onEnterTurn.Raise();
            return ActionResult.Pass;
        }
    }
}
