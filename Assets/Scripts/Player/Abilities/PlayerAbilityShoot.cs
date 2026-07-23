using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void StartShoot()
    {
        gunBase.StartShoot();
        Debug.Log("Shoot started");
    }

    private void CancelShoot()
    {
        gunBase.StopShoot();
        Debug.Log("Shoot canceled");
    }
}