using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    public class InteractablesUI : MonoBehaviour
    {
        [SerializeField] private GameObjectVariable currentSelectedFriendInteractable;
        [SerializeField] private GameObjectVariable currentSelectedEnemyInteractable;
        [SerializeField] private InteractablesContainer interactables;
        [SerializeField] private GameObject interactablesPanel;
        [SerializeField] private RawImage interactablesImage;
        [SerializeField] private TextMeshProUGUI interactablesName;


        public void DisplayCurrentInteractablePanel()
        {
            if (currentSelectedEnemyInteractable.Value == null && currentSelectedFriendInteractable.Value == null) return;
            
            GameObject currentInteractableGO = currentSelectedFriendInteractable.Value ? currentSelectedFriendInteractable.Value : currentSelectedEnemyInteractable.Value;
            IInteractable currentInteractable = interactables.Get(currentInteractableGO.GetInstanceID());
            interactablesPanel.SetActive(true);
            interactablesName.SetText(currentInteractable.PlaceableData.name);
            if (currentInteractable.PlaceableData.image)
            {
                interactablesImage.texture = currentInteractable.PlaceableData.image;
            }
        }

        public void HideInteractablePanel()
        {
            interactablesPanel.SetActive(false);
            Debug.Log("Hide");
        }
        

    }
}
