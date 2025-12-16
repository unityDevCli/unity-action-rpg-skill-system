using Unity.Entities;

namespace RPGDots.Scripts.ComponentData
{
    public struct DesireRange : IComponentData
    {
        public float Value;
        public float Tolerance;
    }
}