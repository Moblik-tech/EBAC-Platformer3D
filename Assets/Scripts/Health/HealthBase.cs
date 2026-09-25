using System;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    [Header("<------ Health General Configs ------>")]
    public int startLife = 10;
    [SerializeField, NaughtyAttributes.ReadOnly] public int _currentLife;
    public int CurrentLife => _currentLife;

    [NaughtyAttributes.ReadOnly] public int damageReductionAmount = 0;
    public bool destroyObjectOnKill = false;

    [Header("Hitbox Detection")]
    public Collider hitboxCollider;
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    [Header("UI")]
    public UIFillUpdater uIHealthUpdater;

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
        SetLife(startLife);
    }

    public void SetLife(int value)
    {
        _currentLife = Mathf.Clamp(value, 0, startLife);
        UpdateUI();
    }

    protected virtual void Kill()
    {
        // Exclui todas as layers de contato com esse GameObject. Outra forma de fazer isso seria de pôr "-1" ao atribuir o valor da exclusão.
        if (hitboxCollider != null) hitboxCollider.excludeLayers = Physics.AllLayers;
        if (destroyObjectOnKill == true) Destroy(gameObject, 1.5f);

        OnKill?.Invoke(this);
    }

    public void Damage(int damage)
    {
        int totalDamage = damage - damageReductionAmount;

        if (totalDamage < 0) totalDamage = 0;
        _currentLife -= totalDamage;

        UpdateUI();
        OnDamage?.Invoke(this);
        //CameraShaker.Instance.Shake();

        if (_currentLife <= 0)
        {
            Kill();
        }
    }

    private void UpdateUI()
    {
        if (uIHealthUpdater != null)
        {
            uIHealthUpdater.UpdateValue(startLife, _currentLife);
        }
    }

    [NaughtyAttributes.Button]
    public void ApplyDamageTest()
    {
        Damage(1);
    }
}