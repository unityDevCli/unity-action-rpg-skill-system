using System;
using RPG.Scripts.Core;
using UnityEngine;

namespace RPG.Scripts.Enemy
{
    [RequireComponent(typeof(Health))]
    public class EnemyAI : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] protected float moveSpeed = 3f;
        [SerializeField] protected float stoppingDistance = 1f;

        [Header("Attack")] [SerializeField] protected float attackRange = 1.6f;
        [SerializeField] protected float attackDamage = 10f;
        [SerializeField] protected float attackCoolDown = 1f;

        [Header("Target")] [SerializeField] protected Transform targetPlayer;

        private float _nextAttackTime;

        public void SetTarget(Transform player)
        {
            targetPlayer = player;
        }

        private void Update()
        {
            if (targetPlayer == null) return;
            var toPlayer = targetPlayer.position - transform.position;
            toPlayer.y = 0;

            var dist = toPlayer.magnitude;
            if (dist > stoppingDistance)
            {
                var dir = toPlayer.normalized;
                transform.position += dir * (moveSpeed * Time.deltaTime);

                if (dir.sqrMagnitude > 0.0001f)
                {
                    transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);
                }

                if (dist <= attackRange && Time.time >= _nextAttackTime)
                {
                    _nextAttackTime = Time.time + attackCoolDown;
                    var hp = targetPlayer.GetComponent<Health>();
                    if (hp != null)
                    {
                        hp.TakeDamage(attackDamage);
                    }
                }
            }
        }
    }
}