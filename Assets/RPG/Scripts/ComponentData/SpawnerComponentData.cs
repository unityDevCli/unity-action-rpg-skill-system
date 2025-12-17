using RPG.Scripts.Authoring;
using Unity.Entities;
using Unity.Mathematics;

namespace RPG.Scripts.ComponentData
{
    public struct SpawnerComponentData : IComponentData
    {
        public Entity Prefab;
        public float SpawnTimer;
        public float SpawnInterval;
        public float SpawnRadius;
        public int WaveSize;
        public Random Randomizer;
        public SpawnShape Shape;
    }
}