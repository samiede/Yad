using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "CardHandlerActions/HandleCardHoverInteractionAction")]
    public class CardHoverAction : StateAction
    {
        [SerializeField] private GameObjectVariable currentSelectedCardObject;
        [SerializeField] private BoolVariable dragging;
        public override void Execute(float d, Object _manager)
        {
            CardManager manager = _manager as CardManager;
            if (!manager) return;
            
            Ray ray = manager.CardRayProvider.CreateRay();
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, 1 << 31))
            {

                GameObject hoveredCard = hit.transform.gameObject;
                if (!currentSelectedCardObject.Value || currentSelectedCardObject.Value.GetInstanceID() != hoveredCard.GetInstanceID())
                {
                    if (currentSelectedCardObject.Value) manager.HandCards[currentSelectedCardObject.Value.GetInstanceID()].MouseExitEvent();
                    currentSelectedCardObject.Value = hoveredCard;
                    manager.HandCards[currentSelectedCardObject.Value.GetInstanceID()].MouseEnterEvent(dragging.Value);
                }

            }
            else
            {
                if (currentSelectedCardObject.Value)
                {
                    manager.HandCards[currentSelectedCardObject.Value.GetInstanceID()].MouseExitEvent();
                    currentSelectedCardObject.Value = null;
                }
            }
        }
        
    }
}
