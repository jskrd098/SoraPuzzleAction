using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMove _playerMove;
    private Rigidbody2D _rb;
    [SerializeField] private float jumpPower = 5f;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMove>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _playerInput.ReadInput();
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, _playerInput.JumpInput ? jumpPower : _rb.linearVelocityY);
    } 

    void FixedUpdate()
    {
        _playerMove.Walk(_rb, _playerInput.MoveInput);
        _playerMove.Climb(_rb, _playerInput.MoveInput);
    }
}