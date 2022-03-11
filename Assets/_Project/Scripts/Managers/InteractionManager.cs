using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class InteractionManager : MonoBehaviour, IStateMachine<InteractionManagerState>
    {
                
        [SerializeField] private GameObjectVariable placeable;
        [SerializeField] private CardGameEvent cardPlayed;
        [SerializeField] private CardVariable currentCastCard;

        [SerializeField] private InteractableDictVariable playerInteractables;
        [SerializeField] private InteractableDictVariable enemyInteractables;
        [SerializeField] private InteractableDictVariable allInteractables;

        [SerializeField] private InteractionManagerState startState;
        [SerializeField] private InteractionManagerState currentState;
        [SerializeField] public MouseScreenRayProvider  interactableRayProvider;

        // public Dictionary<int, IInteractable> playerInteractables = new Dictionary<int, IInteractable>();
        // public Dictionary<int, IInteractable> enemyInteractables = new Dictionary<int, IInteractable>();



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
            Debug.Log("Played");
            if (card.cardData.spawnPlaceablesData)
            {
                GameObject go = Instantiate(card.cardData.spawnPlaceablesData.prefab, placeable.Value.transform.position, Quaternion.identity);
                IInteractable interactable = go.GetComponent<IInteractable>();
                interactable.Initialize(card.cardData.spawnPlaceablesData);
                interactable.PlaySpawnClip();
                
                playerInteractables.Add(go.GetInstanceID(), interactable);
                allInteractables.Add(go.GetInstanceID(), interactable);
                
                Destroy(placeable.Value);
                
                placeable.Value = null;
                // TODO Does this have to happen in card manager? Would mean another event
                currentCastCard.Value = null;
            }

        }

        public void ResetInteractables()
        {
            foreach (var interactable in playerInteractables.Dict)
            {
                interactable.Value.StartTurnReset();
            }
        }
    }
}
