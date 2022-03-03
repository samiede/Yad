using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Deckbuilder
{
    public class CardVisuals : MonoBehaviour
    {
        [HideInInspector] public CardData cardData;
        [SerializeField] private TextMeshPro cardName;
        [SerializeField] private TextMeshPro cardDescription;
        public GameObject statsHolder;
        public CardVisualProperties[] properties;


        private void Start()
        {
            throw new NotImplementedException();
        }

        public void InitializeWithCardData(CardData cData)
        {
            if (!cData) return;
            
            cardData = cData;
            // _renderer.material = cData.cardMaterial;
            
            // cardData.cardType.OnSetType(this);
            

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


    }
}
