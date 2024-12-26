using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _movementKey = KeyCode.Space;
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

    private bool _isMovementKeyPressed;
    private bool _isShootKeyPressed;

    private void Update()
    {
        ReadMovementKey();
        ReadShootKey();
    }

    public bool GetMovementKeyState() => GetKeyState(ref _isMovementKeyPressed);
    public bool GetShootKeyState() => GetKeyState(ref _isShootKeyPressed);

    private void ReadMovementKey()
    {
        if (Input.GetKeyDown(_movementKey))
            _isMovementKeyPressed = true;
    }

    private void ReadShootKey()
    {
        if (Input.GetKeyDown(_shootKey))
            _isShootKeyPressed = true;
    }

    private bool GetKeyState(ref bool keyState)
    {
        if (keyState)
        {
            keyState = false;
            return true;
        }

        return false;
    }
}