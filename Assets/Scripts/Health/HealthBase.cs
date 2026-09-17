using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10;
    [SerializeField, NaughtyAttributes.ReadOnly] public float _currentLife;
    public Collider contactCollider;
    public bool destroyOnKill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [Header("UI")]
    public UIFillUpdater uIFillUpdater;

    public float damageReduction = 0f;

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
        // Exclui todas as layers de contato com esse GameObject. Outra forma de fazer isso seria de pôr "-1" ao atribuir o valor da exclusão.
        if (contactCollider != null) contactCollider.excludeLayers = Physics.AllLayers;

        if (destroyOnKill) Destroy(gameObject, 1.5f);

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void ApplyDamage()
    {
        Damage(5);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage - damageReduction;

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
}