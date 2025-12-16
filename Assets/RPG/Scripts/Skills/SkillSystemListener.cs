using RPG.Scripts.Effects;
using RPG.Scripts.Events;
using UnityEngine;

namespace RPG.Scripts.Skills
{
    public class SkillSystemListener : MonoBehaviour
    {
        private SkillEffectExecutor executor = new();

        private void OnEnable()
        {
            EventBus.Subscribe<SkillCastEvent>(OnSkillCast);
        }

        private void OnSkillCast(SkillCastEvent evt)
        {
            var context = new EffectContext()
            {
                caster = evt.owner,
                target = FindTarget(),
                skill = evt.skill
            };
            executor.Execute(context.skill.BuildEffects(), context);
        }

        private GameObject FindTarget()
        {
            return GameObject.FindWithTag("Enemy");
        }
    }
}