using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _followTarget;

    private Vector2 _startingPosition;
    private float _startingZ;

    private Vector2 _canMoveSinceStart => (Vector2)_camera.transform.position - _startingPosition;
    private float _zDistanceFromTarget => transform.position.z - _followTarget.transform.position.z;

    private float _clippingPlane => (_camera.transform.position.z + (_zDistanceFromTarget > 0? _camera.farClipPlane : _camera.nearClipPlane));

    private float _parallaxFactor => Mathf.Abs(_zDistanceFromTarget) / _clippingPlane;

    private void Start()
    {
        _startingPosition = transform.position;
        _startingZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPosition = _startingPosition + _canMoveSinceStart * _parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ);
    }
}
