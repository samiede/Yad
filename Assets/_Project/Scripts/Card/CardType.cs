using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public abstract class CardType : ScriptableObject
    {
        public string typeName;
        
        public virtual void OnSetType(Card card)
        {
            Element t = Settings.GetResourcesManager().typeElement;
            CardVisualProperties type = card.GetProperty(t);
            type?.text.SetText(typeName);
        }

    }
}
