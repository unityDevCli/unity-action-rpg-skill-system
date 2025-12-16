using Unity.Entities;

namespace RPGDots.Scripts.ComponentData
{
    public struct FireCoolDown : IComponentData
    {
        public float Timer;
        public float Interval;
    }
}