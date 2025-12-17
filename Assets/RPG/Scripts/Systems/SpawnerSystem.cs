using RPG.Scripts.ComponentData;
using RPG.Scripts.Job;
using Unity.Burst;
using Unity.Entities;

namespace RPG.Scripts.Systems
{
    public partial struct SpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SpawnerComponentData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            var dt = SystemAPI.Time.DeltaTime;
            new SpawnJobEntity()
            {
                Ecb = ecb,
                DeltaTime = dt
            }.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}