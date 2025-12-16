using Unity.Entities;

namespace RPGDots.Scripts.ComponentData
{
    public struct BuffElement : IBufferElementData
    {
        public float Value;
        public float Duration;
    }
}