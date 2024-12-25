using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class SpawnedKiller : MonoBehaviour, ISpawnable<SpawnedKiller>
{
	private CollisionDetector _collisionDetector;
	
	public event Action<SpawnedKiller> ReadiedForRelease;
	public event Action KilledTarget;

    private void Awake()
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
	
	protected abstract bool GetAchieveTarget(Collision2D collision);
	
	private void ReactToCollision(Collision2D collision)
	{
		if (GetAchieveTarget(collision))
			KilledTarget?.Invoke();
			
		ReadiedForRelease?.Invoke(this);
	}
}
