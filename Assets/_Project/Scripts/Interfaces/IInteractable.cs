using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder
{
    public interface IInteractable
    {
        public PlaceableData PlaceableData { get; }

        public int RemainingMovement { get; }
        
        public void Select();
        public void Deselect();

        public void Initialize(PlaceableData data);

        public void PlaySpawnClip();

        public void Move(int distance);

        public void StartTurnReset();

        public bool TakeDamage(float damage);

        public void Attack(IInteractable target);

        public void Die();


    }
}