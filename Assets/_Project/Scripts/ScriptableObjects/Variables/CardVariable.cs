using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    

    [CreateAssetMenu(
        fileName = "CardVariable.asset",
        menuName = "Variables/Variable_"+ "Card",
        order = 5)]
    public sealed class CardVariable : BaseVariable<Card, CardEvent>
    {
    }

    
}

