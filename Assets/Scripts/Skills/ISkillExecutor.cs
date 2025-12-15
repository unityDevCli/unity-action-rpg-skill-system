using UnityEngine;

namespace Skills
{
    public interface ISkillExecutor
    {
        void Execute(GameObject owner, SkillData data);
    }
}