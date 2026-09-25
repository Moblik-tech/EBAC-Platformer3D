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
            LoadSaveData();
        }

        private void LoadSaveData()
        {
            if (SaveManager.Instance == null || !SaveManager.Instance.IsLoaded) return;

            SetByType(ItemType.COIN, SaveManager.Instance.Setup.coinAmount);
            SetByType(ItemType.LIFE_PACK, SaveManager.Instance.Setup.lifePackAmount);
        }

        public void AddByType(ItemType type, int amountIncreased = 0)
        {
            if (amountIncreased <= 0) return;

            ItemSetup item = GetItemByType(type);

            if (item == null || item.scriptobInt == null) return;

            item.scriptobInt.amount += amountIncreased;
        }

        public void RemoveByType(ItemType type, int amountDecreased = 0)
        {
            if (amountDecreased <= 0) return;

            ItemSetup item = GetItemByType(type);

            if (item == null || item.scriptobInt == null) return;

            item.scriptobInt.amount -= amountDecreased;
            if (item.scriptobInt.amount < 0) item.scriptobInt.amount = 0;
        }

        public void SetByType(ItemType type, int value)
        {
            ItemSetup item = GetItemByType(type);

            if (item == null || item.scriptobInt == null) return;

            item.scriptobInt.amount = value;
        }

        public int GetAmountByType(ItemType type)
        {
            ItemSetup item = GetItemByType(type);

            if (item == null || item.scriptobInt == null) return 0;

            return item.scriptobInt.amount;
        }

        public ItemSetup GetItemByType(ItemType type)
        {
            return itemSetup.Find(i => i.itemType == type);
        }
    }
}