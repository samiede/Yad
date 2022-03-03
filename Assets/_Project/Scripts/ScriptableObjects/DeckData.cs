using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(fileName = "NewDeck", menuName = "Deckbuilder/Deck Data")]

    public class DeckData : ScriptableObject
    {
        [SerializeField] private List<CardData> cards;
        private int currentCard = 0;
        
        
        public CardData GetNextCardFromDeck()
        {
            //advance the index
            currentCard++;
            if(currentCard >= cards.Count)
                currentCard = 0;

            return cards[currentCard];
        }

    }
}
