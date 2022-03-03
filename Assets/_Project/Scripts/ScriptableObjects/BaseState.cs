using UnityEngine;
using Object = UnityEngine.Object;
namespace Deckbuilder
{
    
    public abstract class BaseState : ScriptableObject
    {
        [SerializeField] private BaseTransition[] _anyTransitions;
        [SerializeField] private BaseTransition[] _transitions;
        [SerializeField] private StateAction[] onTickActions;
        [SerializeField] private StateAction[] onEnterActions;
        [SerializeField] private StateAction[] onExitActions;
        
        private Object manager;
        
        public void SetManager(Object _manager)
        {
            manager = _manager;
        
        }
        protected void ExecuteActions(StateAction[] actions, float d = 0) {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Execute(d, manager);
            }
        }
        
        public void OnEnter()
        {
            ExecuteActions(onEnterActions);
        }

        public void OnExit()
        {
            ExecuteActions(onExitActions);
        }
        
        public void Tick(float d)
        {
            ExecuteActions(onTickActions, d);
        }

        public BaseState ReturnNextState()
        {
            BaseState currentState = this;

            foreach (var anyTransition in _anyTransitions)
            {
                BaseState nextState = anyTransition.GetNextStateOnCondition(currentState);
                if (nextState != currentState)
                {
                    currentState = nextState;
                    return currentState;
                }
            }
            
            foreach (var transition in _transitions)
            {
                BaseState nextState = transition.GetNextStateOnCondition();
                if (nextState != currentState)
                {
                    currentState = nextState;
                    break;
                }
            }

            return currentState;
        }

    }
    
}
