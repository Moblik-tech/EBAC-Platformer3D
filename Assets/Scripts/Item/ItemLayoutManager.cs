using System.Collections.Generic;
using UnityEngine;

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
            foreach (var setup in ItemManager.Instance.itemSetup)
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayouts.Add(item);
            }
        }
    }
}