using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(fileName = "NewPlaceable", menuName = "Deckbuilder/Placeable Data")]

    public class PlaceableData : ScriptableObject
    {
        [Header("Common")] 
        public BasePlaceable.PlaceableType Type;
        public GameObject placeholderPrefab;
        public GameObject prefab;

        [Header("Buildings and Units")] 
        public BasePlaceable.AttackType attackType = BasePlaceable.AttackType.Melee;
        public BasePlaceable.PlaceableTarget targetType = BasePlaceable.PlaceableTarget.All;
        public float attackDamage = 1f;
        public float attackRange = 1f;
        public float hitPoints = 10f;
        public AudioClip spawnClip, attackClip, dieClip;

        [Header("Units")] 
        public float moveRange = 2f;

        [Header("Spells and Limited Effects")] 
        public int lifetime = 1;

        [Header("Spells")] 
        public float damage;
        public float percentDamageReductionPerRound = 1f;
        public Vector2Int areaOfEffect = new Vector2Int(1, 1);



    }
}