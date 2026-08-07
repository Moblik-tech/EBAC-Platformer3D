using System;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public float startLife = 10f;
    [SerializeField, NaughtyAttributes.ReadOnly] private float _currentLife;
    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    protected void ResetLife()
    {
        _currentLife = startLife;
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
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        OnDamage?.Invoke(this);
    }
}