using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class GameTile : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject highlight;
        public Transform SpawnPoint => spawnPoint;

        public void Highlight()
        {
            highlight.SetActive(true);
        }

        public void Unhighlight()
        {
            highlight.SetActive(false);
        }
    }
}
