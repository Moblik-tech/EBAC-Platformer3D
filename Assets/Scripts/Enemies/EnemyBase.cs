using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Moblik.Animation;
using Moblik.Utils;

namespace Moblik.Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public Collider entityCollider;
        public HealthBase healthBase;
        public FlashColor flashColor;
        public ParticleSystem bloodParticleSystem;

        [Header("Player Detection")]
        public bool lookAtPlayer = false;
        public float distanceToLook = 5f;

        [Header("Animation")]
        public bool startWithBorningAnimation = false;
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEaseType = Ease.OutBack;

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

        protected virtual void Init()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
            if (entityCollider == null) entityCollider = GetComponent<Collider>();

            healthBase.OnDamage += OnDamage;
            healthBase.OnKill += OnKill;

            if (startWithBorningAnimation) BornAnimation();
        }

        protected virtual void OnDamage(HealthBase health)
        {
            if (flashColor != null) flashColor.Flash();
            if (bloodParticleSystem != null) bloodParticleSystem.Play();
        }

        protected virtual void OnKill(HealthBase health)
        {
            if (entityCollider != null) entityCollider.enabled = false;

            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        private void OnDestroy()
        {
            if (healthBase == null) return;

            healthBase.OnDamage -= OnDamage;
            healthBase.OnKill -= OnKill;
        }

        private void OnCollisionEnter(Collision other)
        {
            PlayerController player = other.transform.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                player.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            LookAtPlayer();
        }

        public void LookAtPlayer()
        {
            if (!lookAtPlayer || _playerController == null) return;

            Vector3 direction = _playerController.transform.position - transform.position;

            if (direction.sqrMagnitude < distanceToLook * distanceToLook)
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
            if (_animationBase != null) _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distanceToLook);
        }
    }
}