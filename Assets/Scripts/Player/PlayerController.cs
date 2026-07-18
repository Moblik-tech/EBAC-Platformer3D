using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public float speed = 5f;
    private float _currentSpeed;

    public float jumpForce = 7f;

    [Header("Internal Components")]
    public AnimationManager animationManager;
    public PlayerStateMachine playerStateMachine;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = speed;
    }

    private void Update()
    {
        Move();
        Jump();
    }

    public void Move()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            playerStateMachine.stateMachine.SwitchState(CharacterStates.MOVE);
        }
        else
        {
            playerStateMachine.stateMachine.SwitchState(CharacterStates.IDLE);
        }

        _rigidbody.MovementWithLegacyInput(_currentSpeed);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerStateMachine.stateMachine.SwitchState(CharacterStates.JUMP);
            
        }

        _rigidbody.JumpWithLegacyInput(jumpForce);
    }
}