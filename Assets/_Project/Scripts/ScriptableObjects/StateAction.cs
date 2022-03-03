using UnityEngine;

namespace Deckbuilder
{

    public abstract class BaseAction : ScriptableObject
    {
    }

    public abstract class StateAction : BaseAction
    {
        public abstract void Execute(float d, Object manager);

    }

}