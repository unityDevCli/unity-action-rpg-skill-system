using Unity.Entities;

namespace RPGDots.Scripts.ComponentData
{
    public struct HealthComponent : IComponentData
    {
        public float Value;
        public float MaxValue;
    }
}