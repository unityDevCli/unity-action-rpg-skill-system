using RPGDots.Scripts.ComponentData;
using RPGDots.Scripts.Jobs;
using Unity.Burst;
using Unity.Entities;

namespace RPGDots.Scripts.Systems
{
    public partial struct BuffSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            new BuffTickJob()
            {
                DeltaTime = dt
            }.Schedule();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}