using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private EnemySpawnZone _spawnZone;
    [SerializeField] private float _delay;

    private WaitForSeconds _wait;

    protected override void Awake()
    {
        base.Awake();

        _wait = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(SpawnWithDelay());
    }

    protected override void CustomizeObject(Enemy spawnableObject)
    {
        TrackBullets(spawnableObject);
    }

    protected override void SetTransform(Enemy spawnableObject)
    {
        SetSpawnPosition(spawnableObject);
        transform.rotation = _spawnZone.transform.rotation;
    }

    protected override void Release(Enemy spawnableObject)
    {
        BulletSpawner bulletSpawner = spawnableObject.gameObject.GetComponentInChildren<BulletSpawner>();

        if (bulletSpawner != null)
            bulletSpawner.ReturnAllToPool();

        base.Release(spawnableObject);
    }

    protected override void ActOnDestroy(Enemy spawnableObject)
    {
        StopTrackingBullets(spawnableObject);
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

    private void SetSpawnPosition(Enemy spawnableObject)
    {
        float spawnPositionY = 0f;

        if (_spawnZone.TryGetComponent(out Collider2D collider))
            spawnPositionY = Random.Range(collider.bounds.min.y, collider.bounds.max.y);

        Vector2 spawnPosition = new Vector2(_spawnZone.transform.position.x, spawnPositionY);
        spawnableObject.transform.position = spawnPosition;
    }

    private void TrackBullets(Enemy spawnableObject)
    {
        BulletSpawner bulletSpawner = spawnableObject.gameObject.GetComponentInChildren<BulletSpawner>();

        if (bulletSpawner != null)
            bulletSpawner.ObjectKilledTarget += InvokeObjectKilledTarget;
    }

    private void StopTrackingBullets(Enemy spawnableObject)
    {
        BulletSpawner bulletSpawner = spawnableObject.gameObject.GetComponentInChildren<BulletSpawner>();

        if (bulletSpawner != null)
            bulletSpawner.ObjectKilledTarget -= InvokeObjectKilledTarget;
    }
}