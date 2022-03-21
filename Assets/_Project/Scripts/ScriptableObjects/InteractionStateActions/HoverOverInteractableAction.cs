using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/HoverOverInteractablesAction")]
    public class HoverOverInteractableAction : StateAction
    {
        [SerializeField] private GameObjectVariable currentlyHoveringOver;
        [SerializeField] private InteractablesContainer interactables;
        [SerializeField] private LayerMask interactableMask;
        public override void Execute(float d, Object _manager)
        {
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return;
            
            Ray ray = manager.interactableRayProvider.CreateRay();
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, interactableMask))
            {
                if (currentlyHoveringOver.Value != null && currentlyHoveringOver.Value.GetInstanceID() == hit.transform.gameObject.GetInstanceID()) return;
                if (interactables.IsEnemy(hit.transform.gameObject.GetInstanceID()))
                    currentlyHoveringOver.Value = hit.transform.gameObject;

            }
            else
            {
                currentlyHoveringOver.Value = null;
            }
        }
    }
}

