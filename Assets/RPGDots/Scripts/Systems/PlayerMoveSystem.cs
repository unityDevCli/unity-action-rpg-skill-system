using RPGDots.Scripts.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPGDots.Scripts.Systems
{
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            foreach (var (xf, input, speed) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<PlayerInput>, RefRO<MoveSpeedComponent>>()
                         .WithAll<CharacterTag>())
            {
                var move = input.ValueRO.Move;
                var dir = new float3(move.x, 0, move.y);
                var lenSq = math.lengthsq(dir);
                if (lenSq > 1f) dir = math.normalize(dir);
                xf.ValueRW.Position += dir * (speed.ValueRO.Value * dt);
                if (math.lengthsq(dir) > 0.0001f) xf.ValueRW.Rotation = quaternion.LookRotationSafe(dir, math.up());
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}