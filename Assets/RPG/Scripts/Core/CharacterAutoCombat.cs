using System;
using UnityEngine;

namespace RPG.Scripts.Core
{
    [RequireComponent(typeof(Health))]
    public class CharacterAutoCombat : MonoBehaviour
    {
        [Header("Target")] [SerializeField] protected float searchRadius = 12f;
        [SerializeField] protected LayerMask enemyLayer;
        [SerializeField] protected float reTargetInterval = 0.5f;

        [Header("Attack")] [SerializeField] protected float attackRange = 2f;
        [SerializeField] protected float attackDamage = 25f;
        [SerializeField] protected float attackCoolDown = 0.6f;

        private Transform _currentEnemy;
        private float _nextReTargetTime;
        private Collider[] _results;
        private const int MaxColliders = 10;

        private void Awake()
        {
            _results = new Collider[MaxColliders];
        }

        private void Update()
        {
            if (Time.time > _nextReTargetTime)
            {
                _nextReTargetTime = Time.time + reTargetInterval;
                _currentEnemy = FindNearestEnemy();
            }

            if (_currentEnemy == null) return;

            var toEnemy = _currentEnemy.position - transform.position;
            toEnemy.y = 0;

            var dist = toEnemy.magnitude;
            var dir = toEnemy.normalized;
            if (toEnemy.sqrMagnitude > 0.0001f)
            {
                transform.forward = Vector3.Lerp(transform.position, dir, Time.deltaTime * 10f);
            }

            if (dist <= attackRange && Time.time >= _nextReTargetTime)
            {
                _nextReTargetTime = Time.time + _nextReTargetTime;
                var hp = _currentEnemy.GetComponent<Health>();
                if (hp != null) hp.TakeDamage(attackDamage);
            }
        }

        private Transform FindNearestEnemy()
        {
            var numberColliders = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, _results);
            if (numberColliders <= 0) return null;
            Transform best = null;
            var bestSqr = float.PositiveInfinity;
            for (var i = 0; i < numberColliders; i++)
            {
                var t = _results[i].transform;
                var hp = t.GetComponent<Health>();
                if (hp == null || hp.IsDead) continue;
                var sqr = (t.position - transform.position).sqrMagnitude;
                if (sqr < bestSqr)
                {
                    bestSqr = sqr;
                    best = t;
                }
            }

            return best;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.4f, 0.1f, 0.35f);
            Gizmos.DrawWireSphere(transform.position, searchRadius);

            Gizmos.color = new Color(0.2f, 1f, 0.2f, 0.35f);
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}