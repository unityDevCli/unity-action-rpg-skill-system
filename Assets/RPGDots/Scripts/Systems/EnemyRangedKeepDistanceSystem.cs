using RPGDots.Scripts.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPGDots.Scripts.Systems
{
    public partial struct EnemyRangedKeepDistanceSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (!SystemAPI.TryGetSingletonEntity<CharacterTag>(out var playerEntity)) return;
            var playerPos = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position;
            var dt = SystemAPI.Time.DeltaTime;
            foreach (var (xf, speed, desired) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<MoveSpeedComponent>, RefRO<DesireRange>>()
                         .WithAll<EnemyTag, EnemyRangedTag>())
            {
                var pos = xf.ValueRO.Position;
                var to = playerPos - pos;
                to.y = 0;
                var dist = math.length(to);
                if (dist < 0.0001f) continue;
                var dir = to / dist;
                var min = desired.ValueRO.Value - desired.ValueRO.Tolerance;
                var max = desired.ValueRO.Value + desired.ValueRO.Tolerance;

                if (dist > max)
                {
                    pos += dir * (speed.ValueRO.Value * dt);
                }
                else if (dist < min)
                {
                    pos -= dir * (speed.ValueRO.Value * dt);
                }

                xf.ValueRW.Position = pos;
                xf.ValueRW.Rotation = quaternion.LookRotationSafe(dir, math.up());
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}