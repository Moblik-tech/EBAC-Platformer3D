using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DestructableProp : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthBase healthBase;

    [Header("Visual")]
    [SerializeField] private Transform graphicObject;
    [SerializeField] private float scaleDuration = 0.1f;

    [Header("Hit Feedback")]
    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private int shakeForce = 1;

    [Header("Coin Drop")]
    [SerializeField] private int coinDropAmount = 10;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform dropPosition;
    [SerializeField] private float coinDropDelay = 0.1f;

    private Vector3 _initialGraphicScale;

    private void Awake()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();

        _initialGraphicScale = graphicObject.localScale;

        healthBase.OnDamage += OnDamage;
        healthBase.OnKill += OnKill;
    }

    private void OnDestroy()
    {
        if (healthBase == null) return;

        healthBase.OnDamage -= OnDamage;
        healthBase.OnKill -= OnKill;
    }

    private void OnDamage(HealthBase health)
    {
        ShakeGraphic();
        UpdateGraphicScale(health);
    }

    private void OnKill(HealthBase health)
    {
        DropMultipleCoins();
    }

    private void ShakeGraphic()
    {
        graphicObject.DOKill();
        graphicObject.DOShakeScale(shakeDuration, Vector3.up, shakeForce);
    }

    private void UpdateGraphicScale(HealthBase health)
    {
        float healthPercent = Mathf.Clamp01(health._currentLife / health.startLife);

        Vector3 targetScale = _initialGraphicScale;
        targetScale.y *= healthPercent;

        graphicObject.DOKill();
        graphicObject.DOScale(targetScale, scaleDuration).SetEase(Ease.OutBack);
    }

    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        GameObject coin = Instantiate(coinPrefab, dropPosition.position, Quaternion.identity);

        coin.transform.localScale = Vector3.zero;
        coin.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack);
    }

    [NaughtyAttributes.Button]
    private void DropMultipleCoins()
    {
        for (int i = 0; i < coinDropAmount; i++)
        {
            DropCoins();
        }
    }

    [NaughtyAttributes.Button]
    private void CoroutineMultipleCoinsDrop()
    {
        StartCoroutine(DropMultipleCoinsCoroutine());
    }

    private IEnumerator DropMultipleCoinsCoroutine()
    {
        for (int i = 0; i < coinDropAmount; i++)
        {
            DropCoins();

            yield return new WaitForSeconds(coinDropDelay);
        }
    }
}