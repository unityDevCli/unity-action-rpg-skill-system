using Unity.Entities;
using Unity.Mathematics;

namespace RPGDots.Scripts.ComponentData
{
    public struct PlayerInput : IComponentData
    {
        public float2 Move;
    }
}