using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 8f;
    [SerializeField] private float _airWalkSpeed = 3f;
    [SerializeField] private float _jumpImpuls = 10f;

    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private TouchingDirections _touchingDirection;

    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _isFacingRight = true;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            _animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            _animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !_touchingDirection.IsOnWall)
                {
                    if (_touchingDirection.IsGrounded)
                    {
                        if (IsRunning)
                            return _runSpeed;
                        else
                            return _walkSpeed;
                    }
                    else
                        return _airWalkSpeed;
                }
                else
                    return 0f;
            }
            else
                return 0f;
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirection = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveInput.x * CurrentMoveSpeed, _rigidbody.velocity.y);

        _animator.SetFloat(AnimationStrings.yVelocity, _rigidbody.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        IsMoving = _moveInput != Vector2.zero;

        SetFacingDirection(_moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && _touchingDirection.IsGrounded && CanMove)
        {
            _animator.SetTrigger(AnimationStrings.jumpTrigger);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpImpuls);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
