using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }
    
    [CreateAssetMenu(
        fileName = "BoolVariable.asset",
        menuName = "Variables/New Variable_"+ "GameObject",
        order = 5)]
    public sealed class GameObjectVariable: BaseVariable<GameObject, GameObjectEvent>
    {
    }
    
}
