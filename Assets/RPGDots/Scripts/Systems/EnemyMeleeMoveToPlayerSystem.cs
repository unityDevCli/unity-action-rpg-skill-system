using RPGDots.Scripts.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPGDots.Scripts.Systems
{
    public partial struct EnemyMeleeMoveToPlayerSystem : ISystem
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
            foreach (var (xf, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveSpeedComponent>>()
                         .WithAll<EnemyTag, EnemyMeleeTag>())
            {
                var pos = xf.ValueRO.Position;
                var to = playerPos - pos;
                to.y = 0;
                
                var lenSq = math.lengthsq(to);
                if(lenSq < 0.0001f) continue;
                var dir = math.normalize(to);
                pos += dir * (speed.ValueRO.Value * dt);

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