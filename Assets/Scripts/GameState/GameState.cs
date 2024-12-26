using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private Screen _gameStartScreen;
    [SerializeField] private Screen _gameOverScreen;
    [SerializeField] private TrackingGameResult _gameResult;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _gameStartScreen.ButtonClicked += StartGame;
        _gameOverScreen.ButtonClicked += StartGame;
        _gameResult.GameOver += FinishGame;
    }

    private void Awake()
    {
        Time.timeScale = 0;
        _gameStartScreen.Open();
    }

    private void OnDisable()
    {
        _gameStartScreen.ButtonClicked -= StartGame;
        _gameOverScreen.ButtonClicked -= StartGame;
        _gameResult.GameOver -= FinishGame;
    }

    private void StartGame()
    {
        _scoreCounter.ResetScore();
        _gameStartScreen.Close();
        _gameOverScreen.Close();
        _player.Reset();

        Time.timeScale = 1;
    }

    private void FinishGame()
    {
        _gameOverScreen.Open();

        Time.timeScale = 0;
    }
}