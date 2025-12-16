using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

namespace RPGDots.Scripts.Authoring
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float maxHp = 100f;
        public float moveSpeed = 6f;

        [Header("Casting")] public float fireInterval = 0.35f;

        public class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring a)
            {
                var e = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<CharacterTag>(e);
                AddComponent(e, new HealthComponent() { Value = a.maxHp, MaxValue = a.maxHp });
                AddComponent(e, new MoveSpeedComponent() { Value = a.moveSpeed });

                AddComponent<PlayerInput>(e);
                AddComponent<NearestEnemy>(e);

                AddComponent(e, new FireCoolDown() { Timer = 0f, Interval = a.fireInterval });
            }
        }
    }
}