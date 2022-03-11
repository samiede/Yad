using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/IM_PlayerIdleAction")]
    public class PlayerIdleAction : StateAction
    {

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
                    currentInteractable.Value = clickedInteractable;
                    allInteractables.Get(currentInteractable.Value.GetInstanceID())?.Select();
                }
            }

            
        }
    }
}
