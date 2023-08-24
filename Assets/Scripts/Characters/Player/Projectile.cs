using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector2 _moveSpeed = new Vector2 (7f, 0);
    [SerializeField] private Vector2 _knockback = new Vector2(2, 0);
    [SerializeField] private int _damage = 15;

    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed.x * transform.localScale.x, _moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? _knockback : new Vector2(-_knockback.x, _knockback.y);
            bool gotHit = damageable.Hit(_damage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + _damage);
                Destroy(gameObject);
            }

        }
    }
}
