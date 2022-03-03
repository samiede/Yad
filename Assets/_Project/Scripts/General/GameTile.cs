using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class GameTile : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        public Transform SpawnPoint => spawnPoint;
    }
}
