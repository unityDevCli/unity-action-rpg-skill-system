using RPGDots.Scripts.ComponentData;
using Unity.Burst;
using Unity.Entities;

namespace RPGDots.Scripts.Systems
{
    // public partial struct SkillExecutionSystem : ISystem
    // {
    //     [BurstCompile]
    //     public void OnCreate(ref SystemState state)
    //     {
    //     }
    //
    //     [BurstCompile]
    //     public void OnUpdate(ref SystemState state)
    //     {
    //         foreach (var (request, entity) in SystemAPI.Query<RefRO<SkillCastRequest>>().WithEntityAccess())
    //         {
    //             var target = FindTarget(ref state);
    //             var health = SystemAPI.GetComponentRW<HealthComponent>(target);
    //             health.ValueRW.Value -= request.ValueRO.Damage;
    //             state.EntityManager.DestroyEntity(entity);
    //         }
    //     }
    //
    //     [BurstCompile]
    //     public void OnDestroy(ref SystemState state)
    //     {
    //     }
    //
    //     private Entity FindTarget(ref SystemState state)
    //     {
    //         return SystemAPI.QueryBuilder().WithAll<EnemyTag>().Build().GetSingletonEntity();
    //     }
    // }
}