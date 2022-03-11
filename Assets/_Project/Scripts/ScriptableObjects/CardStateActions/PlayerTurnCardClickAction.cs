using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "CardHandlerActions/PlayerTurnCardClickAction")]
    public class PlayerTurnCardClickAction : StateAction
    {
        [SerializeField] private GameObjectVariable currentSelectedCardObject;
        [SerializeField] private BoolVariable dragging;
        public override void Execute(float d, Object _manager)
        {
            var manager = _manager as CardManager;
            if (!manager) return;
            
            if (currentSelectedCardObject.Value)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    dragging.Value = true;
                    manager.HandCards[currentSelectedCardObject.Value.GetInstanceID()].OnMouseDownEvent();
                    
                }

            }
        }
    }
}
