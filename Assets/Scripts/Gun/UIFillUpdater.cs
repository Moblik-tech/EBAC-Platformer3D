using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Moblik.Utils;

public class UIFillUpdater : MonoBehaviour
{
    public Image uIImage;
    public UIStatsDisplayType uIFillType = UIStatsDisplayType.NONE;

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
        _currentTween?.Kill();
        _currentTween = uIImage.DOFillAmount(current / max, duration).SetEase(ease);
    }
}