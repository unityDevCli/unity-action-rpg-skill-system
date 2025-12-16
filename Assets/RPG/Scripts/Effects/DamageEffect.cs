using RPG.Scripts.Combat;

namespace RPG.Scripts.Effects
{
    public class DamageEffect : ISkillEffect
    {
        public float damage;

        public void Apply(EffectContext context)
        {
            var dmg = context.target.GetComponent<IDamageable>();
            dmg?.TakeDamage(damage);
        }
    }
}