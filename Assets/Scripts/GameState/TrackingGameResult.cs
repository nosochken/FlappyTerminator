using System;
using UnityEngine;

public class TrackingGameResult : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BulletSpawner _playerBulletSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Ground _ground;

    public event Action GameOver;

    private void OnEnable()
    {
        _playerBulletSpawner.ObjectKilledTarget += AddScore;
        _enemySpawner.ObjectKilledTarget += InvokeGameOver;
        _ground.KilledTarget += InvokeGameOver;
    }

    private void OnDisable()
    {
        _playerBulletSpawner.ObjectKilledTarget -= AddScore;
        _enemySpawner.ObjectKilledTarget -= InvokeGameOver;
        _ground.KilledTarget -= InvokeGameOver;
    }

    private void AddScore() => _scoreCounter.Add();

    private void InvokeGameOver()
    {
        _enemySpawner.ReturnAllToPool();
        _playerBulletSpawner.ReturnAllToPool();

        GameOver?.Invoke();
    }
}