using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> DetectedColliders = new List<Collider2D>();

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DetectedColliders.Remove(collision);
    }
}
