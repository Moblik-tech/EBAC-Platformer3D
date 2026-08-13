using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int bulletAmountPerShot = 4;
    public float angleBetweenProjectiles = 15f;

    protected override void Shoot()
    {
        float startAngle = -angleBetweenProjectiles * (bulletAmountPerShot - 1) / 2f;

        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            float currentAngle = startAngle + angleBetweenProjectiles * i;
            var projectile = Instantiate(prefabProjectile, positionToShoot);

            projectile.transform.position = positionToShoot.position;
            projectile.transform.localEulerAngles = currentAngle * Vector3.up;
            
            projectile.transform.parent = null;
        }
    }
}