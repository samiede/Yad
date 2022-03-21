using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/IM_PlayerIdleAction")]
    public class PlayerIdleAction : StateAction
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

            
        }
    }
}
