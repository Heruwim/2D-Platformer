using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private Vector2 _knockback = Vector2.zero;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? _knockback : new Vector2(-_knockback.x, _knockback.y);
            bool gotHit = damageable.Hit(_attackDamage, deliveredKnockback);

            if (gotHit)
                Debug.Log(collision.name + " hit for " + _attackDamage);
        }
    }
}
