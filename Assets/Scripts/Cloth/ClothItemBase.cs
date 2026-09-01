using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moblik.Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration = 1f;
        public string tagToCollect = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCollect))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            Debug.Log("Collected");

            var setup = ClothManager.Instance.GetSetupByType(clothType);
            PlayerController.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}