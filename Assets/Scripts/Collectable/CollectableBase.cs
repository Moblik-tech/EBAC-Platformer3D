using UnityEngine;
using Moblik.Utils;

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
        public SFXType audioType;
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

        protected virtual void OnCollect()
        {
            if (collectionParticleSystem != null) collectionParticleSystem.Play();
            if (collectionAudioSource != null) PlaySFX();

            foreach (var collider in objectCollider)
            {
                collider.enabled = false;
            }

            //Debug.Log($"{gameObject.name} collected.");
        }

        private void PlaySFX()
        {
            SFXPool.Instance.PlaySFX(audioType);
        }

        private void HideItem()
        {
            gameObject.SetActive(false);
        }
    }
}