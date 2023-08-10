using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 5f;

    private Vector2 _moveInput;
    private Rigidbody2D _rigidbody;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_moveInput.x * _walkSpeed, _rigidbody.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        IsMoving = _moveInput != Vector2.zero;
    }
}
