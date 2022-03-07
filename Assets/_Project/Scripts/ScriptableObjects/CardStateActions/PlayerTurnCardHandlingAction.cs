using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "CardHandlerActions/HandleCardInteractionAction")]
    public class PlayerTurnCardHandlingAction : StateAction
    {
        [SerializeField] private GameObjectVariable currentSelectedCard;
        [SerializeField] private GameObjectVariable placeable;
        [SerializeField] private BoolVariable dragging;
        public override void Execute(float d, Object _manager)
        {
            CardManager manager = _manager as CardManager;
            if (!manager) return;
            
            # region hover over card

            if (!dragging && !placeable.Value)
            {
                Ray ray = manager.CardRayProvider.CreateRay();
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << 31))
                {

                    GameObject hoveredCard = hit.transform.gameObject;
                    if (!currentSelectedCard.Value || currentSelectedCard.Value.GetInstanceID() != hoveredCard.GetInstanceID())
                    {
                        if (currentSelectedCard.Value) manager.HandCards[currentSelectedCard.Value.GetInstanceID()].MouseExitEvent();
                        currentSelectedCard.Value = hoveredCard;
                        manager.HandCards[currentSelectedCard.Value.GetInstanceID()].MouseEnterEvent(dragging.Value);
                    }


                }
                else
                {
                    if (currentSelectedCard.Value)
                    {
                        manager.HandCards[currentSelectedCard.Value.GetInstanceID()].MouseExitEvent();
                        currentSelectedCard.Value = null;
                    }
                }
            }

            #endregion
    
            #region dragging and casting
            if (currentSelectedCard.Value)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    dragging.Value = true;
                    manager.HandCards[currentSelectedCard.Value.GetInstanceID()].OnMouseDownEvent();
                    
                }
                
                if (Input.GetMouseButtonUp(0))
                {
                    manager.HandCards[currentSelectedCard.Value.GetInstanceID()].OnMouseUpAsButtonEvent(dragging.Value);
                    dragging.Value = false;
                    
                }

                if (dragging.Value)
                {
                    manager.HandCards[currentSelectedCard.Value.GetInstanceID()].MouseDragEvent();

                }
            }
            #endregion
            
            
        }
        
    }
}
