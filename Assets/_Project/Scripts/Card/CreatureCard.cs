using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Cards/Creature")]
    public class CreatureCard : CardType
    {
        
        public override void OnSetType(Card card)
        {
            base.OnSetType(card);
            
            card.statsHolder.SetActive(true);

        }
    }
}
