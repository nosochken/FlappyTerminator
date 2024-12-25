using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class SpawnedKiller : MonoBehaviour
{
	private CollisionDetector _collisionDetector;
	
	public event Action<SpawnedKiller> ReadiedForRelease;
	public event Action KilledTarget;

    protected virtual void Awake()
	{		
		_collisionDetector = GetComponent<CollisionDetector>();
	}
	
	private void OnEnable()
	{
		_collisionDetector.CollisionDetected += ReactToCollision;
	}
	
	private void OnDisable()
	{
		_collisionDetector.CollisionDetected -= ReactToCollision;
	}
	
	protected virtual bool GetAchieveTarget(Collision2D collision) => true;
	
	private void ReactToCollision(Collision2D collision)
	{
		if (GetAchieveTarget(collision))
			KilledTarget?.Invoke();
			
		ReadiedForRelease?.Invoke(this);
	}
}
