using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    public class SpecificGameEventListener<T> : GenericGameEventListener
    {
        [SerializeField] private SpecificGameEvent<T> specificGameEvent;
        [SerializeField] public UnityEvent<T> specificResponse;

        public void OnSpecificEventRaised(T message)
        {
            // Debug.Log(specificResponse.GetType());
            // Debug.Log(message.GetType());
            specificResponse?.Invoke(message);
        }
        
        private void OnEnable()
        {
            if (specificGameEvent) specificGameEvent.AddSpecificListener(this);
            if (gameEvent) gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            if (specificGameEvent) specificGameEvent.RemoveSpecificListener(this);
            if (gameEvent) gameEvent.RemoveListener(this);
        }
    }
}