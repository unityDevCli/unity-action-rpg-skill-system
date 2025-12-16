using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace RPGDots.Scripts.Authoring
{
    public class CharacterAuthoring : MonoBehaviour
    {
        public float hp = 100;
        public float attack = 10;

        private class CharacterAuthoringBaker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new StatComponent()
                {
                    Hp = authoring.hp,
                    Attack = authoring.attack
                });
                AddBuffer<BuffElement>(entity);
            }
        }
    }
}