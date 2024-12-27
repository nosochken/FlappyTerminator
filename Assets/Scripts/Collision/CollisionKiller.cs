using System;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public abstract class CollisionKiller : MonoBehaviour
{
    [SerializeField] private LayerMask _layerOfKillableTarget;

    public event Action KilledTarget;

    private CollisionDetector _collisionDetector;

    protected virtual void Awake()
    {
        _collisionDetector = GetComponent<CollisionDetector>();
    }

    protected virtual void OnEnable()
    {
        _collisionDetector.CollisionDetected += ReactToCollision;
    }

    private void OnDisable()
    {
        _collisionDetector.CollisionDetected -= ReactToCollision;
    }

    protected virtual void PerformAdditionalCollisionAction() { }

    private void ReactToCollision(Collision2D collision)
    {
        if ((_layerOfKillableTarget.value & (1 << collision.gameObject.layer)) != 0)
            KilledTarget?.Invoke();
        else
            PerformAdditionalCollisionAction();
    }
}