using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "CardHandlerActions/CardDraggingAction")]

    public class CardDraggingAction : StateAction
    {
        
        [SerializeField] private GameObjectVariable currentSelectedCardObject;
        [SerializeField] private BoolVariable dragging;


        public override ActionResult Execute(float d, Object _manager)
        {
            CardManager manager = _manager as CardManager;
            if (!manager) return ActionResult.Pass;
            
            manager.HandCards[currentSelectedCardObject.Value.GetInstanceID()].MouseDragEvent();
            
            if (Input.GetMouseButtonUp(0))
            {
                manager.HandCards[currentSelectedCardObject.Value.GetInstanceID()].OnMouseUpAsButtonEvent(dragging.Value);
                dragging.Value = false;
                    
            }

            return ActionResult.Pass;
        }
    }
}
