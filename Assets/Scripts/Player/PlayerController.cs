using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moblik.Core.Singleton;
using Moblik.Utils;

public class PlayerController : Singleton<PlayerController>
{
    public List<Collider> colliders;
    public CharacterController characterController;
    public Animator animator;

    [Header("Movement")]
    public float speed = 5f;

    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpForce = 15f;

    private float _vSpeed = 0f;
    private bool _jumping = false;

    [Header("Run Setup")]
    public KeyCode runKey = KeyCode.LeftShift;
    public float runSpeed = 2f;

    [Header("Flash")]
    public List<FlashColor> flashColors;
    public HealthBase healthBase;

    [Header("Armour")]
    [SerializeField] private ArmourChanger _armourChanger;
    [SerializeField] private ArmourType _currentArmour = ArmourType.NONE;

    public ArmourType CurrentArmour => _currentArmour;

    protected override void Awake()
    {
        base.Awake();

        if (healthBase == null) healthBase = GetComponent<HealthBase>();

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
        animator.SetBool("Run", inputAxisVertical != 0f);
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                animator.SetTrigger("Land");
            }

            _vSpeed = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                _vSpeed = jumpForce;

                if (!_jumping)
                {
                    _jumping = true;
                    animator.SetTrigger("Jump");
                }
            }
        }
    }

    #region LIFE

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
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

    #region ARMOUR

    public void ChangeArmour(ArmourType armourType)
    {
        var setup = ArmourManager.Instance.GetSetupByType(armourType);

        if (setup == null)
        {
            Debug.LogWarning($"Armour setup not found for type: {armourType}");
            return;
        }

        _currentArmour = armourType;
        ApplyArmourStats(setup);
        _armourChanger.ChangeTexture(setup);
    }

    private void ApplyArmourStats(ArmourSetup setup)
    {
        speed = setup.armourStats.newSpeed;
        healthBase.damageReduction = setup.armourStats.damageReduction;
    }

    #endregion
}