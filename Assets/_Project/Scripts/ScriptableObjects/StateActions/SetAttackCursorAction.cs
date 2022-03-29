using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Actions/SetAttackCursorAction")]
    public class SetAttackCursorAction : StateAction
    {
        public override ActionResult Execute(float d, Object manager)
        {
            GameManager.SetAttackCursor();
            return ActionResult.Pass;
        }
    }
}
