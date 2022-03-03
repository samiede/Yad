using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "CardHandlerActions/HandleCardInteractionAction")]

    public class PlayerTurnCardHandlingAction : StateAction
    {
        public override void Execute(float d, Object _manager)
        {
            CardManager manager = _manager as CardManager;
            if (!manager) return;
            
            # region hover over card

            if (!manager.Dragging && !manager.Placeable)
            {
                Ray ray = manager.CardRayProvider.CreateRay();
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << 31))
                {

                    GameObject hoveredCard = hit.transform.gameObject;
                    if (!manager.CurrentSelectedCard || manager.CurrentSelectedCard.GetInstanceID() != hoveredCard.GetInstanceID())
                    {
                        if (manager.CurrentSelectedCard) manager.HandCards[manager.CurrentSelectedCard.GetInstanceID()].MouseExitEvent();
                        manager.CurrentSelectedCard = hoveredCard;
                        manager.HandCards[manager.CurrentSelectedCard.GetInstanceID()].MouseEnterEvent(manager.Dragging);
                    }


                }
                else
                {
                    if (manager.CurrentSelectedCard)
                    {
                        manager.HandCards[manager.CurrentSelectedCard.GetInstanceID()].MouseExitEvent();
                        manager.CurrentSelectedCard = null;
                    }
                }
            }

            #endregion
    
            #region dragging and casting
            if (manager.CurrentSelectedCard)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    manager.Dragging = true;
                    manager.HandCards[manager.CurrentSelectedCard.GetInstanceID()].OnMouseDownEvent();
                    
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    manager.HandCards[manager.CurrentSelectedCard.GetInstanceID()].OnMouseUpAsButtonEvent(manager.Dragging);
                    manager.Dragging = false;
                    
                }

                if (manager.Dragging)
                {
                    manager.HandCards[manager.CurrentSelectedCard.GetInstanceID()].MouseDragEvent();

                }
            }
            #endregion

            #region placeeables

            if (manager.Placeable)
            {
                Ray groundRay = manager.PlaceableRayProvider.CreateRay();
                if (Physics.Raycast(groundRay, out var groundHit, Mathf.Infinity, 1 << 3))
                {
                    GameTile tile = groundHit.transform.GetComponent<GameTile>();
                    manager.Placeable.SetActive(true);
                    manager.Placeable.transform.position = tile.SpawnPoint.transform.position;
                    
                    if (Input.GetMouseButtonDown(0))
                    {
                        manager.Placeable = null;

                    }
                }
                
                
            }
            
            #endregion
            
        }
        
    }
}
