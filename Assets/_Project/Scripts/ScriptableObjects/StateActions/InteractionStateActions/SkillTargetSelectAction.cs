using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/SkillTargetSelectAction")]
    public class SkillTargetSelectAction : StateAction
    {

        [SerializeField] private GameObjectVariable currentFriendlyInteractable;
        [SerializeField] private SkillVariable currentSkill;
        [SerializeField] private InteractablesContainer interactables;
        [SerializeField] private LayerMask interactableMask;

        public override ActionResult Execute(float d, Object _manager)
        {
             InteractionManager manager = _manager as InteractionManager;
            if (!manager) return ActionResult.Pass;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = manager.interactableRayProvider.CreateRay();
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, interactableMask))
                {

                    GameObject clickedObject = hit.transform.gameObject;
                    IInteractable clickedInteractable = interactables.Get(clickedObject.GetInstanceID());
                    IInteractable casterInteractable = interactables.GetFriendly(currentFriendlyInteractable.Value.GetInstanceID());
                    // TODO check if valid target
                    
                    currentSkill.Value.Execute(casterInteractable.GetUnit(), clickedInteractable.GetGameObject());
                    currentSkill.Value = null;

                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentSkill.Value = null;
            }

            return ActionResult.Pass;
        }
    }
}
