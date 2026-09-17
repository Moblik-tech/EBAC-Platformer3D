using UnityEngine;
using TMPro;
using Moblik.Utils;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    private void Start()
    {
        SaveManager.Instance.FileLoaded += OnLoad;
    }

    public void OnLoad(SaveSetupParams setup)
    {
        uiTextName.text = $"{uiTextName.text} {setup.currentLevelNumber + 1}";
    }

    private void OnDestroy()
    {
        SaveManager.Instance.FileLoaded -= OnLoad;
    }
}