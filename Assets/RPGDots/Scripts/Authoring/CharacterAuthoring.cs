using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace RPGDots.Scripts.Authoring
{
    public class CharacterAuthoring : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float hp = 100;
        public float attack = 10;
        public float defense = 5;

        private class CharacterAuthoringBaker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new StatComponent()
                {
                    Attack = authoring.attack,
                    Defense = authoring.defense
                });
                AddComponent(entity, new HealthComponent() { Value = authoring.hp });
                AddComponent(entity, new CharacterTag());
                AddBuffer<BuffElement>(entity);
                AddComponent(entity, new MoveSpeedComponent() { Value = authoring.moveSpeed });
                AddComponent(entity, new NearestEnemy());
            }
        }
    }
}