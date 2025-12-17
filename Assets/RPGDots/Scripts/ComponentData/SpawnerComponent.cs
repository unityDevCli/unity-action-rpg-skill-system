using RPGDots.Scripts.Authoring;
using Unity.Entities;
using Unity.Mathematics;

namespace RPGDots.Scripts.ComponentData
{
    public struct SpawnerComponent : IComponentData
    {
        public Entity Prefab;
        public float SpawnInterval;
        public float SpawnTimer;

        public SpawnShape Shape;
        public int WaveSize;
        public float SpawnRadius;
        public Random Randomizer;
    }
}