using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class SpecificGameEvent<T> : GenericGameEvent
    {
        private List<SpecificGameEventListener<T>> _specificListeners = new List<SpecificGameEventListener<T>>();

        public void AddSpecificListener(SpecificGameEventListener<T> listener)
        {
            _specificListeners.Add(listener);
        }

        public void RemoveSpecificListener(SpecificGameEventListener<T> listener)
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