using Deckbuilder.ScriptableObjectArchitecture;
using UnityEngine;

namespace Deckbuilder
{
    // public sealed class GameObjectGameEventListener: BaseVariableGameEventListener<GameObject, GameEventBase<GameObject>, GameObjectEvent> {}

    public class GameObjectGameEventListener : SpecificGameEventListener<GameObject, GameObjectEvent>
    {
    }
    
    
}