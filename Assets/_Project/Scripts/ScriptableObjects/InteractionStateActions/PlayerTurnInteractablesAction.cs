using UnityEngine;

namespace Deckbuilder
{        
    [CreateAssetMenu(menuName = "InteractionManagerActions/HandleInteractableInteractablesAction")]
    public class PlayerTurnInteractablesAction: StateAction
    {

        [SerializeField] private GameObjectVariable placeable;
        [SerializeField] private GameObjectVariable currentInteractable;
        [SerializeField] private InteractableDictVariable playerInteractables;
        [SerializeField] private InteractableDictVariable enemyInteractables;
        [SerializeField] private InteractableDictVariable allInteractables;
        [SerializeField] private LayerMask interactableMask;

        public override void Execute(float d, Object _manager)
        {
            
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = manager.interactableRayProvider.CreateRay();
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, interactableMask))
                {

                    GameObject clickedInteractable = hit.transform.gameObject;
                    if (!placeable.Value && (!currentInteractable.Value || currentInteractable.Value.GetInstanceID() != clickedInteractable.GetInstanceID()))
                    {
                        if (currentInteractable.Value) allInteractables.Get(currentInteractable.Value.GetInstanceID()).Deselect();
                        currentInteractable.Value = clickedInteractable;
                        
                        allInteractables.Get(currentInteractable.Value.GetInstanceID())?.Select();
                        
                    }
                    
                }
                else
                {
                    if (currentInteractable.Value)
                    {
                        allInteractables.Get(currentInteractable.Value.GetInstanceID()).Deselect();
                    }
                    currentInteractable.Value = null;
                }
            }

            
        }
    }
}