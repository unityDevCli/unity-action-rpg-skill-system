using RPGDots.Scripts.ComponentData;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RPGDots.Scripts.Jobs
{
    public partial struct NearestJob : IJobEntity
    {
        [ReadOnly] public NativeArray<Entity> EnemyEntities;
        [ReadOnly] public NativeArray<LocalTransform> EnemyTransforms;

        private void Execute(in LocalTransform playerXf, ref NearestEnemy nearest)
        {
            var position = playerXf.Position;
            var bestEntity = Entity.Null;
            float3 bestPos = default;
            var bestDistSq = float.PositiveInfinity;

            for (var i = 0; i < EnemyTransforms.Length; i++)
            {
                var epos = EnemyTransforms[i].Position;
                var d2 = math.lengthsq(epos - position);
                if (d2 < bestDistSq)
                {
                    bestDistSq = d2;
                    bestEntity = EnemyEntities[i];
                    bestPos = epos;
                }
            }

            nearest.Value = bestEntity;
            nearest.Position = bestPos;
            nearest.Distance = bestDistSq;
        }
    }
}