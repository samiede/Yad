using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    
    [CreateAssetMenu(fileName = "New Specific Game Event", menuName = "Events/Game Event - Card")]

    public class CardGameEvent : SpecificGameEvent<Card, CardEvent>
    {
    }
}
