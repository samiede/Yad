using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/MoveInteractablesAction")]
    public class MoveInteractablesAction : StateAction
    {

        [SerializeField] private GameObjectVariable currentInteractable;
        [SerializeField] private InteractableDictVariable playerInteractables;
        [SerializeField] private InteractableDictVariable enemyInteractables;
        [SerializeField] private InteractableDictVariable allInteractables;
        [SerializeField] private LayerMask groundMask;

        public override void Execute(float d, Object _manager)
        {
            
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return;

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = manager.interactableRayProvider.CreateRay();
                if (Physics.Raycast(ray, out var groundHit, Mathf.Infinity, groundMask))
                {
                    // TODO get this from dictionary instead
                    GameTile tile = groundHit.transform.GetComponent<GameTile>();
                    IInteractable currentSelection = allInteractables.Get(currentInteractable.Value.GetInstanceID());
                    
                    // TODO this is shit
                    int[] interactableIndices = MapGenerator.WorldPosToGrid(currentInteractable.Value.transform.position);
                    Vector2Int interactablePos = new Vector2Int(interactableIndices[0], interactableIndices[1]);
                    
                    int[] tileIndices = MapGenerator.WorldPosToGrid(groundHit.transform.position);
                    Vector2Int tilePos = new Vector2Int(tileIndices[0], tileIndices[1]);

                    int distance = MapGenerator.ManhattanDistance(interactablePos, tilePos);
                    if (distance <= currentSelection.RemainingMovement)
                    {

                        currentInteractable.Value.transform.position = tile.SpawnPoint.transform.position;
                        currentSelection.Move(distance);
                        // currentSelection.Deselect();
                        // currentInteractable.Value = null;
                    }


                    // GameObject clickedInteractable = hit.transform.gameObject;
                    // if (currentInteractable.Value.GetInstanceID() != clickedInteractable.GetInstanceID())
                    // {
                    //     allInteractables.Get(currentInteractable.Value.GetInstanceID()).Deselect();
                    //     currentInteractable.Value = clickedInteractable;
                    //     allInteractables.Get(currentInteractable.Value.GetInstanceID())?.Select();
                    //     
                    // }

                }
                // else
                // {
                //     if (currentInteractable.Value)
                //     {
                //         allInteractables.Get(currentInteractable.Value.GetInstanceID()).Deselect();
                //     }
                //     currentInteractable.Value = null;
                // }
            }

            
        }
    }
}
