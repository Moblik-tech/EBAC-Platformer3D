using UnityEngine;
using Moblik.StateMachine;

public class PlayerStateMachine : MonoBehaviour
{
    public StateMachine<CharacterStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<CharacterStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(CharacterStates.IDLE, new IdleState());
        stateMachine.RegisterStates(CharacterStates.MOVE, new MoveState());
        stateMachine.RegisterStates(CharacterStates.JUMP, new JumpState());

        stateMachine.SwitchState(CharacterStates.IDLE);
    }
}