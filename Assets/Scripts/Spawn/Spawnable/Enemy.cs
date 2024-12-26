using System.Collections;
using UnityEngine;

public class Enemy : SpawnableKillingFlyer<Enemy>
{
    [SerializeField] private float _delayInShooting;

    private Weapon _weapon;
    private WaitForSeconds _wait;

    protected override void Awake()
    {
        base.Awake();
        _weapon = GetComponentInChildren<Weapon>();

        _wait = new WaitForSeconds(_delayInShooting);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(ShootWithDelay());
    }

    private IEnumerator ShootWithDelay()
    {
        while (isActiveAndEnabled)
        {
            yield return _wait;
            _weapon.Shoot();
        }
    }
}