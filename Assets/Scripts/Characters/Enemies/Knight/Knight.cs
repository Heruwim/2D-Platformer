using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _wolkStopRate = 0.05f;
    [SerializeField] private DetectionZone _attackZone;
    [SerializeField] private DetectionZone _cliffDetectionZone;

    private Rigidbody2D _rigidbody;
    private WalkableDirection _walkDirection;
    private Vector2 _walkDirectionVector = Vector2.right;
    private TouchingDirections _touchingDirections;
    private Animator _animator;
    private Damageable _damageable;

    private bool _hasTarget = false;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    _walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    _walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return _animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            _animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    public enum WalkableDirection
    {
        Right,
        Left
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _touchingDirections = GetComponent<TouchingDirections>();
        _animator = GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = _attackZone.DetectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_touchingDirections.IsGrounded && _touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (!_damageable.LockVelocity)
        {
            if (CanMove)
                _rigidbody.velocity = new Vector2(_walkSpeed * _walkDirectionVector.x, _rigidbody.velocity.y);
            else
                _rigidbody.velocity = new Vector2(Mathf.Lerp(_rigidbody.velocity.x, 0, _wolkStopRate), _rigidbody.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal values right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        _rigidbody.velocity = new Vector2(knockback.x, _rigidbody.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if(_touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
