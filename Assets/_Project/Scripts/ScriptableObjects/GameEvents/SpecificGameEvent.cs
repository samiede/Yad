using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    public class SpecificGameEvent<T, TEvent> : GenericGameEvent where TEvent: UnityEvent<T>
    {
        private List<SpecificGameEventListener<T, TEvent>> _specificListeners = new List<SpecificGameEventListener<T, TEvent>>();

        public void AddSpecificListener(SpecificGameEventListener<T, TEvent> listener)
        {
            _specificListeners.Add(listener);
        }

        public void RemoveSpecificListener(SpecificGameEventListener<T, TEvent> listener)
        {
            _specificListeners.Remove(listener);
        }

        public new void RemoveAll()
        {
            _specificListeners.RemoveRange(0, _specificListeners.Count);
            _listeners.RemoveRange(0, _listeners.Count);

        }
        public void Raise(T message)
        {
            for (int i = 0; i < _specificListeners.Count; i++)
            {
                _specificListeners[i].OnSpecificEventRaised(message);
            }
        }

    }
}