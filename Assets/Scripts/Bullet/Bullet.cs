using System;
using UnityEngine;

[RequireComponent(typeof(BulletView), typeof(Collider2D), typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour, ISpawnable<Bullet>
{
	[SerializeField] private float _speed = 500f;
	
	private Rigidbody2D _rigidbody;
	private CapsuleCollider2D _collider;
	
	public event Action<Bullet> ReadiedForRelease;
	
	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.gravityScale = 0;
		
		_collider = GetComponent<CapsuleCollider2D>();
		_collider.isTrigger = true;
	}
	
	public void Fly(Vector3 direction)
	{	
		_rigidbody.velocity = direction.normalized * _speed;
	}
}