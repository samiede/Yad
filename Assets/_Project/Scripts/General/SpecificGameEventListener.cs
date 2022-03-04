using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    public class SpecificGameEventListener<T, TEvent> : GenericGameEventListener where TEvent : UnityEvent<T>
    {
        [SerializeField] private SpecificGameEvent<T, TEvent> specificGameEvent;
        [SerializeField] public TEvent specificResponse;

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