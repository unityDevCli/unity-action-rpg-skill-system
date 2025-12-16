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
        private EntityQuery _playerQuery;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _enemyQuery = state.GetEntityQuery(ComponentType.ReadOnly<EnemyTag>(),
                ComponentType.ReadOnly<LocalTransform>());

            _playerQuery = state.GetEntityQuery(ComponentType.ReadOnly<CharacterTag>(),
                ComponentType.ReadOnly<LocalTransform>(),
                ComponentType.ReadWrite<NearestEnemy>());
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var enemyCount = _enemyQuery.CalculateEntityCount();
            if (enemyCount == 0) return;
            var enemyEntities = _enemyQuery.ToEntityArray(state.WorldUpdateAllocator);
            var enemyTransforms = _enemyQuery.ToComponentDataArray<LocalTransform>(state.WorldUpdateAllocator);

            var nearestJob = new NearestJob()
            {
                EnemyEntities = enemyEntities,
                EnemyTransforms = enemyTransforms
            };
            state.Dependency = nearestJob.ScheduleParallel(_playerQuery, state.Dependency);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }
    }
}