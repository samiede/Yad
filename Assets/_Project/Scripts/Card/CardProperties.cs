using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    [System.Serializable]
    public class CardProperties
    {
        
        // This needs values for all card textures
        public string stringValue;
        public Texture texture;
        public Sprite sprite;
        public int intValue;
        public Element element;

    }
}
