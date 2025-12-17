using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPGDots.Scripts.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public enum EnemyType
        {
            Melee,
            Ranged
        }

        public EnemyType type = EnemyType.Melee;
        public float moveSpeed = 3.5f;

        [Header("Set up for ranged only")] public float desireRange = 8f;
        public float rangeTolerance = 0.7f;

        public float fireInterval = 0.9f;

        public float hp = 100;

        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                // if (authoring.type == EnemyType.Melee)
                // {
                //     AddComponent(entity, new EnemyMeleeTag());
                // }
                // else
                // {
                //     AddComponent(entity, new EnemyRangedTag());
                //     AddComponent(entity, new DesireRange()
                //     {
                //         Value = authoring.desireRange,
                //         Tolerance = authoring.rangeTolerance
                //     });
                // }
                Debug.Log("EnemyAuthoring");
                AddComponent(entity, new MoveSpeedComponent() { Value = authoring.moveSpeed });
                AddComponent(entity, new HealthComponent() { Value = authoring.hp });
                AddComponent(entity, new EnemyTag());
                // AddComponent(entity, new FireCoolDown()
                // {
                //     Timer = 0f,
                //     Interval = authoring.fireInterval
                // });
            }
        }
    }
}