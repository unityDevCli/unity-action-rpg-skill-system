using RPG.Scripts.Core;
using RPG.Scripts.Projectiles;
using UnityEngine;

namespace RPG.Scripts.Enemy
{
    public class EnemyRangedAI : BaseEnemyAI
    {
        [SerializeField] private float desiredRange = 8f;
        [SerializeField] private float rangeTolerance = 0.7f;

        [Header("Shooting")] [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject projectilePrefab;

        [SerializeField] private float projectileDamage = 8f;
        [SerializeField] private float shootCooldown = 0.8f;

        [Tooltip("HitMask cho projectile của enemy (thường là layer Player)")] [SerializeField]
        private LayerMask playerLayer;

        private float _nextShootTime;

        protected override void Update()
        {
            base.Update();
            var toPlayer = targetPlayer.position - transform.position;
            toPlayer.y = 0f;

            var dist = toPlayer.magnitude;
            var dir = toPlayer.sqrMagnitude > 0.0001f ? toPlayer.normalized : transform.forward;

            var shouldMoveCloser = dist > (desiredRange + rangeTolerance);
            var shouldMoveAway = dist < (desiredRange - rangeTolerance);

            if (shouldMoveCloser)
            {
                transform.position += dir * (moveSpeed * Time.deltaTime);
            }
            else if (shouldMoveAway)
            {
                transform.position -= dir * (moveSpeed * Time.deltaTime);
            }
            else
            {
                // Trong vùng đẹp: dừng lại (không di chuyển)
            }

            // Luôn nhìn về player
            if (dir.sqrMagnitude > 0.0001f)
                transform.forward = Vector3.Lerp(transform.forward, dir, 12f * Time.deltaTime);

            // Chỉ bắn khi trong vùng đứng bắn
            var inGoodRange = dist >= (desiredRange - rangeTolerance) && dist <= (desiredRange + rangeTolerance);

            if (inGoodRange && Time.time >= _nextShootTime && projectilePrefab != null && firePoint != null)
            {
                _nextShootTime = Time.time + shootCooldown;

                var go = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(dir, Vector3.up));
                var proj = go.GetComponent<Projectile>();
                if (proj != null)
                    proj.Init(dir, projectileDamage, playerLayer);
            }
        }
    }
}