using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder {
    
    [CreateAssetMenu(fileName = "NewCard", menuName = "Deckbuilder/Card Data")]
    public class CardData : ScriptableObject
    {
        [Header("Visuals")]
        public CardType cardType;
        public CardProperties[] properties;
        [SerializeField] public Material cardMaterial;


        [Header("Description here, paste above")]
        [SerializeField, TextArea(15, 20)] public string description = "";

        [Header("Placeables")] 
        [SerializeField] public PlaceableData spawnPlaceablesData; // Cards may spawn multiple placeables
        // [SerializeField] private PlaceableData[] spawnPlaceablesData; // Cards may spawn multiple placeables
        // [SerializeField] private PlaceableData[] sideEffectsPlaceablesData; // Cards can have spawn effects


    }


}

