using UnityEngine;

namespace Deckbuilder
{

    public abstract class BaseAction : ScriptableObject
    {
    }

    public abstract class StateAction : BaseAction
    {
        public abstract ActionResult Execute(float d, Object manager);

    }

}