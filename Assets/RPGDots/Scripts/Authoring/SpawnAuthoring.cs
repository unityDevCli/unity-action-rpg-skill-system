using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

namespace RPGDots.Scripts.Authoring
{
    public enum SpawnShape
    {
        Circle,
        Line,
        Square
    }

    public class SpawnAuthoring : MonoBehaviour
    {
        [Header("Spawn Setting")] public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public SpawnShape shape = SpawnShape.Circle;
        public float spawnRadius = 5f;
        public int waveSize = 5;

        private class SpawnAuthoringBaker : Baker<SpawnAuthoring>
        {
            public override void Bake(SpawnAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new SpawnerComponent()
                {
                    Prefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                    SpawnTimer = authoring.spawnTimer,
                    SpawnInterval = authoring.spawnInterval,
                    Shape = authoring.shape,
                    SpawnRadius = authoring.spawnRadius,
                    WaveSize = authoring.waveSize,
                    Randomizer = Random.CreateFromIndex((uint)authoring.GetInstanceID())
                });
            }
        }
    }
}