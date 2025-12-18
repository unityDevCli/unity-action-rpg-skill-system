using RPG.Scripts.ComponentData;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPG.Scripts.Systems
{
    [UpdateInGroup(typeof(TransformSystemGroup))]
    public partial struct PlayerMovement : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerInputData>();
            state.RequireForUpdate<MoveSpeedComponent>();
            state.RequireForUpdate<LocalTransform>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;
            foreach (var (transform, inputData, speed) in SystemAPI
                         .Query<RefRW<LocalTransform>, RefRO<PlayerInputData>, RefRO<MoveSpeedComponent>>())
            {
                var moveDir = new float3(inputData.ValueRO.MoveInput.x, 0, inputData.ValueRO.MoveInput.y);
                if (math.lengthsq(moveDir) > float.Epsilon)
                {
                    transform.ValueRW.Position += moveDir * speed.ValueRO.Value;
                    transform.ValueRW.Rotation = quaternion.LookRotation(moveDir, math.up());
                }
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}