using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "InteractionManagerActions/HandleInteractablePlaceablesAction")]

    public class PlayerTurnPlaceablesAction : StateAction
    {


        [SerializeField] private GameObjectVariable placeable;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private CardGameEvent cardPositionChosen;
        [SerializeField] private CardVariable currentCastCard;

        public override ActionResult Execute(float d, Object _manager)
        {
                        
            InteractionManager manager = _manager as InteractionManager;
            if (!manager) return ActionResult.Pass;
                    
            Ray groundRay = manager.interactableRayProvider.CreateRay();
            if (Physics.Raycast(groundRay, out var groundHit, Mathf.Infinity, groundMask))
            {
                // TODO get this from dictionary instead
                GameTile tile = groundHit.transform.GetComponent<GameTile>();
                placeable.Value.SetActive(true);
                placeable.Value.transform.position = tile.SpawnPoint.transform.position;
                
                if (Input.GetMouseButtonDown(0))
                {
                    cardPositionChosen.Raise(currentCastCard.Value);
                }
            }

            return ActionResult.Pass;

        }
    }
}
