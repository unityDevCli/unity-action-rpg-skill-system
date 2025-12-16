using RPG.Scripts.Core;
using UnityEngine;

namespace RPG.Scripts.Enemy
{
    public class EnemyMeleeAI : BaseEnemyAI
    {
        protected override void Update()
        {
            base.Update();
            var toPlayer = targetPlayer.position - transform.position;
            toPlayer.y = 0f;
            var dist = toPlayer.magnitude;

            if (dist > stoppingDistance)
            {
                var dir = toPlayer.normalized;
                transform.position += dir * (moveSpeed * Time.deltaTime);

                if (dir.sqrMagnitude > 0.0001f)
                    transform.forward = Vector3.Lerp(transform.forward, dir, 12f * Time.deltaTime);
            }

            if (dist <= attackRange && Time.time >= NextAttackTime)
            {
                NextAttackTime = Time.time + attackCoolDown;

                var hp = targetPlayer.GetComponent<Health>();
                if (hp != null) hp.TakeDamage(attackDamage);
            }
        }
    }
}