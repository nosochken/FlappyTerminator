using System;
using UnityEngine;

[RequireComponent(typeof(BulletView), typeof(Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionDetector))]
public abstract class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
	[SerializeField] private float _speed = 5f;
	
	private Rigidbody2D _rigidbody;
	private CollisionDetector _collisionDetector;
	
	public event Action<Bullet> ReadiedForRelease;
	public event Action KilledTarget;
	
	private void Awake()
	{		
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.gravityScale = 0;
		
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
	
	public void Fly()
	{	
		_rigidbody.velocity = transform.right * _speed;
	}
	
	protected abstract bool GetAchieveTarget(Collision2D collision);
	
	private void ReactToCollision(Collision2D collision)
	{
		if (GetAchieveTarget(collision))
			KilledTarget?.Invoke();
			
		ReadiedForRelease?.Invoke(this);
	}
}