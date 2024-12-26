using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : SpawnableKillingFlyer<T>
{
    [SerializeField] private T _prefab;

    [SerializeField, Min(1)] private int _poolCapacity;
    [SerializeField, Min(1)] private int _poolMaxSize;

    public event Action ObjectKilledTarget;

    private ObjectPool<T> _pool;
    private List<T> _activeObjects = new List<T>();

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Create(),
        actionOnGet: (spawnableObject) => ActOnGet(spawnableObject),
        actionOnRelease: (spawnableObject) => spawnableObject.gameObject.SetActive(false),
        actionOnDestroy: (spawnableObject) => ActOnDestroy(spawnableObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    public void ReturnAllToPool()
    {
        foreach (T activeObject in _activeObjects)
            Release(activeObject);

        _activeObjects.Clear();
        _pool.Clear();
    }

    protected virtual void Release(T spawnableObject)
    {
        _pool.Release(spawnableObject);
    }

    protected virtual void ActOnDestroy(T spawnableObject)
    {
        spawnableObject.ReadiedForRelease -= ReturnToPool;
        spawnableObject.KilledTarget -= InvokeObjectKilledTarget;

        Destroy(spawnableObject.gameObject);
    }

    protected virtual void SetTransform(T spawnableObject) { }
    protected virtual void CustomizeObject(T spawnableObject) { }

    protected void Spawn()
    {
        T spawnableObject = _pool.Get();
        _activeObjects.Add(spawnableObject);
    }

    protected void InvokeObjectKilledTarget()
    {
        ObjectKilledTarget?.Invoke();
    }

    private T Create()
    {
        T spawnableObject = Instantiate(_prefab);

        spawnableObject.ReadiedForRelease += ReturnToPool;
        spawnableObject.KilledTarget += InvokeObjectKilledTarget;

        CustomizeObject(spawnableObject);

        return spawnableObject;
    }

    private void ActOnGet(T spawnableObject)
    {
        SetTransform(spawnableObject);
        spawnableObject.gameObject.SetActive(true);

        spawnableObject.Fly();
    }

    private void ReturnToPool(T spawnableObject)
    {
        _activeObjects.Remove(spawnableObject);
        _pool.Release(spawnableObject);
    }
}