using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    
    public abstract class GameEventBase<T> : GameEventBase, IGameEvent<T>
    {
        private readonly List<IGameEventListener<T>> _typedListeners = new List<IGameEventListener<T>>();
        
        [SerializeField]
        protected T _debugValue = default(T);

        public void Raise(T value)
        {

            for (int i = _typedListeners.Count - 1; i >= 0; i--)
                _typedListeners[i].OnEventRaised(value);

            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();
            
        }
        public void AddListener(IGameEventListener<T> listener)
        {
            if (!_typedListeners.Contains(listener))
                _typedListeners.Add(listener);
        }
        public void RemoveListener(IGameEventListener<T> listener)
        {
            if (_typedListeners.Contains(listener))
                _typedListeners.Remove(listener);
        }

        public override string ToString()
        {
            return "GameEventBase<" + typeof(T) + ">";
        }
    }
    public class GameEventBase: ScriptableObject, IGameEvent, IGenericGameEvent
    {
        
        protected readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();
        protected readonly List<GenericGameEventListener> _gListeners = new List<GenericGameEventListener>();

        
        public virtual void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
            for (int i = _gListeners.Count - 1; i >= 0; i--)
            {
                _gListeners[i].OnEventRaised();
            }
        }
        
        public void AddListener(IGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void AddListener(GenericGameEventListener listener)
        {
            if (!_gListeners.Contains(listener))
                _gListeners.Add(listener);
        }

        public void RemoveListener(IGameEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }

        public void RemoveListener(GenericGameEventListener listener)
        {
            if (_gListeners.Contains(listener))
                _gListeners.Remove(listener);
        }

        public virtual void RemoveAll()
        {
            _listeners.RemoveRange(0, _listeners.Count);
            _gListeners.RemoveRange(0, _gListeners.Count);
            
        }
    }
}