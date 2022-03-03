using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
 
        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }

        [CreateAssetMenu(
            fileName = "BoolVariable.asset",
            menuName = "Variables/New Variable_"+ "bool",
            order = 5)]
        public sealed class BoolVariable : BaseVariable<bool, BoolEvent>
        {
        }

    
}
