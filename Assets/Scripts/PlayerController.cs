using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerCensor))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerCensor _playerCensor;
    private Rigidbody2D _rb;
    [SerializeField] private float jumpPower = 5f;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMove>();
        _playerCensor = GetComponent<PlayerCensor>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _playerInput.ReadInput();
        //_rb.linearVelocity = new Vector2(_rb.linearVelocityX, _playerInput.JumpInput ? jumpPower : _rb.linearVelocityY);
    } 

    void FixedUpdate()
    {
        if(_playerCensor.IsGrounded(_rb.position))
        {
            _playerMove.Walk(_rb, _playerInput.MoveInput);
        }
        if(_playerCensor.IsOnLadder(_rb.GetComponent<Collider2D>()))
        {
            _playerMove.Climb(_rb, _playerInput.MoveInput);
        }
    }
}