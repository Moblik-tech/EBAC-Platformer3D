using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    public float speed = 5f;
    private float _currentSpeed;

    public float jumpForce = 7f;

    [Header("Internal Components")]
    public AnimationManager animationManager;

    private Rigidbody _rigidbody;

    public CharacterStates currentState = CharacterStates.IDLE;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = speed;
    }

    private void Update()
    {
        Move();
        Jump();

        Debug.Log(currentState);
    }

    public void Move()
    {
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (inputs.x != 0 || inputs.y != 0)
        {
            currentState = CharacterStates.MOVE;
        }
        else
        {
            currentState = CharacterStates.IDLE;
        }

        _rigidbody.MovementWithLegacyInput(_currentSpeed);
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            currentState = CharacterStates.JUMP;
        }

        _rigidbody.JumpWithLegacyInput(jumpForce);
    }
}