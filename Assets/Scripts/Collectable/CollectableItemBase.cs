using UnityEngine;
using Moblik.Utils;
using Moblik.Manager;

namespace Moblik.Items
{
    public class CollectableItemBase : CollectableBase
    {
        [Header("<------ Item Configs ------>")]
        public ItemType itemType;
        [Tooltip("Quantity earned after picking the GameObject.")] public int amountEarned = 0;

        protected override void OnCollect()
        {
            base.OnCollect();
            InventoryManager.Instance.AddByType(itemType, amountEarned);
        }
    }
}