using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
	[SerializeField] private T _prefab;
	[SerializeField] private Transform _container;
	
	[SerializeField, Min(1)] private int _poolCapacity = 5;
	[SerializeField, Min(1)] private int _poolMaxSize = 5;
	
	private ObjectPool<T> _pool;

	private void Awake()
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

		return spawnableObject;
	}
	
	protected virtual void ActOnGet(T spawnableObject)
	{
		spawnableObject.gameObject.transform.position = _container.transform.position;
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
	
	private void ActOnDestroy(T spawnableObject)
	{
		spawnableObject.ReadiedForRelease -= ReturnToPool;
		Destroy(spawnableObject.gameObject);
	}
}
