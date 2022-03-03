using UnityEngine;

namespace Deckbuilder
{
    
    
    [System.Serializable]
    public class BaseTransition
    {
        [SerializeField] private BaseState from;
        [SerializeField] private BaseState to;
        [SerializeField] private Condition _condition;

        public BaseState GetNextStateOnCondition()
        {
            return _condition.Check() ? to : from;
        }   
        
        public BaseState GetNextStateOnCondition(BaseState f)
        {
            return _condition.Check() ? to : f;
        }
    }
}