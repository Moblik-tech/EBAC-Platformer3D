using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;

    public float speed = 5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    private float _vSpeed = 0f;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Rotate(0f, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0f);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        // Option 1
        if (inputAxisVertical != 0f)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // Option 2
        //animator.SetBool("Run", inputAxisVertical != 0);
    }

    //[Header("General")]
    //public float speed = 5f;
    //private float _currentSpeed;

    //public float jumpForce = 7f;

    //[Header("Internal Components")]
    //public AnimationManager animationManager;
    //public PlayerStateMachine playerStateMachine;

    //private Rigidbody _rigidbody;

    //private void Start()
    //{
    //    _rigidbody = GetComponent<Rigidbody>();
    //    _currentSpeed = speed;
    //}

    //private void Update()
    //{
    //    Move();
    //    Jump();
    //}

    //public void Move()
    //{
    //    Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    //    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
    //    {
    //        playerStateMachine.stateMachine.SwitchState(CharacterStates.MOVE);
    //    }
    //    else
    //    {
    //        playerStateMachine.stateMachine.SwitchState(CharacterStates.IDLE);
    //    }

    //    _rigidbody.MovementWithLegacyInput(_currentSpeed);
    //}

    //public void Jump()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        playerStateMachine.stateMachine.SwitchState(CharacterStates.JUMP);

    //    }

    //    _rigidbody.JumpWithLegacyInput(jumpForce);
    //}
}