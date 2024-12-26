using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private PlayerMover _mover;
    private Weapon _weapon;

    private Vector2 _startPosition;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        _mover.ApplyGravityRotation();

        if (_inputReader.GetMovementKeyState())
            _mover.Move();

        if (_inputReader.GetShootKeyState())
            _weapon.Shoot();
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _mover.Stop();
    }
}