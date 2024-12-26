using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Screen : MonoBehaviour
{
    [SerializeField] private Button _button;

    private CanvasGroup _screenGroup;

    public event Action ButtonClicked;

    private void Awake()
    {
        _screenGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ReactButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ReactButtonClick);
    }

    public void Open()
    {
        _screenGroup.alpha = 1f;
        _button.interactable = true;
    }

    public void Close()
    {
        _screenGroup.alpha = 0f;
        _button.interactable = false;
    }

    private void ReactButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}