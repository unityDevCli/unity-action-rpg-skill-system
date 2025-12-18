using Unity.Entities;
using Unity.Mathematics;

namespace RPG.Scripts.ComponentData
{
    public struct PlayerInputData : IComponentData
    {
        public float2 MoveInput;
    }
}