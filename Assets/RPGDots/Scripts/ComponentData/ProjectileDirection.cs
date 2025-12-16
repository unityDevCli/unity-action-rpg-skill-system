using Unity.Entities;
using Unity.Mathematics;

namespace RPGDots.Scripts.ComponentData
{
    public struct ProjectileDirection : IComponentData
    {
        public float3 Value;
    }
}