using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Run Stats")]
    public class RunStats : ScriptableObject
    {
        public string turnButtonText;
        public bool playerTurn = true;

    }
}
