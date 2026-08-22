using System.Collections.Generic;
using UnityEngine;
using Moblik.Core.Singleton;

namespace Moblik.Items
{
    public enum ItemType
    {
        NONE, COIN, LIFE_PACK
    }

    public class ItemManager : Singleton<ItemManager>
    {
        public List<ItemSetup> itemSetup;

        private void Start()
        {
            Reset();
        }

        private void Reset()
        {
            foreach (var item in itemSetup)
            {
                item.sOInt.value = 0;
            }
        }

        public void AddByType(ItemType itemType, int amountIncreased)
        {
            if (amountIncreased < 0) return;
            
            itemSetup.Find(i => i.itemType == itemType).sOInt.value += amountIncreased;
        }

        public void RemoveByType(ItemType itemType, int amountDecreased)
        {
            var item = itemSetup.Find(i => i.itemType == itemType);
            item.sOInt.value -= amountDecreased;

            if (item.sOInt.value < 0) item.sOInt.value = 0;
        }

        public ItemSetup GetItemByType(ItemType itemType)
        {
            return itemSetup.Find(i => i.itemType == itemType);
        }
    }

    [System.Serializable]
    public class ItemSetup
    {
        public ItemType itemType;
        public SOInt sOInt;
        public Sprite icon;
    }
}