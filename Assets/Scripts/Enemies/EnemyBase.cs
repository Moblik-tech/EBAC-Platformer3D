using UnityEngine;
using DG.Tweening;
using Moblik.Animation;

namespace Moblik.Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor flashColor;
        public ParticleSystem particleSystem;
        public float startLife = 10f;
        [SerializeField, NaughtyAttributes.ReadOnly] private float _currentLife;

        [Header("Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEaseType = Ease.OutBack;
        public bool startWithBornAnimation = false;

        [Space(15)]

        [SerializeField] private AnimationBase _animationBase;

        private void Awake()
        {
            Init();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation == true) BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (collider != null) collider.enabled = false;
            PlayAnimationByTrigger(AnimationType.DEATH);
            Destroy(gameObject, 3f);
        }

        public void OnDamage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Play();

            _currentLife -= damage;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }

        public void Damage(float damage)
        {
            Debug.Log("Hit");
            OnDamage(damage);
        }

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEaseType).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion
    }
}