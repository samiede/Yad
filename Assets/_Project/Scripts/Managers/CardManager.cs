    using System;
using System.Collections;
using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
using UnityEngine.Assertions.Must;
    using UnityEngine.Serialization;

    namespace Deckbuilder
{
    public class CardManager : MonoBehaviour, IStateMachine<CardHandlerState>
    {
        [Header("Card Data")]
        [SerializeField] private DeckData playerDeck;

        [SerializeField] private GameObjectVariable currentlySelectedCardObject;
        [SerializeField] private CardVariable currentCastCard;
        
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private int maxHandSize = 10;
        [SerializeField] private int initialHandSize = 10;
        private Dictionary<int, Card> _handCards;
        public Dictionary<int, Card> HandCards => _handCards;
        
        [Header("UI Elements")] 
        [SerializeField] private Camera cardCamera;
        [SerializeField] private Camera uiCamera;
        [SerializeField] private RectTransform cardsDashboard;
        [SerializeField] private float dashboardMargin = 10;
        [SerializeField] private float cardMargin = 0.4f;
        [SerializeField] private Transform cardParent;
        private List<CardPosition> cardPositions;

        [SerializeField] private MouseScreenRayProvider  _cardRayProvider;
        public MouseScreenRayProvider CardRayProvider => _cardRayProvider;
        

        
        
        [SerializeField] private CardHandlerState startState;
        [SerializeField] private CardHandlerState currentState;
        
        
        IEnumerator InitDeck()
        {
            for (int i = 0; i < initialHandSize; i++)
            {
                yield return StartCoroutine(DrawNewCard(0));

            }
        }
        private void Start()
        {
            cardPositions = new List<CardPosition>();
            _handCards = new Dictionary<int, Card>();
            StartCoroutine(InitDeck());
            currentState = startState;
            startState.SetManager(this);
        }
        
        
        public void CheckForStateChange()
        {
            CardHandlerState nextState = currentState.ReturnNextState() as CardHandlerState;
            if (nextState == currentState || !nextState) return;
            nextState.SetManager(this);
            SetState(nextState);
        }

        public void SetState(CardHandlerState state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }

        private void Update()
        {   
            currentState.Tick(Time.deltaTime);
        }
        

        private IEnumerator DrawNewCard(float delay)
        {

            CalculateAndMove(_handCards.Count + 1);

            // yield return null;
            yield return new WaitForSeconds(delay);

            Vector3 spawnPos = new Vector3(cardPositions[cardPositions.Count - 1].x,
                cardPositions[cardPositions.Count - 1].y, cardPositions[cardPositions.Count - 1].z);
            GameObject newCard = Instantiate(cardPrefab, spawnPos, Quaternion.identity, cardParent);
            Card card = newCard.GetComponent<Card>();
            
            card.SetCamera(cardCamera);
            card.InitializeWithCardData(playerDeck.GetNextCardFromDeck());
            
            _handCards.Add(newCard.GetInstanceID(), card);
            card.transform.localScale *= 0.5f;


        }
        

        [ContextMenu("Add")]
        public void AddCard()
        {
            StartCoroutine(DrawNewCard(.4f));

        }
        

        public void OnCardCast(Card card)
        {
            currentCastCard.Value = card;
            Destroy(card.gameObject);
            _handCards.Remove(currentlySelectedCardObject.Value.GetInstanceID());
            currentlySelectedCardObject.Value = null;
            CalculateAndMove(_handCards.Count);
        }



        #region Card Placement in Hand
        
        // TODO make this work with other values
        private void CalcuateCardPositions(int num)
        {
            // TODO Check why this works this way with the margins, a lot of guesswork in this one!
            cardPositions.Clear();
            float cardWidth = cardPrefab.transform.localScale.x * .5f;
            float availableSpace = GetAvailableSpace(cardsDashboard, dashboardMargin);
            availableSpace -= cardWidth * 2;
            float maxSpacing = Mathf.Min((availableSpace - cardWidth) / (num * (cardWidth + cardMargin) / 2),  cardMargin + cardWidth/2);
            Vector3 dashboardCenter = cardsDashboard.position;
            float cardsNumOffset = (num - 1) / 2f;
            for (int i = 0; i < num; i++)
            {
                float xPos = dashboardCenter.x + (i - cardsNumOffset) * maxSpacing;
                cardPositions.Add(new CardPosition(xPos, -2.16f, i * -0.0003f, Quaternion.identity));
            }

        }

        private void CalculateAndMove(int num)
        {
            CalcuateCardPositions(num);
            
            for (int c = 0; c < _handCards.Count; c++)
            {
                _handCards.ElementAt(c).Value.transform.localPosition = new Vector3(cardPositions[c].x, cardPositions[c].y, cardPositions[c].z);
                _handCards.ElementAt(c).Value.transform.localRotation = cardPositions[c].rotation;
            }
        } 
        
        float GetAvailableSpace(RectTransform uiElement, float margin)
        {
            Matrix4x4 rectMatrix = uiElement.transform.localToWorldMatrix;
            float x = uiElement.rect.x;
            x += margin * 2;
            Vector3 left = new Vector3(x, 0f, 0f);
            float xMax = uiElement.rect.xMax;
            xMax -= margin * 2;
            Vector3 right = new Vector3(xMax, 0f, 0f);
            left = rectMatrix.MultiplyPoint(left);
            right = rectMatrix.MultiplyPoint(right);
            return Vector3.Distance(left, right);
        }
        
        private class CardPosition
        {
            public CardPosition(float _x, float _y, float _z, Quaternion _rotation)
            {
                x = _x;
                y = _y;
                z = _z;
                rotation = _rotation;
            }
            public float x;
            public float y;
            public float z;
            public Quaternion rotation;
        }

        #endregion
        
        #region REMEMBER THIS FOR SOME OTHER TIME ON GUI DRAWING STUFF
        // void OnGUI()
        // {
        //     Vector3 point = new Vector3();
        //     Event   currentEvent = Event.current;
        //     Vector2 mousePos = new Vector2();
        //
        //     // Get the mouse position from Event.
        //     // Note that the y position from Event is inverted.
        //     mousePos.x = currentEvent.mousePosition.x;
        //     mousePos.y = cardCamera.pixelHeight - currentEvent.mousePosition.y;
        //
        //     point = uiCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cardCamera.nearClipPlane));
        //
        //     GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        //     GUILayout.Label("Screen pixels: " + cardCamera.pixelWidth + ":" + cardCamera.pixelHeight);
        //     GUILayout.Label("Mouse position: " + mousePos);
        //     GUILayout.Label("World position: " + point.ToString("F3"));
        //     GUILayout.EndArea();
        // }
        #endregion
    
        
    }
}
