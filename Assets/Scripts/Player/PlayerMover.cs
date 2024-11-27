using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
   [SerializeField] private float _tapForce;
   [SerializeField] private float _speed;
   [SerializeField] private float _rotationSpeed;
   [SerializeField] private float _maxRotationZ;
   [SerializeField] private float _minRotationZ;
   
   private Vector3 _startPosition;
   private Rigidbody2D _rigidbody;
   private Quaternion _minRotation;
   private Quaternion _maxRotation;
   
   private void Start()
   {
		_rigidbody = GetComponent<Rigidbody2D>();
		
		_startPosition = transform.position;
		_maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
		_minRotation = Quaternion.Euler(0, 0, _minRotationZ);

		Reset();
	}
	
	public void Move()
	{
		_rigidbody.velocity = new Vector2(_speed, _tapForce);
		transform.rotation = _maxRotation;
	}
	
	public void ApplyGravityRotation()
	{
		transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
	}

	public void Reset()
	{
		transform.position = _startPosition;
		transform.rotation = Quaternion.identity;
		_rigidbody.velocity = Vector2.zero;
	}
}