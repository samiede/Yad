using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    public class GameManager : MonoBehaviour, IStateMachine<GameState>
    {

        [SerializeField] private BoardManager _generator;
        [SerializeField] private Camera cc;

        [SerializeField] private GameState startState;
        [SerializeField] private GameState currentState;

        [SerializeField] private GameObjectVariable currentHoveredEnemy;

        [SerializeField] Texture2D defaultCursor;
        [SerializeField] Texture2D attackCursor;

        private static Texture2D _defaultCursor;
        private static Texture2D _attackCursor;
        void Start()
        {

            _defaultCursor = defaultCursor;
            _attackCursor = attackCursor;
            
            SetDefaultCursor();
            
            _generator.GenerateMap();
            currentState = startState;
            startState.OnEnter();
        }

        public void ToggleCursor()
        {
            
            Debug.Log("Toggle");
            if (currentHoveredEnemy.Value)
            {
                SetAttackCursor();
            }
            else
            {
                SetDefaultCursor();
            }
        }

        public static void SetCursor(Texture2D cursor, Vector2 offset)
        {

            Cursor.SetCursor(cursor, offset, CursorMode.Auto);
        }

        public static void SetDefaultCursor()
        {
            var offset = new Vector2(0.35f * _defaultCursor.width, 0.25f * _defaultCursor.height); 
            SetCursor(_defaultCursor,  offset);
        }

        public static void SetAttackCursor()
        {
            SetCursor(_attackCursor, Vector2.zero);
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
            
        }
    }
}
