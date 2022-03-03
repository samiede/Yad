using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    public class GameManager : MonoBehaviour, IStateMachine<GameState>
    {

        [SerializeField] private MapGenerator _generator;
        [SerializeField] private Camera cc;

        [SerializeField] private GameState startState;
        [SerializeField] private GameState currentState;
        void Start()
        {
            _generator.GenerateMap();
            currentState = startState;
            startState.OnEnter();
        }

        public void CheckForStateChange()
        {
            GameState nextState = currentState.ReturnNextState() as GameState;
            if (nextState == currentState) return;
            SetState(nextState);
        }

        public void SetState(GameState state)
        {
            currentState.OnExit();
            currentState = state;
            currentState.OnEnter();
        }

        private void Update()
        {   
            currentState.Tick(Time.deltaTime);
            
            // Vector3 start = cc.ViewportToWorldPoint(new Vector3(0, threshold, 4.24f));
            // Vector3 end = cc.ViewportToWorldPoint(new Vector3(1, threshold, 4.24f));
            // Debug.DrawLine(start, end);
        }
    }
}
