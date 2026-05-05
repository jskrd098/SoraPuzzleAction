using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCensor))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public PlayerInput _playerInput;
    public PlayerCensor _playerCensor;
    public StateMachine _stateMachine;
    public Rigidbody2D _rb;
    public Collider2D _collider2D;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerCensor = GetComponent<PlayerCensor>();
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _stateMachine = new StateMachine(this);
        _stateMachine.Initialize(_stateMachine.idleState);
    }

    void Update()
    {
        _playerInput.ReadInput();
        _playerCensor.Enter();
    } 

    void FixedUpdate()
    {
        _playerCensor.CensorUpdate(_collider2D);
        _stateMachine.Update();
    }
}