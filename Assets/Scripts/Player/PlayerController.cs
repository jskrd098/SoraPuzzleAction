using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCensor))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerWalk))]
[RequireComponent(typeof(PlayerClimb))]
[RequireComponent(typeof(PlayerFall))]

public class PlayerController : MonoBehaviour
{
    public PlayerInput _playerInput;
    public PlayerCensor _playerCensor;
    public PlayerAnimation _playerAnimation;
    public StateMachine _stateMachine;
    public Rigidbody2D _rb;
    public Collider2D _collider2D;

    // public float InputEpsilon {get; private set;} // 入力の有効判定に使用する小さな値

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerCensor = GetComponent<PlayerCensor>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _stateMachine = new StateMachine(this);
        _stateMachine.Initialize(_stateMachine.idleState);
        // InputEpsilon = 0.001f;
    }

    void Update()
    {
        // Read Input
        _playerInput.ReadInput();
    } 

    void FixedUpdate()
    {
        // Update Censor and State Machine
        _playerCensor.CensorUpdate(_collider2D);
        _stateMachine.Update();
    }
}