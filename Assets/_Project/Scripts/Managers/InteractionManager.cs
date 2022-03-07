using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class InteractionManager : MonoBehaviour, IStateMachine<InteractionManagerState>
    {
                
        [SerializeField] private GameObjectVariable placeable;
        [SerializeField] private CardGameEvent cardPlayed;
        
        [SerializeField] private InteractionManagerState startState;
        [SerializeField] private InteractionManagerState currentState;
        [SerializeField] public MouseScreenRayProvider  interactableRayProvider;

        public Dictionary<int, IInteractable> playerInteractables = new Dictionary<int, IInteractable>();
        public Dictionary<int, IInteractable> enemyInteractables = new Dictionary<int, IInteractable>();

        private Card currentCastCard;


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

        public void CardCast(Card card)
        {
            currentCastCard = card;
            if (card.cardData.spawnPlaceablesData)
            {
                placeable.Value = Instantiate(card.cardData.spawnPlaceablesData.placeholderPrefab, new Vector3(0, 0.5f), Quaternion.identity);
                placeable.Value.SetActive(false);
            }
            else
            {
                cardPlayed.Raise(card);
            }

        }


        public void CardPlayed(Card card)
        {
            // TODO very ugly, maybe save the current card in GO as well!?!?
            if (!card) card = currentCastCard;
            if (card.cardData.spawnPlaceablesData)
            {
                GameObject go = Instantiate(card.cardData.spawnPlaceablesData.prefab, placeable.Value.transform.position, Quaternion.identity);
                IInteractable interactable = go.GetComponent<IInteractable>();
                interactable.Initialize(card.cardData.spawnPlaceablesData);
                playerInteractables.Add(go.GetInstanceID(), interactable);
                Destroy(placeable.Value);
                
                placeable.Value = null;
                currentCastCard = null;
            }

        }
    }
}
