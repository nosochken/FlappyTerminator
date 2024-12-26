using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private Weapon _weapon;

    private void OnEnable()
    {
        _weapon.Shooting += Spawn;
    }

    private void OnDisable()
    {
        _weapon.Shooting -= Spawn;
    }

    protected override void SetTransform(Bullet spawnableObject)
    {
        spawnableObject.gameObject.transform.position = _weapon.transform.position;
        spawnableObject.transform.rotation = _weapon.transform.rotation;
    }
}