using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
	[SerializeField] private EnemySpawnZone _spawnZone;
	[SerializeField] private float _delay = 3f;
	
	private WaitForSeconds _wait;

	protected override void Awake()
	{
		base.Awake();
		
		_wait =  new WaitForSeconds(_delay);
	}

	private void Start()
	{
		StartCoroutine(SpawnWithDelay());
	}

	protected override void ActOnGet(Enemy spawnableObject)
	{
	   	SetSpawnPosition(spawnableObject);
		transform.rotation = _spawnZone.transform.rotation;
		
		base.ActOnGet(spawnableObject);
	}

	protected override void CustomizeObject(Enemy spawnableObject)
	{
		base.CustomizeObject(spawnableObject);
		
		if (spawnableObject.gameObject.TryGetComponent(out EnemyBulletSpawner bulletSpawner))
			bulletSpawner.ObjectKilledTarget += InvokeObjectKilledTarget;
	}

    protected override void ActOnDestroy(Enemy spawnableObject)
    {
        if (spawnableObject.gameObject.TryGetComponent(out EnemyBulletSpawner bulletSpawner))
			bulletSpawner.ObjectKilledTarget -= InvokeObjectKilledTarget;
			
		base.ActOnDestroy(spawnableObject);
    }
	
	private IEnumerator SpawnWithDelay()
	{
		while (isActiveAndEnabled)
		{
			yield return _wait;
			Spawn();
		}
	}
	
	private void SetSpawnPosition(SpawnedKiller spawnableObject)
	{
		float spawnPositionY = 0f;
		
		if (_spawnZone.TryGetComponent(out Collider2D collider))
			spawnPositionY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
			
		Vector2 spawnPosition = new Vector2 (_spawnZone.transform.position.x, spawnPositionY);
		spawnableObject.transform.position = spawnPosition;
	}
}