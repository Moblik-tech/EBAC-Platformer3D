using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        NONE,
        IDLE,
        RUN,
        DEATH
    }

    // Key
    public Dictionary<States, StateBase> dictionaryState;

    public float timeToStartGame = 1f;
    private StateBase _currentState;

    private void Awake()
    {
        dictionaryState = new Dictionary<States, StateBase>();
        dictionaryState.Add(States.IDLE, new StateBase());

        SwitchState(States.NONE);

        Invoke(nameof(StartGame), timeToStartGame);
    }

    [Button]
    private void StartGame()
    {
        SwitchState(States.NONE);
    }

#if UNITY_EDITOR
    #region DEBUG
    [Button]
    private void ChangeStateToStateX()
    {
        SwitchState(States.NONE);
    }

    [Button]
    private void ChangeStateToStateY()
    {
        SwitchState(States.NONE);
    }
    #endregion
#endif

    private void SwitchState(States state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter();
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnStateStay();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //SwitchState(States.DEATH);
        }
    }
}