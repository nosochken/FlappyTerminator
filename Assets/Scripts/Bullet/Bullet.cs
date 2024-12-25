using UnityEngine;

[RequireComponent(typeof(BulletView), typeof(Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(CollisionDetector))]
public abstract class Bullet : SpawnedKiller
{
	[SerializeField] private float _speed = 5f;
	
	private Rigidbody2D _rigidbody;

	protected override void Awake()
	{
		base.Awake();
		
		_rigidbody = GetComponent<Rigidbody2D>();
		_rigidbody.gravityScale = 0;
	}
	
	public void Fly()
	{	
		_rigidbody.velocity = transform.right * _speed;
	}
}