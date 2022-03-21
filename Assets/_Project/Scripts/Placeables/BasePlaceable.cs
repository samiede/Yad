using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class BasePlaceable : MonoBehaviour
    {
        public PlaceableType pType;
        public Faction faction;
        public AttackType attackType;
        

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
