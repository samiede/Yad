using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Deckbuilder
{
    public class GenericGameEventListener: MonoBehaviour
    {
        
        [SerializeField] protected GenericGameEvent gameEvent;
        [SerializeField] public UnityEvent response;

        private void OnEnable()
        {
            gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveListener(this);
        }

        public void OnEventRaised()
        {
            response.Invoke();
        }
    }
}
