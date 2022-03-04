using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    [System.Serializable]
    public class CardEvent : UnityEvent<Card> { }
    public class CardGameEventListener : SpecificGameEventListener<Card, CardEvent>
    {
    }
}
