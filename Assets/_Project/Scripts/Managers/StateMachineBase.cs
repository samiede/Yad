using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public abstract class StateMachineBase<T> : MonoBehaviour, IStateMachine<T> where T: BaseState
    {
        [SerializeField] protected T startState;
        [SerializeField] protected T currentState;
        
        public virtual void CheckForStateChange()
        {
            T nextState = currentState.ReturnNextState() as T;
            if (nextState == currentState || !nextState) return;
            currentState.PushStateBreak();
            nextState.SetManager(this);
            SetState(nextState);
        }

        public virtual void SetState(T state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }
    }
}
