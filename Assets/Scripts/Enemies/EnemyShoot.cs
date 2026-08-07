using UnityEngine;

namespace Moblik.Enemy
{
    public class EnemyShoot : EnemyBase
    {
        [Header("Shoot")]
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();

            gunBase.StartShoot();
        }
    }
}