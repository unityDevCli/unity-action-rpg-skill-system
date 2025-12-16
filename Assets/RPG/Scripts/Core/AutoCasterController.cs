using UnityEngine;

public class AutoCaster : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private float searchRadius = 14f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float retargetInterval = 0.15f;

    [Header("Casting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float damage = 20f;
    [SerializeField] private float castCooldown = 0.35f;
    [SerializeField] private float minShootDistance = 0.5f;

    private Transform _target;
    private float _nextRetargetTime;
    private float _nextCastTime;

    private void Update()
    {
        if (Time.time >= _nextRetargetTime)
        {
            _nextRetargetTime = Time.time + retargetInterval;
            _target = FindNearestEnemy();
        }

        if (_target == null) return;
        if (projectilePrefab == null || firePoint == null) return;

        Vector3 toTarget = _target.position - firePoint.position;
        toTarget.y = 0f;

        if (toTarget.magnitude < minShootDistance) return;

        // Quay mặt về phía target (giúp cảm giác "cast đúng hướng")
        if (toTarget.sqrMagnitude > 0.0001f)
            transform.forward = Vector3.Lerp(transform.forward, toTarget.normalized, 25f * Time.deltaTime);

        if (Time.time >= _nextCastTime)
        {
            _nextCastTime = Time.time + castCooldown;

            var go = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(toTarget.normalized, Vector3.up));
            var proj = go.GetComponent<Projectile>();
            if (proj != null)
                proj.Init(toTarget.normalized, damage, enemyLayer);
        }
    }

    private Transform FindNearestEnemy()
    {
        var hits = Physics.OverlapSphere(transform.position, searchRadius, enemyLayer);
        if (hits == null || hits.Length == 0) return null;

        Transform best = null;
        float bestSqr = float.PositiveInfinity;

        for (int i = 0; i < hits.Length; i++)
        {
            var t = hits[i].transform;
            var hp = t.GetComponent<Health>();
            if (hp == null || hp.IsDead) continue;

            float sqr = (t.position - transform.position).sqrMagnitude;
            if (sqr < bestSqr)
            {
                bestSqr = sqr;
                best = t;
            }
        }

        return best;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.9f, 1f, 0.35f);
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
