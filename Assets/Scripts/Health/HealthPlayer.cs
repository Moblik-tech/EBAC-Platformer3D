using UnityEngine;

public class HealthPlayer : HealthBase
{
    private void Start()
    {
        LoadSavedHealth();
    }

    private void LoadSavedHealth()
    {
        if (SaveManager.Instance == null && !SaveManager.Instance.IsLoaded) return;

        int savedHealth = SaveManager.Instance.Setup.playerCurrentHealth;

        SetLife(savedHealth);
    }
}