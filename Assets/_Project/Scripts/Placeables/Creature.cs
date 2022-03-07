using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class Creature : MonoBehaviour, IInteractable
    {
        private PlaceableData placeableData;

        public void Select()
        {
            Debug.Log("Select");
        }

        public void Deselect()
        {
            Debug.Log("Deselect");
        }

        public void Initialize(PlaceableData data)
        {
            placeableData = data;
        }
    }
}
