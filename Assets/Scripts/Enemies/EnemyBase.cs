using UnityEngine;
using DG.Tweening;
using Moblik.Animation;
using UnityEngine.Events;

namespace Moblik.Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider entityCollider;
        public FlashColor flashColor;
        public ParticleSystem bloodParticleSystem;
        public float startLife = 10f;
        [SerializeField, NaughtyAttributes.ReadOnly] private float _currentLife;
        
        [Space(15)]
        
        public bool lookAtPlayer = false;
        public float distanceToLook = 5f;

        [Header("Animation")]
        public bool startWithBorningAnimation = false;
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEaseType = Ease.OutBack;

        [Space(15)]

        [SerializeField] private AnimationBase _animationBase;

        [Header("Events")]
        public UnityEvent OnKillEvent;

        private PlayerController _playerController;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _playerController = GameObject.FindAnyObjectByType<PlayerController>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBorningAnimation == true) BornAnimation();
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (entityCollider != null) entityCollider.enabled = false;
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
            Destroy(gameObject, 3f);
        }

        public void OnDamage(float damage)
        {
            if (flashColor != null) flashColor.Flash();
            if (bloodParticleSystem != null) bloodParticleSystem.Play();

            transform.position -= transform.forward;

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

        public void Damage(float damage, Vector3 knockbackDirection)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - knockbackDirection, 0.1f);
        }

        private void OnCollisionEnter(Collision other)
        {
            PlayerController p = other.transform.GetComponent<PlayerController>();

            if (p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            LookAtPlayer();
        }

        public void LookAtPlayer()
        {
            if (lookAtPlayer == true && Vector3.Distance(transform.position, _playerController.transform.position) < distanceToLook)
            {
                transform.LookAt(_playerController.transform.position);
            }
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distanceToLook);
        }
    }
}