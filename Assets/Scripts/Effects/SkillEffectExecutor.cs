using System.Collections.Generic;

namespace Effects
{
    public class SkillEffectExecutor
    {
        public void Execute(IEnumerable<ISkillEffect> effects, EffectContext context)
        {
            foreach (var effect in effects) effect.Apply(context);
        }
    }
}