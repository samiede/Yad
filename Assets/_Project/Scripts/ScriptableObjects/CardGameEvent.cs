using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    
    [CreateAssetMenu(fileName = "New Specific Game Event", menuName = "Events/Game Event - Card")]

    public class CardGameEvent : SpecificGameEvent<Card>
    {
    }
}
