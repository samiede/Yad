using UnityEngine;

namespace Deckbuilder
{        
    [CreateAssetMenu(menuName = "InteractionManagerActions/SelectDeselectInteractablesAction")]
    public class SelectDeselectInteractablesAction: StateAction
    {

        [SerializeField] private GameObjectVariable currentFriendlyInteractable;
        [SerializeField] private GameObjectVariable currentEnemyInteractable;
        [SerializeField] private InteractablesContainer interactables;
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
                    GameObject currentInteractable =
                        currentFriendlyInteractable.Value ? currentFriendlyInteractable.Value : currentEnemyInteractable.Value;
                    if (currentInteractable.GetInstanceID() != clickedInteractable.GetInstanceID())
                    {
                        interactables.Get(currentInteractable.GetInstanceID()).Deselect();
                        interactables.Get(clickedInteractable.GetInstanceID())?.Select();
                        if (interactables.IsFriendly(clickedInteractable.GetInstanceID()))
                        {
                            currentFriendlyInteractable.Value = clickedInteractable;
                            currentEnemyInteractable.Value = null;
                        }
                        else
                        {
                            currentEnemyInteractable.Value = clickedInteractable;
                            currentFriendlyInteractable.Value = null;
                        }
                        
                    }
                    
                }
                else
                {
                    if (currentFriendlyInteractable.Value)
                    {
                        interactables.Get(currentFriendlyInteractable.Value.GetInstanceID()).Deselect();
                        currentFriendlyInteractable.Value = null;
                    }
                    
                    if (currentEnemyInteractable.Value)
                    {
                        interactables.Get(currentEnemyInteractable.Value.GetInstanceID()).Deselect();
                        currentEnemyInteractable.Value = null;
                    }
                }
            }

            
        }
    }
}