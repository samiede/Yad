using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Deckbuilder
{
    public class Card : MonoBehaviour
    {
        // [HideInInspector] public int cardId;
        [HideInInspector] public CardData cardData;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI cardDescription;
        public GameObject statsHolder;
        public CardVisualProperties[] properties;
        
        #region Dragging Properties
        private Transform _transform;
        private Vector3 cardPos;
        private Vector3 _mouseStartPos;
        private Vector3 _cardStartPos;
        private Vector3 _cardDashboardPosition;
        private Vector3 _offset;
        [SerializeField] private MeshRenderer _renderer;

        private Vector3 cardSmallSize;
        private Vector3 cardLargeSize;
        [SerializeField] float bigCardYPos = -1.4f;
        [SerializeField] public Camera _cardCamera;
        
        #endregion
        
        [SerializeField] private float castThreshold = 0.3f;
        [SerializeField] private float screenPercentLerp = 0.5f;

        // private GameObject _parent;
        
        // private static bool _dragging;
        public bool _isOverThreshold;

        public Action<Card> OnPlayed;
        
        private void Start()
        {
            _transform = transform;
            cardSmallSize = _transform.localScale;
            cardLargeSize = cardSmallSize * 1.5f;
        }

        public void InitializeWithCardData(CardData cData)
        {
            if (!cData) return;
            
            cardData = cData;
            _renderer.material = cData.cardMaterial;
            
            cardData.cardType.OnSetType(this);
            

            for (int i = 0; i < cardData.properties.Length; i++)
            {
                CardProperties props = cardData.properties[i];
                CardVisualProperties p = GetProperty(props.element);
                if (p == null) continue;
                if (props.element is ElementInt)
                {
                    p.text.SetText(props.intValue.ToString());
                }
                else if (props.element is ElementText)
                {
                    p.text.SetText(props.stringValue);
                    
                } else if (props.element is ElementTexture)
                {
                    p.renderer.material.mainTexture = props.texture;
                } else if (props.element is ElementImage)
                {
                    p.img.sprite = props.sprite;
                }
            }
            
        }

        public CardVisualProperties GetProperty(Element e)
        {
            return properties.FirstOrDefault(t => t.element == e);
        }

        public void SetCamera(Camera camera)
        {
            _cardCamera = camera;
        }

        // TODO refactor to CardManager
        #region Mouse Action
        public void MouseEnterEvent(Boolean dragging)
        {
            if (dragging) return;
            
            _cardDashboardPosition = _transform.localPosition;
            // Debug.Log(_cardDashboardPosition);
            Vector3 position = _transform.localPosition;
            position.y = bigCardYPos;
            position.z -= 0.1f;
            
            _transform.localScale = cardLargeSize;
            _transform.localPosition = position;
            _cardStartPos = position;
            
        }

        public void MouseExitEvent()
        {
            ResetPosition(true);
        }

        public void MouseDragEvent()
        {
            Vector3 mousePosition = _cardCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cardCamera.transform.position.z * -1));
            float dragDistance = Vector3.Distance(mousePosition, _mouseStartPos);
            if (dragDistance > 0f)
            {
                
                mousePosition += _offset;
                _transform.position = mousePosition;
                _isOverThreshold = _cardCamera.WorldToViewportPoint(_transform.position).y > castThreshold;
                
                if (_isOverThreshold)
                {
                    float percent = Mathf.InverseLerp(castThreshold, screenPercentLerp, _cardCamera.WorldToViewportPoint(_transform.position).y);
                    _renderer.material.color = Color.Lerp(cardData.cardMaterial.color, Color.blue, percent);
                }
                else
                {
                    _renderer.material.color = cardData.cardMaterial.color;
                }
            }
        }

        public void OnMouseDownEvent()
        {
            // _dragging = true;
            _mouseStartPos = _cardCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cardCamera.transform.position.z * -1));
            _offset = _transform.position - _mouseStartPos;
        }

        public void OnMouseUpAsButtonEvent(Boolean _dragging)
        {
            
            if (_dragging)
            {
                if (_isOverThreshold)
                {
                    Cast();
                }
                else
                {
                    // Check if mouse is over large card collider
                    bool insideOriginalCollider = IsInsideOriginalCollider(
                    _cardCamera.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x, 
                    Input.mousePosition.y, 
                    -_cardCamera.transform.position.z))
                    );
                    ResetPosition(!insideOriginalCollider);
                    _dragging = false;

                }
                
            }
        }
        

        private bool IsInsideOriginalCollider(Vector3 checkPosition)
        {
            Debug.Log("Check: " + checkPosition);
            // TODO cache local scale
            return checkPosition.x < _cardDashboardPosition.x + cardSmallSize.x / 2
                   && checkPosition.x > _cardDashboardPosition.x - cardSmallSize.x / 2
                   && checkPosition.y < _cardDashboardPosition.y + cardSmallSize.y / 2
                   && checkPosition.y > _cardDashboardPosition.y - cardSmallSize.y / 2;

        }

        private void Cast()
        {
            OnPlayed?.Invoke(this);
            // Destroy(gameObject);
        }

        public void ResetPosition(bool dismissLargeCard)
        {
            if (dismissLargeCard)
            {
                _transform.localPosition = _cardDashboardPosition;
                _transform.localScale = cardSmallSize;
            }
            else
            {
                _transform.localPosition = _cardStartPos;
            }

        }
        
        #endregion

        
    }
}
