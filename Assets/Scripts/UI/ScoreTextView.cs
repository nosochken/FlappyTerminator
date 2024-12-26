using TMPro;
using UnityEngine;

public class ScoreTextView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += Display;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged -= Display;
    }

    private void Display(int score)
    {
        _text.text = score.ToString();
    }
}