using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerSensor))]
[RequireComponent(typeof(MovementSensor))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerWalk))]
[RequireComponent(typeof(PlayerClimb))]
[RequireComponent(typeof(PlayerFall))]

public class PlayerController : MonoBehaviour
{
    public PlayerInput _playerInput;
    public PlayerSensor _playerSensor;
    public MovementSensor _movementSensor;
    public PlayerAnimation _playerAnimation;
    public StateMachine _stateMachine;
    public Rigidbody2D _rb;
    // public BoxCollider2D _collider2D;
    // public float InputEpsilon {get; private set;} // 入力の有効判定に使用する小さな値

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerSensor = GetComponent<PlayerSensor>();
        _movementSensor = GetComponent<MovementSensor>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _rb = GetComponent<Rigidbody2D>();
        _stateMachine = new StateMachine(this);
        _stateMachine.Initialize(_stateMachine.idleState);
    }

    void Update()
    {
        // Read Input
        _playerInput.ReadInput();
    } 

    void FixedUpdate()
    {
        // Update Sensor and State Machine
        _playerSensor.SensorUpdate();
        _stateMachine.Update();
    }
}