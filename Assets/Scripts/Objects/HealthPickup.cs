using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int _healthRestore = 20;
    [SerializeField] private Vector3 _spinRotationSpeed = new Vector3(0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(_healthRestore);
            if (wasHealed)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += _spinRotationSpeed * Time.deltaTime;
    }
}
