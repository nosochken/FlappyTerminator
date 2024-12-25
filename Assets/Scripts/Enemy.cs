using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionDetector))]
public class Enemy : MonoBehaviour, ISpawnable<Enemy>
{
	private CollisionDetector _collisionDetector;
	
	public event Action<Enemy> ReadiedForRelease;
	public event Action KilledTarget;
	
	private void Awake()
	{
		_collisionDetector = GetComponent<CollisionDetector>();
	}
}
