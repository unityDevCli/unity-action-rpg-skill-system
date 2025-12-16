using RPG.Scripts.Stats;
using UnityEngine;

namespace RPG.Scripts.Skills
{
    public class BuffSkillExecutor : ISkillExecutor
    {
        public void Execute(GameObject owner, SkillData data)
        {
            var stat = owner.GetComponent<StatComponent>();
            if (stat == null) return;
            owner.GetComponent<MonoBehaviour>()
                .StartCoroutine(Buff.Apply(stat, data.buffStat, data.buffValue, data.buffDuration));
        }
    }
}