using UnityEngine.UI;

namespace Deckbuilder
{
    public interface IInteractable
    {
        public void Select();
        public void Deselect();

        public void Initialize(PlaceableData data);
    }
}