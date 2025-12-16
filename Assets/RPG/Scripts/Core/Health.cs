using System;
using UnityEngine;

namespace RPG.Scripts.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHp = 100;
        [field: SerializeField] public float CurrentHp { get; private set; }
        public bool IsDead => CurrentHp <= 0f;
        public event Action<Health> Died;

        private void Awake()
        {
            CurrentHp = maxHp;
        }

        public void TakeDamage(float damage)
        {
            if(IsDead) return;
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                CurrentHp = 0f;
                Died?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}