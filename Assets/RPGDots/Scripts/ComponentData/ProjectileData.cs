using Unity.Entities;

namespace RPGDots.Scripts.ComponentData
{
    public struct ProjectileData : IComponentData
    {
        public float Speed;
        public float Damage;
        public float LifeTime;
        public float HitRadius;
        public Entity Owner;
    }
}