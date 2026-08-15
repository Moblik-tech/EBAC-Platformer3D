using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFillUpdater : MonoBehaviour
{
    public enum UIFillType
    {
        NONE, HEALTH, AMMO
    }

    public Image uIImage;
    public UIFillType uIFillType = UIFillType.NONE;

    [Header("Animation")]
    public float duration = 0.1f;
    public Ease ease = Ease.OutBack;

    private Tween _currentTween;

    private void OnValidate()
    {
        if (uIImage == null) uIImage = GetComponent<Image>();
    }

    public void UpdateValue(float f)
    {
        uIImage.fillAmount = f;
    }

    public void UpdateValue(float max, float current)
    {
        if (_currentTween != null) _currentTween.Kill();
        uIImage.DOFillAmount(1 - (current / max), duration).SetEase(ease);
    }
}