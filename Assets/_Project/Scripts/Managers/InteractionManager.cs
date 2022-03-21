using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class InteractionManager : MonoBehaviour, IStateMachine<InteractionManagerState>
    {
        
        [Header("Card variables")]
        [SerializeField] private CardGameEvent cardPlayed;
        [SerializeField] private CardVariable currentCastCard;

        [Header("Interactables")]
        [SerializeField] private GameObjectVariable placeable;
        [SerializeField] private InteractablesContainer interactables;
        [SerializeField] private GameObjectVariable currentFriendlyInteractable;
        [SerializeField] private GameObjectVariable currentEnemyInteractable;

        [Header("State Machine")]
        [SerializeField] private InteractionManagerState startState;
        [SerializeField] private InteractionManagerState currentState;
        [SerializeField] public MouseScreenRayProvider  interactableRayProvider;

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
                
                interactables.AddToFriendly(go.GetInstanceID(), interactable);
                Destroy(placeable.Value);
                placeable.Value = null;
            }

        }

        public void UnitDied(GameObject unit)
        {
            
            interactables.RemoveEntity(unit.GetInstanceID());
            Destroy(unit);
        }

        public void FriendlyAttackSelection(GameObject target)
        {
            interactables.GetFriendly(currentFriendlyInteractable.Value.GetInstanceID()).Attack(interactables.Get(target.GetInstanceID()));
        }

        public void ResetInteractables()
        {
            foreach (var interactable in interactables.PlayerInteractables.Dict)
            {
                interactable.Value.StartTurnReset();
            }
        }
    }
}
