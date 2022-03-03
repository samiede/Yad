using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class BasePlaceable : MonoBehaviour
    {
        public PlaceableType pType;
        [HideInInspector] public Faction faction;
        [HideInInspector] public AttackType attackType;
        

        public enum AttackType
        {
            Melee,
            Ranged,
            None
        }
        
        public enum PlaceableType
        {
            Unit,
            Building,
            Spell
        }
        
        public enum PlaceableTarget
        {
            All,
            None,
        }

        public enum Faction
        {
            Player,
            Opponent,
            Neutral
        }
    }
}
