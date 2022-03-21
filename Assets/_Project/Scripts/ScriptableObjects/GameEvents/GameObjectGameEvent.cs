using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{   
    [CreateAssetMenu(fileName = "New Specific Game Event", menuName = "Events/Game Event - Game Object")]

    public class GameObjectGameEvent : SpecificGameEvent<GameObject, GameObjectEvent> { }
    
}
