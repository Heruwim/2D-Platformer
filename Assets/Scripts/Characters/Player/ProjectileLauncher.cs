using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Transform _launchPoint;
    [SerializeField] GameObject _projactilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(_projactilePrefab, _launchPoint.position, _projactilePrefab.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? 1 : -1,
            originalScale.y,
            originalScale.z
            );
    }
}
