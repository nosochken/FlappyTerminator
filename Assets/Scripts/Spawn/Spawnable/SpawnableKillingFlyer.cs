using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SpawnableKillingFlyer<T> : CollisionKiller where T : SpawnableKillingFlyer<T>
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    public event Action<T> ReadiedForRelease;

    protected override void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;

        base.Awake();
    }

    public void Fly()
    {
        _rigidbody.velocity = transform.right * _speed;
    }

    protected override void PerformAdditionalCollisionAction()
    {
        ReadiedForRelease?.Invoke((T)this);
    }
}