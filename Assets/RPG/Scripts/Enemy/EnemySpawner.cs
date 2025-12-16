using System;
using RPG.Scripts.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RPG.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")] [SerializeField]
        protected GameObject enemyPrefab;

        [SerializeField] protected Transform[] spawnPoints;

        [SerializeField] protected float spawnInterval = 1.5f;
        [SerializeField] protected float maxAlive = 20f;

        [Header("Target Player")] [SerializeField]
        protected Transform targetPlayer;

        private float _nextSpawnTime;
        private int _aliveCount;

        private void Start()
        {
            _nextSpawnTime = Time.time + spawnInterval;
        }

        private void Update()
        {
            if (enemyPrefab == null || spawnPoints == null) return;
            if (targetPlayer == null) return;

            if (Time.time >= _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + spawnInterval;
                if (_aliveCount < maxAlive) SpawnOne();
            }
        }

        private void SpawnOne()
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var obj = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            var enemyAI = obj.GetComponent<EnemyAI>();
            if (enemyAI != null) enemyAI.SetTarget(targetPlayer);

            var hp = obj.GetComponent<Health>();
            if (hp != null)
            {
                _aliveCount++;
                hp.Died += OnEnemyDied;
            }
        }

        private void OnEnemyDied(Health healthValue)
        {
            healthValue.Died -= OnEnemyDied;
            _aliveCount = Mathf.Max(0, _aliveCount - 1);
        }
    }
}