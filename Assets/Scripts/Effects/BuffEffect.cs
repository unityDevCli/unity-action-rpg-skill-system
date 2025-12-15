using Stats;
using UnityEngine;

namespace Effects
{
    public class BuffEffect : ISkillEffect
    {
        public StatType type;
        public float value;
        public float duration;

        public void Apply(EffectContext context)
        {
            var statComponent = context.caster.GetComponent<StatComponent>();
            context.caster.GetComponent<MonoBehaviour>()
                .StartCoroutine(Buff.Apply(statComponent, type, value, duration));
        }
    }
}