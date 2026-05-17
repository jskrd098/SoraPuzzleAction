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
        // 必要なコンポーネントを取得
        _playerInput = GetComponent<PlayerInput>();
        _playerCensor = GetComponent<PlayerCensor>();
        _rb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _stateMachine = new StateMachine(this);
        _stateMachine.Initialize(_stateMachine.idleState);
    }

    void Update()
    {
        // 入力の更新
        _playerInput.ReadInput();
    } 

    void FixedUpdate()
    {
        // 接地・接触判定の更新と状態の更新
        _playerCensor.CensorUpdate(_collider2D);
        _stateMachine.Update();
    }
}