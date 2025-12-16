using RPGDots.Scripts.ComponentData;
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
            foreach (var (buffer, entity) in SystemAPI.Query<DynamicBuffer<BuffElement>>().WithEntityAccess())
            {
                for (var i = buffer.Length - 1; i >= 0; i--)
                {
                    var buff = buffer[i];
                    buff.Duration -= dt;
                    if (buff.Duration <= 0) buffer.RemoveAt(i);
                    // else buffer[i] = buff;
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}