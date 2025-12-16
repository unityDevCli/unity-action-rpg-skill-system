using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace RPGDots.Scripts.Authoring
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float hp = 100;
        public float attack = 10;
        public float defense = 0;

        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new StatComponent()
                {
                    Attack = authoring.attack,
                    Defense = authoring.defense
                });
                AddComponent(entity, new HealthComponent() { Value = authoring.hp });
                AddComponent(entity, new EnemyTag());
            }
        }
    }
}