using TMPro;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Actions/EnterTurnUIUpdate")]
    public class EnterTurnUIUpdate: StateAction
    {
        [SerializeField] private string buttonText;
        [SerializeField] private GenericGameEvent onEnterTurn;
        [SerializeField] private RunStats stats;
        
        public override ActionResult Execute(float d, Object manager)
        {
            stats.turnButtonText = buttonText;
            onEnterTurn.Raise();
            return ActionResult.Pass;
        }
    }
}