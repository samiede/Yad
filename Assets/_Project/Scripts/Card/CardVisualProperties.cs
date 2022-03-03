using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    [System.Serializable]
    public class CardVisualProperties
    {
        // This has to have the ability to access the Material -> Shader -> Set all textures in the Card
        public TextMeshProUGUI text;
        public Texture tex;
        public MeshRenderer renderer;
        public Image img;
        public Element element;
    }
}