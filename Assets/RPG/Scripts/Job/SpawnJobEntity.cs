using RPG.Scripts.Authoring;
using RPG.Scripts.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPG.Scripts.Job
{
    public partial struct SpawnJobEntity : IJobEntity
    {
        public EntityCommandBuffer Ecb;
        public float DeltaTime;

        private void Execute(Entity entity, ref SpawnerComponentData spawner)
        {
            spawner.SpawnTimer -= DeltaTime;
            if (spawner.SpawnTimer <= 0f)
            {
                spawner.SpawnTimer = spawner.SpawnInterval;
                var centerPos = float3.zero;
                for (var i = 0; i < spawner.WaveSize; i++)
                {
                    var enemy = Ecb.Instantiate(spawner.Prefab);

                    var spawnPosition = float3.zero;
                    switch (spawner.Shape)
                    {
                        case SpawnShape.Circle:
                            spawnPosition = GetCirclePosition(i, spawner.WaveSize, spawner.SpawnRadius, centerPos);
                            break;
                        case SpawnShape.Line:
                            spawnPosition = GetLinePosition(i, spawner.WaveSize, spawner.SpawnRadius, centerPos);
                            break;
                        case SpawnShape.Square:
                            spawnPosition = GetSquarePosition(i, spawner.WaveSize, spawner.SpawnRadius, centerPos);
                            break;
                    }

                    Ecb.SetComponent(enemy, LocalTransform.FromPosition(spawnPosition));
                }
            }
        }

        private float3 GetCirclePosition(int index, int total, float radius, float3 center)
        {
            var angle = (2 * math.PI * index) / total;
            var x = center.x + math.cos(angle) * radius;
            var z = center.z + math.sin(angle) * radius;
            return new float3(x, 0, z);
        }

        private float3 GetLinePosition(int index, int total, float spacing, float3 center)
        {
            var startX = center.x - (total * spacing) / 2f;
            var x = startX + (index * spacing);
            return new float3(x, 0, center.z + 5f);
        }

        private float3 GetSquarePosition(int index, int total, float size, float3 center)
        {
            var sideCount = math.max(1, total / 4);
            var currentSide = (index / sideCount) % 4;
            var indexOnSide = index % sideCount;
            var step = size / sideCount;
            var offset = indexOnSide * step - (size / 2f);
            float x = 0, z = 0;
            switch (currentSide)
            {
                case 0: // Top
                    x = center.x + offset;
                    z = center.z + size / 2f;
                    break;
                case 1: //Right
                    x = center.x + size / 2f;
                    z = center.z - offset;
                    break;
                case 2: //Bottom
                    x = center.x - offset;
                    z = center.z - size / 2f;
                    break;
                case 3: //Left
                    x = center.x - size / 2f;
                    z = center.z + offset;
                    break;
            }

            return new float3(x, 0, z);
        }
    }
}