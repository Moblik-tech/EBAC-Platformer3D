using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<Collider> colliders;
    public CharacterController characterController;
    public Animator animator;

    public float speed = 5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    public float jumpForce = 15f;

    private float _vSpeed = 0f;

    [Header("Run Setup")]
    public KeyCode runKey = KeyCode.LeftShift;
    public float runSpeed = 2f;

    [Header("Flash")]
    public List<FlashColor> flashColors;
    public HealthBase healthBase;

    private void Awake()
    {
        healthBase = GetComponent<HealthBase>();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Rotate(0f, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0f);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = inputAxisVertical * speed * transform.forward;

        Jump();

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        var isWalking = inputAxisVertical != 0;

        if (isWalking)
        {
            if (Input.GetKey(runKey))
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        if (inputAxisVertical != 0f)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            _vSpeed = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                _vSpeed = jumpForce;
            }
        }
    }

    #region LIFE
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
    }

    private void OnKill(HealthBase h)
    {
        animator.SetTrigger("Death");
        colliders.ForEach(i => i.enabled = false);

        Invoke(nameof(Revive), 2f);
    }

    private void Revive()
    {
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        colliders.ForEach(i => i.enabled = true);
    }
    #endregion

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }
}