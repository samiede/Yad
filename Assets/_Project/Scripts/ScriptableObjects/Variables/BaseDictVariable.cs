using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    public abstract class BaseDictVariable<Tk, Tv, TEvent>: ScriptableObject where TEvent : UnityEvent<Tv>
    {
        protected readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();
        protected readonly List<GenericGameEventListener> _gListeners = new List<GenericGameEventListener>();
        
        [SerializeField] private TEvent _event = default(TEvent);
        [SerializeField] private GenericGameEvent _genericEvent;

        public Dictionary<Tk, Tv> Dict { get; } = new Dictionary<Tk, Tv>();

        public void AddSilent(Tk addKey, Tv addVal)
        {
            Dict.Add(addKey, addVal);
        }

        public void Add(Tk addKey, Tv addVal)
        {
            Dict.Add(addKey, addVal);
            Raise(addVal);
        }

        public Tv Get(Tk val)
        {
            Dict.TryGetValue(val, out var retVal);
            return retVal;
        }
        
        public void Raise(Tv newVal)
        {
            _event.Invoke(newVal);
            if (_genericEvent) _genericEvent.Raise();
        }
        
        public void AddListener(UnityAction<Tv> callback)
        {
            _event.AddListener(callback);
        }
        
        public void RemoveListener(UnityAction<Tv> callback)
        {
            _event.RemoveListener(callback);
        }
        
        public virtual void RemoveAll()
        {
            _listeners.RemoveRange(0, _listeners.Count);
            _gListeners.RemoveRange(0, _gListeners.Count);
            _event.RemoveAllListeners();

        }
    }
}
