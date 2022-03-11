using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public class Unit : BasePlaceable, IInteractable
    {
        
        // [Header("Projectile for Ranged")]
        // public GameObject projectilePrefab;
        // public Transform projectileSpawnPoint;

        [SerializeField] private GenericGameEvent moved;
        
        private PlaceableData _placeableData;
        public PlaceableData PlaceableData
        {
            get => _placeableData;
        }

        public int RemainingMovement { get; private set; }


        private AudioSource _audioSource;


        
        #region Lifecycle Functions
        
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
            RemainingMovement = PlaceableData.moveRange;
        }

        public void PlaySpawnClip()
        {
            if (_placeableData.spawnClip) _audioSource.PlayOneShot(_placeableData.spawnClip);

        }

        public void StartTurnReset()
        {
            ResetMove();
        }

        public void Move(int distance)
        {
            RemainingMovement -= distance;
            moved.Raise();
        }

        public void ResetMove()
        {
            RemainingMovement = PlaceableData.moveRange;
        }

        public PlaceableData GetData()
        {
            return _placeableData;
        }
        #endregion
    }
}
