using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Actions/SetDefaultCursorAction")]
    public class SetDefaultCursorAction : StateAction
    {
        public override ActionResult Execute(float d, Object manager)
        {
            GameManager.SetDefaultCursor();
            return ActionResult.Pass;
        }
    }
}
