using UnityEngine;

namespace Moblik.Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        [Header("<======== General ========>")]
        public ItemType itemType;
        public string tagToCollect = "Player";
        public Collider itemCollider;

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

            Invoke(nameof(HideGameObject), timeToHideGameObject);
            OnCollect();
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

            itemCollider.enabled = false;

            //Debug.Log($"{gameObject.name} collected.");
        }
    }
}