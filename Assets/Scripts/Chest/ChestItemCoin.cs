using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Moblik.Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinPrefab;

    private List<GameObject> _items = new List<GameObject>();

    public Vector2 randomRangePosition = new Vector2(-2f, 2f);
    public Ease ease = Ease.OutBack;
    public float tweenEndTime = 0.5f;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    private void CreateItems()
    {
        for (int i = 0; i < coinNumber; i++)
        {
            var item = Instantiate(coinPrefab);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRangePosition.x, randomRangePosition.y) + Vector3.right * Random.Range(randomRangePosition.x / 2, randomRangePosition.y / 2);
            item.transform.DOScale(0, tweenEndTime).SetEase(ease).From();
            _items.Add(item);
        }
    }

    public override void Collect()
    {
        base.Collect();

        foreach (var item in _items)
        {
            item.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            item.transform.DOScale(0, tweenEndTime / 2).SetDelay(tweenEndTime / 2);
            ItemManager.Instance.AddByType(ItemType.COIN, 1);
        }
    }
}