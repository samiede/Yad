using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    [CreateAssetMenu(menuName = "Variables/InteractablesContainer")]
    public class InteractablesContainer : ScriptableObject
    {
        [SerializeField] private InteractableDictVariable allInteractables;
        [SerializeField] private InteractableDictVariable playerInteractables;
        [SerializeField] private InteractableDictVariable enemyInteractables;

        public InteractableDictVariable AllInteractables => allInteractables;

        public InteractableDictVariable PlayerInteractables => playerInteractables;

        public InteractableDictVariable EnemyInteractables => enemyInteractables;


        public void AddToFriendly(int key, IInteractable value)
        {
            allInteractables.Add(key, value);
            playerInteractables.Add(key, value);
        }

        public void AddToEnemy(int key, IInteractable value)
        {
            allInteractables.Add(key, value);
            enemyInteractables.Add(key, value);
        }

        public bool IsEnemy(int key)
        {
            return enemyInteractables.Dict.ContainsKey(key);
        }

        public IInteractable Get(int key)
        {
            return allInteractables.Get(key);
        }

        public IInteractable GetEnemy(int key)
        {
            return enemyInteractables.Get(key);
        }

        public bool IsFriendly(int key)
        {
            return playerInteractables.Dict.ContainsKey(key);
        }

        public IInteractable GetFriendly(int key)
        {
            return playerInteractables.Get(key);
        }

        public void RemoveFriendly(int key)
        {
            if (playerInteractables.Dict.ContainsKey(key))
            {
                playerInteractables.Dict.Remove(key);
                allInteractables.Dict.Remove(key);
            }
        }       
        
        public void RemoveEnemy(int key)
        {
            if (enemyInteractables.Dict.ContainsKey(key))
            {
                enemyInteractables.Dict.Remove(key);
                allInteractables.Dict.Remove(key);
            }
        }

        public void RemoveEntity(int key)
        {
            if (enemyInteractables.Dict.ContainsKey(key)) enemyInteractables.Dict.Remove(key);
            if (playerInteractables.Dict.ContainsKey(key)) playerInteractables.Dict.Remove(key);
            if (allInteractables.Dict.ContainsKey(key)) allInteractables.Dict.Remove(key);
        }
        
        
    }
}
