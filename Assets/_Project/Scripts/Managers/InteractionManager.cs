using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class InteractionManager : MonoBehaviour, IStateMachine<InteractionManagerState>
    {
                
        
        [SerializeField] private InteractionManagerState startState;
        [SerializeField] private InteractionManagerState currentState;
        [SerializeField] public MouseScreenRayProvider  interactableRayProvider;
        
        public Dictionary<int, IInteractable> playerInteractables = new Dictionary<int, IInteractable>();
        public Dictionary<int, IInteractable> enemyInteractables = new Dictionary<int, IInteractable>();



        void Start()
        {
            currentState = startState;
            startState.SetManager(this);

        }
 
        void Update()
        {
            currentState.Tick(Time.deltaTime);
        }


        public void CheckForStateChange()
        {
            InteractionManagerState nextState = currentState.ReturnNextState() as InteractionManagerState;
            if (nextState == currentState || !nextState) return;
            nextState.SetManager(this);
            SetState(nextState);
        }

        public void SetState(InteractionManagerState state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }


        public void CardPlayed()
        {
            Debug.Log("Played ");
            // TODO this check is not necessary when all cards have placeable data
            // if (card.cardData.spawnPlaceablesData)
            // {
            //     placeable.Value = Instantiate(card.cardData.spawnPlaceablesData.placeholderPrefab, new Vector3(0, 0.5f), Quaternion.identity);
            //     placeable.Value.SetActive(false);
            // }
            // Destroy(card.gameObject);
            // _handCards.Remove(currentlySelectedCard.Value.GetInstanceID());
            // currentlySelectedCard.Value = null;
            // CalculateAndMove(_handCards.Count);
        }
    }
}
