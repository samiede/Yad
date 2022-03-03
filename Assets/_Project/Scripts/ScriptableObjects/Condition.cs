using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{

    public abstract class Condition: ScriptableObject
    {
        public abstract bool Check();
    }
}
