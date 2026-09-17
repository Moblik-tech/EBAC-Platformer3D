using System.Collections.Generic;
using UnityEngine;
using Moblik.Manager;

namespace Moblik.Items
{
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform container;

        [NaughtyAttributes.ReadOnly] public List<ItemLayout> itemLayouts;

        private void Start()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            foreach (var setup in InventoryManager.Instance.itemSetup)
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }
    }
}