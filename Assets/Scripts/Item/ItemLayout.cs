using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            uIIcon.sprite = _currentSetup.icon;
        }

        private void Update()
        {
            uITextValue.text = _currentSetup.sOInt.value.ToString();
        }
    }
}