using UnityEngine;

namespace Deckbuilder
{        
    [CreateAssetMenu(menuName = "InteractionManagerActions/HandleInteractableInteractionAction")]
    public class PlayerTurnInteractionAction: StateAction
    {

        [SerializeField] private GameObjectVariable currentInteractable;
        [SerializeField] private LayerMask layerMask;
        public override void Execute(float d, Object _manager)
        {
            
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = manager.interactableRayProvider.CreateRay();
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, layerMask))
                {

                    GameObject clickedInteractable = hit.transform.gameObject;
                    if (!currentInteractable.Value || currentInteractable.Value.GetInstanceID() != clickedInteractable.GetInstanceID())
                    {
                        if (currentInteractable.Value) manager.playerInteractables[currentInteractable.Value.GetInstanceID()].Deselect();
                        currentInteractable.Value = clickedInteractable;
                        manager.playerInteractables[currentInteractable.Value.GetInstanceID()].Select();
                    }
                    // if (!currentSelectedCard.Value || currentSelectedCard.Value.GetInstanceID() != hoveredCard.GetInstanceID())
                    // {
                    //     if (currentSelectedCard.Value) manager.HandCards[currentSelectedCard.Value.GetInstanceID()].MouseExitEvent();
                    //     currentSelectedCard.Value = hoveredCard;
                    //     manager.HandCards[currentSelectedCard.Value.GetInstanceID()].MouseEnterEvent(dragging.Value);
                    // }


                }
            }
            
        }
    }
}