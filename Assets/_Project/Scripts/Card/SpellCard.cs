using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Cards/Spell")]
    public class SpellCard : CardType
    {
        public override void OnSetType(Card card)
        {
            base.OnSetType(card);
            
            card.statsHolder.SetActive(false);
        }
    }
}
