using RPGDots.Scripts.ComponentData;
using Unity.Entities;
using UnityEngine;

public class ProjectileAuthoring : MonoBehaviour
{
    public float speed = 14f;
    public float damage = 20f;
    public float lifetime = 3f;
    public float hitRadius = 0.35f;

    public class Baker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring a)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<ProjectileTag>(e);
            AddComponent(e, new ProjectileData
            {
                Speed = a.speed,
                Damage = a.damage,
                LifeTime = a.lifetime,
                HitRadius = a.hitRadius,
                Owner = Entity.Null
            });
            AddComponent(e, new ProjectileDirection { Value = new Unity.Mathematics.float3(0, 0, 1) });

            // Spawn runtime thôi, nên disable sẵn prefab entity (tránh nằm sẵn trong scene)
            AddComponent<Disabled>(e);
        }
    }
}
