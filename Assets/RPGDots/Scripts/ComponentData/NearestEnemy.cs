using Unity.Entities;
using Unity.Mathematics;

namespace RPGDots.Scripts.ComponentData
{
    public struct NearestEnemy : IComponentData
    {
        public Entity Value;
        public float3 Position;
        public float Distance;
    }
}