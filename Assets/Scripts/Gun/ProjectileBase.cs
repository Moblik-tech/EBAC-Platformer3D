using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float projectileSpeed = 50f;
    public int damageAmount = 2;
    public float timeToDestroy = 2f;

    public List<string> tagsToHit;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * projectileSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {/*
        foreach(var t in tagsToHit)
        {
            if (collision.collider.CompareTag(t))
            {*/
                var damageable = collision.transform.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    Vector3 knockbackDirection = collision.transform.position - transform.position;

                    knockbackDirection = -knockbackDirection.normalized;
                    knockbackDirection.y = 0;

                    damageable.Damage(damageAmount, knockbackDirection);
                }
                /*
                break;
            }
        }*/

        Destroy(gameObject);
    }
}