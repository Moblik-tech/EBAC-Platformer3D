using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Moblik.StateMachine;

namespace Moblik.Enemy.Boss
{
    public enum BossAction
    {
        INIT, IDLE, WALK, ATTACK, DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = 0.5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Walk")]
        public float speed = 5f;
        public List<Transform> waypoints;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = 0.5f;

        public HealthBase healthBase;

        private StateMachine<BossAction> _stateMachine;

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }

        private void Init()
        {
            _stateMachine = new StateMachine<BossAction>();
            _stateMachine.Init();

            _stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            _stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            _stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            _stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback)
        {
            int attacks = 0;

            while (attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            endCallback?.Invoke();
        }
        #endregion

        #region RANDOM POINT
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive)
        {
            while (Vector3.Distance(transform.position, t.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            _stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}