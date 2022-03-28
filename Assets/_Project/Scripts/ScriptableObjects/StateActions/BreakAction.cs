using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    
    [CreateAssetMenu(menuName = "Action/BreakAction")]
    public class BreakAction : StateAction
    {
        public override ActionResult Execute(float d, Object manager)
        {
            return ActionResult.Break;
        }
    }
}
