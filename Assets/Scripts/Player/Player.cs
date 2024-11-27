using UnityEngine;

[RequireComponent(typeof(PlayerMover), typeof(PlayerShooter))]
public class Player : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader;
	
	private PlayerMover _mover;
	private PlayerShooter _shooter;
	
	private void Awake()
	{
		_mover = GetComponent<PlayerMover>();
		_shooter = GetComponentInChildren<PlayerShooter>();
	}
	
	private void OnEnable()
	{
		_inputReader.MovementKeyPressed += Move;
		_inputReader.ShootKeyPressed += Shoot;
	}
	
	private void Update()
	{
		_mover.ApplyGravityRotation();
	}
	
	private void OnDisable()
	{
		_inputReader.MovementKeyPressed -= Move;
		_inputReader.ShootKeyPressed -= Shoot;
	}	
	
	private void Move() => _mover.Move();
	private void Shoot() => _shooter.Shoot();
}
