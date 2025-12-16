using System;
using RPG.Scripts.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace RPG.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn Settings")] [SerializeField]
        protected GameObject meleeEnemyPrefab;

        [SerializeField] protected GameObject rangedEnemyPrefab;

        [SerializeField] protected Transform[] spawnPoints;

        [SerializeField] protected float spawnInterval = 1.5f;
        [SerializeField] protected float maxAlive = 20f;
        [Range(0f, 1f)] [SerializeField] private float rangedChance = 0.35f;

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
            if (targetPlayer == null) return;
            if (spawnPoints is not { Length: > 0 }) return;

            if (Time.time >= _nextSpawnTime)
            {
                _nextSpawnTime = Time.time + spawnInterval;
                if (_aliveCount < maxAlive) SpawnOne();
            }
        }

        private void SpawnOne()
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var spawnRanged = Random.value < rangedChance;
            var prefab = spawnRanged ? rangedEnemyPrefab : meleeEnemyPrefab;
            var obj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            var enemyAI = obj.GetComponent<BaseEnemyAI>();
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