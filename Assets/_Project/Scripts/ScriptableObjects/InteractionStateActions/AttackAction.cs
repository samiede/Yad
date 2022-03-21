using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/AttackAction")]
    public class AttackAction : StateAction
    {

        [SerializeField] private GameObjectVariable currentInteractable;
        [SerializeField] private GameObjectVariable currentHoveredEnemyInteractable;
        [SerializeField] private GameObjectGameEvent attackSelection;
        [SerializeField] private InteractablesContainer interactables;
        [SerializeField] private LayerMask interactableMask;
        [SerializeField]

        public override void Execute(float d, Object _manager)
        {
            
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return;
            
            // TODO this is shit
            int[] interactableIndices = BoardManager.WorldPosToGrid(currentInteractable.Value.transform.position);
            Vector2Int interactablePos = new Vector2Int(interactableIndices[0], interactableIndices[1]);
                    
            int[] tileIndices = BoardManager.WorldPosToGrid(currentHoveredEnemyInteractable.Value.transform.position);
            Vector2Int tilePos = new Vector2Int(tileIndices[0], tileIndices[1]);

            int distance = BoardManager.ManhattanDistance(interactablePos, tilePos);
            IInteractable currentSelectedInteractable = interactables.GetFriendly(currentInteractable.Value.GetInstanceID());
                  
            
            if (distance <= currentSelectedInteractable.PlaceableData.attackRange)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    attackSelection.Raise(currentHoveredEnemyInteractable.Value);
                }
            }

            return;
            
            Ray ray = manager.interactableRayProvider.CreateRay();
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, interactableMask))
            {
                
                if (interactables.IsEnemy(hit.transform.gameObject.GetInstanceID()))
                {
         
                    

                }
                

            }
            else
            {
                if (currentHoveredEnemyInteractable.Value != null) currentHoveredEnemyInteractable.Value = null; 
            }

        }
    }
}
