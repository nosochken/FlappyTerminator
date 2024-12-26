using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionDetector : MonoBehaviour
{
    public event Action<Collision2D> CollisionDetected;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionDetected?.Invoke(collision);
    }
}