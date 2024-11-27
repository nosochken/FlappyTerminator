using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : BulletSpawner
{
	[SerializeField] private PlayerShooter _playerShooter;
	
	private void OnEnable()
	{
		_playerShooter.Shooting += Spawn;
	}
	
	private void OnDisable()
	{
		_playerShooter.Shooting -= Spawn;
	}
}
