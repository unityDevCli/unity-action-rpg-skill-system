using System;
using RPG.Scripts.Core;
using UnityEngine;

namespace RPG.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected float speed = 14f;
        [SerializeField] protected float lifeTime = 3f;
        [SerializeField] protected float damage = 10f;

        [SerializeField] protected LayerMask hitMask;
        [SerializeField] protected bool destroyOnHit = true;

        private Vector3 _direction = Vector3.forward;

        public void Init(Vector3 direction, float newDamage, LayerMask newHitMask)
        {
            _direction = direction.sqrMagnitude > 0.0001f ? direction : Vector3.forward;
            damage = newDamage;
            hitMask = newHitMask;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.position += _direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherLayerBit = 1 << other.gameObject.layer;
            var isInMask = (hitMask.value & otherLayerBit) != 0;

            if (isInMask)
            {
                var hp = other.GetComponent<Health>();
                if (hp != null) hp.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }

            if (destroyOnHit) Destroy(gameObject);
        }
    }
}