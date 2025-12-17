using RPG.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace RPG.Scripts.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        [Header("Enemy Settings")] public float health = 100f;
        public float moveSpeed = 3f;

        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new HealthComponent()
                {
                    Value = authoring.health
                });
                AddComponent(entity, new MoveSpeedComponent()
                {
                    Value = authoring.moveSpeed
                });
                AddComponent(entity, new EnemyTagComponent());
            }
        }
    }
}