using RPG.Scripts.ComponentData;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RPG.Scripts.Authoring
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float hp = 100f;
        public float moveSpeed = 4f;

        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new HealthComponent() { Value = authoring.hp });
                AddComponent(entity, new MoveSpeedComponent() { Value = authoring.moveSpeed });
                AddComponent(entity, new PlayerInputData() { MoveInput = float2.zero });
                AddComponent(entity, new PlayerTag());
            }
        }
    }
}