using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    [CreateAssetMenu(fileName = "New Generic Game Event", menuName = "Events/Generic Game Event")]
    public class GenericGameEvent : ScriptableObject, IGenericGameEvent
    {
        protected List<GenericGameEventListener> _listeners = new List<GenericGameEventListener>();
        
        public void AddListener(GenericGameEventListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(GenericGameEventListener listener)
        {
            _listeners.Remove(listener);
        }

        public void RemoveAll()
        {
            _listeners.RemoveRange(0, _listeners.Count);

        }

        public void Raise()
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].OnEventRaised();
            }
        }

    }
}
