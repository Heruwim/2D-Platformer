using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyingEye : MonoBehaviour
{
    [SerializeField] private float _flightSpeed = 2f;
    [SerializeField] private float _waypointReachedDistance = 0.1f;
    
    [SerializeField] private DetectionZone _biteDetectionZone;
    [SerializeField] private Collider2D _deathCollider;
    [SerializeField] private List<Transform> _waypoints;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private Damageable _damageable;
    private Transform _nextWaypoint;

    private bool _hasTarget = false;
    private int _waypointNum = 0;

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
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        _nextWaypoint = _waypoints[_waypointNum];
    }
    private void OnEnable()
    {
        _damageable.DemageableDeath.AddListener(OnDeath);
    }

    private void Update()
    {
        HasTarget = _biteDetectionZone.DetectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (_damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }        
    }

    public void OnDeath()
    {
        _deathCollider.enabled = true;
        _rigidbody.gravityScale = 2f;
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (_nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(_nextWaypoint.position, transform.position);

        _rigidbody.velocity = directionToWaypoint * _flightSpeed;
        UpdateDirection();

        if (distance <= _waypointReachedDistance)
        {
            _waypointNum++;
            if(_waypointNum >= _waypoints.Count)
            {
                _waypointNum = 0;
            }

            _nextWaypoint = _waypoints[_waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            if (_rigidbody.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (_rigidbody.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
}
