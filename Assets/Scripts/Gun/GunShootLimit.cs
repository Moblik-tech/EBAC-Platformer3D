using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> uIGunUpdater;

    public int bulletAmount = 15;
    public float timeToReload = 1f;

    private int _currentShots;
    private bool _reloading = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_reloading) yield break;

        while (true)
        {
            if (_currentShots < bulletAmount)
            {
                Shoot();
                _currentShots++;
                CheckReload();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShots);
            }
        }
    }

    private void CheckReload()
    {
        if (_currentShots >= bulletAmount)
        {
            StopShoot();
            StartReload();
        }
    }

    private void StartReload()
    {
        _reloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        float time = 0f;

        while (time < timeToReload)
        {
            time += Time.deltaTime;
            uIGunUpdater.ForEach(i => i.UpdateValue(time/timeToReload));
            yield return new WaitForEndOfFrame();
        }

        _currentShots = 0;
        _reloading = false;
    }

    private void UpdateUI()
    {
        uIGunUpdater.ForEach(i => i.UpdateValue(bulletAmount, _currentShots));
    }

    private void GetAllUIs()
    {
        uIGunUpdater = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}