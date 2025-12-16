using UnityEngine;

namespace RPG.Scripts.Skills
{
    public interface ISkillExecutor
    {
        void Execute(GameObject owner, SkillData data);
    }
}