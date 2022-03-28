using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/MoveInteractablesAction")]
    public class MoveInteractablesAction : StateAction
    {

        [SerializeField] private GameObjectVariable currentInteractable;
        [SerializeField] private InteractablesContainer interactables;
        [SerializeField] private LayerMask groundMask;

        public override ActionResult Execute(float d, Object _manager)
        {
            
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return ActionResult.Pass;

            if (!Input.GetMouseButtonDown(1)) return ActionResult.Pass;
            
            Ray ray = manager.interactableRayProvider.CreateRay();
            if (Physics.Raycast(ray, out var groundHit, Mathf.Infinity, groundMask))
            {
                // TODO get this from dictionary instead
                GameTile tile = groundHit.transform.GetComponent<GameTile>();
                    
                // TODO check if tile is occupied
                    
                IInteractable currentSelection = interactables.Get(currentInteractable.Value.GetInstanceID());
                    

                // TODO this is shit
                int[] interactableIndices = BoardManager.WorldPosToGrid(currentInteractable.Value.transform.position);
                Vector2Int interactablePos = new Vector2Int(interactableIndices[0], interactableIndices[1]);
                    
                int[] tileIndices = BoardManager.WorldPosToGrid(groundHit.transform.position);
                Vector2Int tilePos = new Vector2Int(tileIndices[0], tileIndices[1]);

                int distance = BoardManager.ManhattanDistance(interactablePos, tilePos);
                if (distance <= currentSelection.RemainingMovement)
                {

                    currentInteractable.Value.transform.position = tile.SpawnPoint.transform.position;
                    currentSelection.Move(distance);
                }
                    

            }

            return ActionResult.Pass;
        }
    }
}
