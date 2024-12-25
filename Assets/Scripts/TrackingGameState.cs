using System;
using UnityEngine;

public class TrackingGameState : MonoBehaviour
{
	[SerializeField] private ScoreCounter _scoreCounter;
	[SerializeField] private PlayerBulletSpawner _playerBulletSpawner;
	[SerializeField] private EnemySpawner _enemySpawner;
	
	public event Action GameOver;
	
	private void OnEnable()
	{
		_playerBulletSpawner.ObjectKilledTarget += AddScore;
		_enemySpawner.ObjectKilledTarget += InvokeGameOver;
	}
	
	private void OnDisable()
	{
		_playerBulletSpawner.ObjectKilledTarget -= AddScore;
		_enemySpawner.ObjectKilledTarget -= InvokeGameOver;
	}
	
	private void AddScore() => _scoreCounter.Add();
	private void InvokeGameOver() => GameOver?.Invoke();
}