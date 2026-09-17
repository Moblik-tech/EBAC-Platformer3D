using UnityEngine;

namespace Moblik.Items
{
    public class CollectableBase : MonoBehaviour
    {
        [Header("<------ Base Configs ------>")]
        public string tagToCollect = "Player";
        public float delayToHideObject = 2f;
        public System.Collections.Generic.List<Collider> objectCollider;

        [Header("VFX")]
        public GameObject graphicObject;
        public ParticleSystem collectionParticleSystem;

        [Header("SFX")]
        public AudioSource collectionAudioSource;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCollect))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            if (graphicObject != null) graphicObject.SetActive(false);

            OnCollect();
            Invoke(nameof(HideItem), delayToHideObject);
        }

        private void HideItem()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (collectionParticleSystem != null) collectionParticleSystem.Play();
            if (collectionAudioSource != null) collectionAudioSource.Play();

            foreach (var collider in objectCollider)
            {
                collider.enabled = false;
            }

            //Debug.Log($"{gameObject.name} collected.");
        }
    }
}