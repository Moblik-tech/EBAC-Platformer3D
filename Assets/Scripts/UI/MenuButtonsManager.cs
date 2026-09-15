using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtonsManager : MonoBehaviour
{
    public List<GameObject> menuButtons;

    [Header("Animation")]
    public float duration = 0.2f;
    public float delay = 0.1f;
    public Ease easeType = Ease.OutBack;

    void Start()
    {
        HideAllButtons();
        ShowAllButtons();
    }

    void HideAllButtons()
    {
        foreach (var button in menuButtons)
        {
            button.transform.localScale = Vector3.zero;
            button.SetActive(false);
        }
    }

    void ShowAllButtons()
    {
        for (int i = 0; i < menuButtons.Count; i++)
        {
            var b = menuButtons[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(i * delay).SetEase(easeType);
        }
    }
}