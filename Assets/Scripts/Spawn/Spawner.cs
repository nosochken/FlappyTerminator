using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
	[SerializeField] private T _prefab;
	
	[SerializeField, Min(1)] private int _poolCapacity = 5;
	[SerializeField, Min(1)] private int _poolMaxSize = 5;
	
	public event Action ObjectKilledTarget;
	
	private ObjectPool<T> _pool;

	protected virtual void Awake()
	{
		_pool = new ObjectPool<T>(
		createFunc: () => Create(),
		actionOnGet: (spawnableObject) => ActOnGet(spawnableObject),
		actionOnRelease : (spawnableObject) => spawnableObject.gameObject.SetActive(false),
		actionOnDestroy : (spawnableObject) => ActOnDestroy(spawnableObject),
		collectionCheck : true,
		defaultCapacity : _poolCapacity,
		maxSize : _poolMaxSize);
	}
	
	private T Create()
	{
		T spawnableObject = Instantiate(_prefab);
		
		spawnableObject.ReadiedForRelease += ReturnToPool;
		spawnableObject.KilledTarget += InvokeObjectKilledTarget;
		
		CustomizeObject(spawnableObject);

		return spawnableObject;
	}
	
	protected virtual void ActOnGet(T spawnableObject)
	{
		spawnableObject.gameObject.SetActive(true);
	}
	
	protected void Spawn()
	{
		_pool.Get();
	}
	
	private void ReturnToPool(T spawnableObject)
	{
		_pool.Release(spawnableObject);
	}
	
	protected virtual void ActOnDestroy(T spawnableObject)
	{
		spawnableObject.ReadiedForRelease -= ReturnToPool;
		spawnableObject.KilledTarget -= InvokeObjectKilledTarget;
		
		Destroy(spawnableObject.gameObject);
	}
	
	protected virtual void CustomizeObject(T spawnableObject) { }
	
	protected void InvokeObjectKilledTarget()
	{
		ObjectKilledTarget?.Invoke();
	}
}
