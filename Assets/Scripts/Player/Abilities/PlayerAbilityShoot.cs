using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase firstGun;
    public GunBase secondGun;
    public Transform gunPosition;

    private GunBase _mainGun;
    private GunBase _secondGun;
    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        inputs.Gameplay.MainGun.performed += ctx => UseMainGun();
        inputs.Gameplay.SecondGun.performed += ctx => UseSecondGun();
    }

    private void CreateGun()
    {
        _mainGun = Instantiate(firstGun, gunPosition);
        _secondGun = Instantiate(secondGun, gunPosition);

        _currentGun = _mainGun;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Shoot started");
    }

    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Shoot canceled");
    }

    void UseMainGun()
    {
        _currentGun = _mainGun;
    }

    void UseSecondGun()
    {
        _currentGun = _secondGun;
    }
}