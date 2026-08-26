using System.Collections.Generic;
using UnityEngine;

namespace Moblik.Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        [Header("<======== General ========>")]
        public ItemType itemType;
        public string tagToCollect = "Player";
        public List<Collider> itemCollider;

        [Header("Visual")]
        public GameObject graphicItem;
        public float timeToHideGameObject = 2.5f;
        public ParticleSystem collectionParticleSystem;

        [Header("Audio")]
        public AudioSource audioSource;

        void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(tagToCollect))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if (graphicItem != null) graphicItem.SetActive(false);

            OnCollect();
            Invoke(nameof(HideGameObject), timeToHideGameObject);
        }

        void HideGameObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (collectionParticleSystem != null) collectionParticleSystem.Play();
            if (audioSource != null) audioSource.Play();

            ItemManager.Instance.AddByType(itemType, 1);

            foreach (var collider in itemCollider)
            {
                collider.enabled = false;
            }

            //Debug.Log($"{gameObject.name} collected.");
        }
    }
}