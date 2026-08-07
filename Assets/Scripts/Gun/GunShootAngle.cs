using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int bulletAmountPerShot = 4;
    public float angle = 15f;

    protected override void Shoot()
    {
        int mult = 0;

        for (int i = 0; i < bulletAmountPerShot; i++)
        {
            if (i % 2 == 0)
            {
                mult++;
            }

            var projectile = Instantiate(prefabProjectile, positionToShoot);

            projectile.transform.position = positionToShoot.position;
            projectile.transform.localEulerAngles = Vector3.zero + (i % 2 == 0 ? angle : -angle) * mult * Vector3.up;
            
            projectile.transform.parent = null;
        }
    }
}