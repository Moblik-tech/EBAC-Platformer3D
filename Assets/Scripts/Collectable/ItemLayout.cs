using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Moblik.Utils;

namespace Moblik.Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uIIcon;
        public TextMeshProUGUI uITextValue;

        private ItemSetup _currentSetup;

        public void Load(ItemSetup setup)
        {
            _currentSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uIIcon.sprite = _currentSetup.itemIcon;
        }

        private void Update()
        {
            uITextValue.text = _currentSetup.scriptobInt.value.ToString();
        }
    }
}