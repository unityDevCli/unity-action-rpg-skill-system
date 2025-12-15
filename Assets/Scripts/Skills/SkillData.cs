using System.Collections.Generic;
using Effects;
using Stats;
using UnityEngine;

namespace Skills
{
    [CreateAssetMenu(menuName = "RPG/Skill")]
    public class SkillData : ScriptableObject
    {
        public string skillId;
        public SkillType skillType;

        public float coolDown;
        public float manaCost;

        [Header("Damage")] public float damage;

        [Header("Buff")] public StatType buffStat;
        public float buffValue;
        public float buffDuration;


        public IEnumerable<ISkillEffect> BuildEffects()
        {
            yield return new DamageEffect { damage = damage };
            if (buffDuration > 0)
            {
                yield return new BuffEffect
                {
                    type = buffStat,
                    value = buffValue, duration = buffDuration
                };
            }
        }
    }
}