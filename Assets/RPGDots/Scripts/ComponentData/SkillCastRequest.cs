using Unity.Entities;

namespace RPGDots.Scripts.ComponentData
{
    public struct SkillCastRequest : IComponentData
    {
        public Entity Caster;
        public float Damage;
        public float BuffValue;
        public float BuffDuration;
    }
}