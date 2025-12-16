using RPGDots.Scripts.ComponentData;
using RPGDots.Scripts.Jobs;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace RPGDots.Scripts.Systems
{
    public partial struct FindNearestEnemySystem : ISystem
    {
        private EntityQuery _enemyQuery;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _enemyQuery = state.GetEntityQuery(ComponentType.ReadOnly<EnemyTag>(),
                ComponentType.ReadOnly<LocalTransform>());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var enemyCount = _enemyQuery.CalculateEntityCount();
            if (enemyCount == 0) return;
            var enemyEntities = _enemyQuery.ToEntityArray(state.WorldUpdateAllocator);
            var enemyTransforms = _enemyQuery.ToComponentDataArray<LocalTransform>(state.WorldUpdateAllocator);

            state.Dependency = new NearestJob()
            {
                EnemyEntities = enemyEntities,
                EnemyTransforms = enemyTransforms
            }.ScheduleParallel(state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}