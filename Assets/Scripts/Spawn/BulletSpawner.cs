using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletSpawner<T> : Spawner<Bullet> where T: MonoBehaviour, ISpawnable<Bullet>
{
	 [SerializeField] private Weapon _gun;
	
	private void OnEnable()
	{
		_gun.Shooting += Spawn;
	}
	
	private void OnDisable()
	{
		_gun.Shooting -= Spawn;
	}
	
	protected override void ActOnGet(Bullet spawnableObject)
	{
		spawnableObject.gameObject.transform.position = _gun.transform.position;
		spawnableObject.transform.rotation = _gun.transform.rotation;
		
		base.ActOnGet(spawnableObject);
		
		spawnableObject.Fly();
	}
	
}
