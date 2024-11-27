using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
	[SerializeField] private KeyCode _movementKey = KeyCode.Space;
	[SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;
	
	public event Action MovementKeyPressed;
	public event Action ShootKeyPressed;

	private void Update()
	{
		ReadMovementKey();
		ReadShootKey();
	}
	
	private void ReadMovementKey()
	{
		if (Input.GetKeyDown(_movementKey))
			MovementKeyPressed?.Invoke();
	}
	
	private void ReadShootKey()
	{
		if (Input.GetKeyDown(_shootKey))
			ShootKeyPressed?.Invoke();
	}
}