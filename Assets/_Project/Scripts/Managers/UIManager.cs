using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas interactablesCanvas;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private HealthBarCreator healthBarCreator;

        private void Start()
        {
            healthBarCreator.Initialize(interactablesCanvas, mainCamera);
        }
    }
}
