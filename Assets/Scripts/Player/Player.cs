using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
	[SerializeField] private InputReader _inputReader;
	
	private PlayerMover _mover;
	private Weapon _weapon;
	
	private void Awake()
	{
		_mover = GetComponent<PlayerMover>();
		_weapon = GetComponentInChildren<Weapon>();
	}
	
	private void FixedUpdate()
	{
		_mover.ApplyGravityRotation();
		
		if (_inputReader.GetMovementKeyState())
			_mover.Move();
			
		if (_inputReader.GetShootKeyState())
			_weapon.Shoot();
	}
}