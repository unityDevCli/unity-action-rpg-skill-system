using RPGDots.Scripts.ComponentData;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPGDots.Scripts.Systems
{
    // public partial struct AutoCastSystem : ISystem
    // {
    //     [BurstCompile]
    //     public void OnCreate(ref SystemState state)
    //     {
    //         state.RequireForUpdate<ProjectilePrefabPref>();
    //     }
    //
    //     [BurstCompile]
    //     public void OnUpdate(ref SystemState state)
    //     {
    //         float dt = SystemAPI.Time.DeltaTime;
    //
    //         if (!SystemAPI.TryGetSingleton<ProjectilePrefabPref>(out var projPrefabRef))
    //             return;
    //
    //         var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
    //
    //         foreach (var (entity, xf, nearest, cd) in SystemAPI
    //                      .Query<Entity, RefRO<LocalTransform>, RefRO<NearestEnemy>, RefRW<FireCoolDown>>()
    //                      .WithAll<CharacterTag>())
    //         {
    //             cd.ValueRW.Timer -= dt;
    //             if (cd.ValueRW.Timer > 0f) continue;
    //
    //             if (nearest.ValueRO.Value == Entity.Null) continue;
    //
    //             float3 to = nearest.ValueRO.Position - xf.ValueRO.Position;
    //             to.y = 0f;
    //
    //             if (math.lengthsq(to) < 0.0001f) continue;
    //             float3 dir = math.normalize(to);
    //
    //             var proj = ecb.Instantiate(projPrefabRef.Value);
    //
    //             // Enable projectile (vì prefab baked có Disabled)
    //             ecb.RemoveComponent<Disabled>(proj);
    //
    //             // Spawn trước mặt 1 chút cho đẹp
    //             float3 spawnPos = xf.ValueRO.Position + dir * 0.7f;
    //
    //             ecb.SetComponent(proj,
    //                 LocalTransform.FromPositionRotation(spawnPos, quaternion.LookRotationSafe(dir, math.up())));
    //             ecb.SetComponent(proj, new ProjectileDirection { Value = dir });
    //
    //             // Ghi owner để sau này lọc friendly fire nếu bạn muốn
    //             var pdata = state.EntityManager.GetComponentData<ProjectileData>(projPrefabRef.Value);
    //             pdata.Owner = entity;
    //             ecb.SetComponent(proj, pdata);
    //
    //             cd.ValueRW.Timer = cd.ValueRO.Interval;
    //         }
    //
    //         ecb.Playback(state.EntityManager);
    //         ecb.Dispose();
    //     }
    //
    //     [BurstCompile]
    //     public void OnDestroy(ref SystemState state)
    //     {
    //     }
    // }
}