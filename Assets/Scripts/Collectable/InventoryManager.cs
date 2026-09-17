using System.Collections.Generic;
using Moblik.Utils;
using Moblik.Core.Singleton;

namespace Moblik.Manager
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        public List<ItemSetup> itemSetup;

        private void Start()
        {
            ResetValues();
        }

        private void ResetValues()
        {
            foreach (var item in itemSetup)
            {
                item.scriptobInt.value = 0;
            }
        }

        public void AddByType(ItemType type, int amountIncreased = 0)
        {
            if (amountIncreased <= 0) return;

            itemSetup.Find(i => i.itemType == type).scriptobInt.value += amountIncreased;
        }

        public void RemoveByType(ItemType type, int amountDecreased = 0)
        {
            var item = itemSetup.Find(i => i.itemType == type).scriptobInt;
            item.value -= amountDecreased;

            if (item.value < 0) item.value = 0;
        }

        public ItemSetup GetItemByType(ItemType type)
        {
            return itemSetup.Find(i => i.itemType == type);
        }
    }
}