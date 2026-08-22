using UnityEngine;

namespace Moblik.Items
{
    public class ItemCollectableCoin : ItemCollectableBase
    {
        [Header("<======== Coins ========>")]
        [Header("Value")]
        public int bonusAmount = 0;

        protected override void OnCollect()
        {
            base.OnCollect();
            ItemManager.Instance.AddByType(itemType, bonusAmount);
        }
    }
}