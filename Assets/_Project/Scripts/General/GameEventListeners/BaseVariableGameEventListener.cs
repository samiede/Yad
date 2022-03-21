using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseVariableGameEventListener<TType, TEvent, TResponse> : MonoBehaviour, IGameEventListener<TType>
where TEvent : GameEventBase<TType>
where TResponse : UnityEvent<TType>
    { 
        protected ScriptableObject GameEvent { get { return _event; } }
        protected UnityEventBase Response { get { return _response; } }

        // [SerializeField]
        private TEvent _previouslyRegisteredEvent = default(TEvent);
        [SerializeField]
        private TEvent _event = default(TEvent);
        [SerializeField]
        private TResponse _response = default(TResponse);
        

        public void OnEventRaised(TType value)
        {
            RaiseResponse(value);
            
        }
        private void RaiseResponse(TType value)
        {
            _response.Invoke(value);
        }
        private void OnEnable()
        {
            if (_event != null)
                Register();
        }
        private void OnDisable()
        {
            if (_event != null)
                _event.RemoveListener(this);
        }
        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }
    public abstract class BaseVariableGameEventListener<TEvent, TResponse> : MonoBehaviour, IGameEventListener
        where TEvent : GameEventBase
        where TResponse : UnityEvent
    {
        protected ScriptableObject GameEvent { get { return _event; } }
        protected UnityEventBase Response { get { return _response; } }

        [SerializeField]
        private TEvent _previouslyRegisteredEvent = default(TEvent);
        [SerializeField]
        private TEvent _event = default(TEvent);
        [SerializeField]
        private TResponse _response = default(TResponse);

        public void OnEventRaised()
        {
            RaiseResponse();
            
        }
        protected void RaiseResponse()
        {
            _response.Invoke();
        }
        private void OnEnable()
        {
            if (_event != null)
                Register();
        }
        private void OnDisable()
        {
            if (_event != null)
                _event.RemoveListener(this);
        }
        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }
}
}
