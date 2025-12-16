using System.Collections.Generic;
using RPG.Scripts.Events;
using UnityEngine;

namespace RPG.Scripts.Skills
{
    public class SkillController : MonoBehaviour
    {
        [SerializeField] private List<SkillData> skills;

        private Dictionary<string, float> coolDowns = new();
        private Dictionary<SkillType, ISkillExecutor> _executors;

        private void Awake()
        {
            _executors = new Dictionary<SkillType, ISkillExecutor>()
            {
                { SkillType.Damage, new DameSkillExecutor() },
                { SkillType.Buff, new BuffSkillExecutor() }
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryCast(skills[0]);
            }
        }

        private void TryCast(SkillData skillData)
        {
            if (IsOnCoolDown(skillData)) return;

            // _executors[skillData.skillType].Execute(gameObject, skillData);
            EventBus.Publish(new SkillCastEvent
            {
                owner = gameObject,
                skill = skillData
            });
            coolDowns[skillData.skillId] = Time.time + skillData.coolDown;
        }

        private bool IsOnCoolDown(SkillData skillData)
        {
            return coolDowns.ContainsKey(skillData.skillId) && Time.time < coolDowns[skillData.skillId];
        }
    }
}