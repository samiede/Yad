using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Deckbuilder
{
    public class TurnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI toggleTurnButton;
        [SerializeField] private RunStats stats;

        [SerializeField] private BoolVariable playerTurn;
        // [SerializeField] private GenericGameEvent toggleTurn;
        public void UpdateUI()
        {
            toggleTurnButton.SetText(stats.turnButtonText);
        }


        public void PressEndTurnButton()
        {
            playerTurn.Value = !playerTurn.Value;
            // stats.playerTurn = !stats.playerTurn;
            // toggleTurn.Raise();
        }
        
    }
}
