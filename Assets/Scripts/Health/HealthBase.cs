using System;
using System.Collections;
using UnityEngine;
using Moblik.Cloth;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10;
    [SerializeField, NaughtyAttributes.ReadOnly] private float _currentLife;
    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [Header("UI")]
    public UIFillUpdater uIFillUpdater;

    public float damageMultiplier = 1f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void ApplyDamage()
    {
        Damage(5);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage * damageMultiplier;

        if (_currentLife <= 0)
        {
            Kill();
        }

        UpdateUI();
        OnDamage?.Invoke(this);
        //CameraShaker.Instance.Shake();
    }

    public void Damage(float damage, Vector3 knockbackDirection)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uIFillUpdater != null)
        {
            uIFillUpdater.UpdateValue(_currentLife / startLife);
        }
    }

    public void ChangeDamageMultiplier(float damage, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(damage, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float damageMultiplier, float duration)
    {
        this.damageMultiplier = damageMultiplier;
        yield return new WaitForSeconds(duration);
        this.damageMultiplier = 1f;
    }
}