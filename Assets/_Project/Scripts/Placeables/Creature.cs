using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class Creature : MonoBehaviour, IInteractable
    {


        public void Select()
        {
            Debug.Log("Select");
        }

        public void Deselect()
        {
            Debug.Log("Deselect");
        }
    }
}
