using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
namespace Deckbuilder
{

    public enum ActionResult
    {
        Pass,
        Break
    }
    
    public abstract class BaseState : ScriptableObject
    {
        [SerializeField] private StateAction BreakAction;
        [SerializeField] private BaseTransition[] _anyTransitions;
        [SerializeField] private BaseTransition[] _transitions;
        [SerializeField] private List<StateAction> onTickActions;
        [SerializeField] private List<StateAction> onEnterActions;
        [SerializeField] private List<StateAction> onExitActions;

        private ListStack<StateAction> _onTickStack;
        private ListStack<StateAction> _onEnterStack;
        private ListStack<StateAction> _onExitStack;

        private Object manager;
        
        public void SetManager(Object _manager)
        {
            manager = _manager;
        
        }

        private void ExecuteActions(ListStack<StateAction> actions, float d = 0) {
            while (actions.Count > 0)
            {
                var res = actions.Pop().Execute(d, manager);
                if (res != ActionResult.Pass) break;
            }

        }
        
        public void OnEnter()
        {
            _onEnterStack = new ListStack<StateAction>(onEnterActions);
            ExecuteActions(_onEnterStack);
        }

        public void OnExit()
        {
            _onExitStack = new ListStack<StateAction>(onExitActions);
            ExecuteActions(_onExitStack);
        }
        
        public void Tick(float d)
        {
            _onTickStack = new ListStack<StateAction>(onTickActions);
            ExecuteActions(_onTickStack, d);
        }

        public void PushStateBreak()
        {
            _onTickStack.PushToBack(BreakAction);
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
