using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float projectileSpeed = 50f;
    public int damageAmount = 2;
    public float timeToDestroy = 2f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * projectileSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IDamageable>(out var damageable))
        {
            Vector3 knockbackDirection = collision.transform.position - transform.position;

            knockbackDirection = -knockbackDirection.normalized;
            knockbackDirection.y = 0;

            damageable.Damage(damageAmount/*, knockbackDirection*/);
        }
        Destroy(gameObject);
    }
}