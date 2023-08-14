using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    [SerializeField] private ContactFilter2D _castFilter;
    [SerializeField] private float _groundDistance = 0.05f;
    [SerializeField] private float _wallDistance = 0.2f;
    [SerializeField] private float _ceilingDistance = 0.05f;

    private CapsuleCollider2D _touchingCollider;
    private RaycastHit2D[] _groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] _wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] _ceilingHits = new RaycastHit2D[5];
    private Animator _animator;

    private bool _isGrounded;
    [SerializeField] private bool _isOnWall;
    private bool _isOnCeiling;

    private Vector2 _wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimationStrings.isGrounded, value);
        }
    } 

    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            _animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }
    
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            _animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        _touchingCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGrounded = _touchingCollider.Cast(Vector2.down, _castFilter, _groundHits, _groundDistance) > 0;
        IsOnWall = _touchingCollider.Cast(_wallCheckDirection, _castFilter, _wallHits, _wallDistance) > 0;
        IsOnCeiling = _touchingCollider.Cast(Vector2.up, _castFilter, _ceilingHits, _ceilingDistance) > 0;
    }
}
