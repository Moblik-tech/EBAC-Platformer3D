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

    }
}