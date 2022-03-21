using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deckbuilder
{
    public abstract class Unit : BasePlaceable, IInteractable
    {
        
        // [Header("Projectile for Ranged")]
        // public GameObject projectilePrefab;
        // public Transform projectileSpawnPoint;

        [SerializeField] private GenericGameEvent moved;
        [SerializeField] private SpecificGameEvent<GameObject, GameObjectEvent> died;
        [SerializeField] private GenericGameEvent deselected;

        public float currentHP;
        private PlaceableData _placeableData;
        private AudioSource _audioSource;
        private Animator _animator;
        public int RemainingMovement { get; private set; }
        public PlaceableData PlaceableData
        {
            get => _placeableData;
        }

        private IInteractable currentTarget;

        
        #region Lifecycle Functions
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        public virtual void Select()
        {
            Debug.Log("Select");
            if (_placeableData.selectClip) _audioSource.PlayOneShot(_placeableData.selectClip);
            
        }

        public virtual void Deselect()
        {
            deselected.Raise();
            Debug.Log("Deselect");
        }

        public virtual void Initialize(PlaceableData data)
        {
            _placeableData = data;
            RemainingMovement = PlaceableData.moveRange;
            currentHP = PlaceableData.hitPoints;
        }

        public virtual void PlaySpawnClip()
        {
            if (_placeableData.spawnClip) _audioSource.PlayOneShot(_placeableData.spawnClip);

        }

        public virtual void StartTurnReset()
        {
            ResetMove();
        }

        public virtual void Move(int distance)
        {
            RemainingMovement -= distance;
            moved.Raise();
        }

        public virtual void ResetMove()
        {
            RemainingMovement = PlaceableData.moveRange;
        }

        public virtual bool TakeDamage(float damage)
        {
            Debug.Log(this.name + "Taking Damage! " + damage);
            currentHP = Mathf.Max(0, currentHP - damage);
            
            if (currentHP > 0) return false;
            StartDeath();
            return true;

        }

        public void Attack(IInteractable target)
        {
            currentTarget = target;
            _animator.SetTrigger("Attack");
            Debug.Log("Attack " + target.PlaceableData.name);
        }

        public void SendDamage()
        {
            currentTarget.TakeDamage(_placeableData.attackDamage);
            currentTarget = null;

        }
        


        [ContextMenu("Die")]
        void StartDeath()
        {
            Deselect();
            PlayDeathAudio();
            _animator.SetTrigger("Die");
        }

            
        public void Die()
        {
            died.Raise(gameObject);
        }

        private void PlayDeathAudio()
        {
            if (_placeableData.dieClip)
            {
                _audioSource.PlayOneShot(_placeableData.dieClip);
            }
        }
        
        

        #endregion
    }
}
