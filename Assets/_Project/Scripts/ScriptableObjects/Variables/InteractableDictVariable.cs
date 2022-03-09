using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Deckbuilder
{
    [System.Serializable]
    public class InteractableEvent: UnityEvent<IInteractable> {};
    
    [CreateAssetMenu(
        fileName = "InteractableDictVariable.asset",
        menuName = "Variables/New Variable_"+ "InteractableDict",
        order = 5)]
    public class InteractableDictVariable : BaseDictVariable<int, IInteractable, InteractableEvent>
    {
    }
}
