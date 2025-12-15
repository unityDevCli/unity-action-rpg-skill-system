using Combat;
using UnityEngine;

namespace Skills
{
    public class DameSkillExecutor : ISkillExecutor
    {
        public void Execute(GameObject owner, SkillData data)
        {
            var target = FindTarget();
            if (target == null) return;
            var damageable = target.GetComponent<IDamageable>();
            if (damageable == null) return;

            damageable.TakeDamage(data.damage);
        }

        private GameObject FindTarget()
        {
            return GameObject.FindGameObjectWithTag("Enemy");
        }
    }
}