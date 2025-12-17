using RPG.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace RPG.Scripts.Authoring
{
    public enum SpawnShape
    {
        Circle,
        Line,
        Square
    }

    public class SpawnerAuthoring : MonoBehaviour
    {
        [Header("Prefab")] public GameObject enemyPrefab;
        [Header("Spawn Settings")] public float spawnRadius = 5f;
        public SpawnShape shape = SpawnShape.Circle;
        public float spawnTimer = 1f;
        public float spawnInterval = 1.5f;
        public int waveSize = 5;

        private class SpawnerAuthoringBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SpawnerComponentData()
                {
                    Prefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                    SpawnInterval = authoring.spawnInterval,
                    SpawnRadius = authoring.spawnRadius,
                    WaveSize = authoring.waveSize,
                    SpawnTimer = authoring.spawnTimer,
                    Shape = authoring.shape,
                    Randomizer = Random.CreateFromIndex((uint)authoring.GetInstanceID())
                });
            }
        }
    }
}