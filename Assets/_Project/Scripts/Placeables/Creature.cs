using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class Creature : MonoBehaviour, IInteractable
    {
        private PlaceableData _placeableData;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void Select()
        {
            Debug.Log("Select");
            if (_placeableData.selectClip) _audioSource.PlayOneShot(_placeableData.selectClip);
            
        }

        public void Deselect()
        {
            Debug.Log("Deselect");
        }

        public void Initialize(PlaceableData data)
        {
            _placeableData = data;
        }

        public void PlaySpawnClip()
        {
            if (_placeableData.spawnClip) _audioSource.PlayOneShot(_placeableData.spawnClip);

        }

        public PlaceableData GetData()
        {
            return _placeableData;
        }
    }
}
