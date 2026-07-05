using UnityEngine;

// [RequireComponent(typeof(PlayerInput))]
// [RequireComponent(typeof(PlayerSensor))]
// [RequireComponent(typeof(MovementSensor))]
// [RequireComponent(typeof(PlayerAnimation))]
// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(PlayerWalk))]
// [RequireComponent(typeof(PlayerClimb))]
// [RequireComponent(typeof(PlayerFall))]

public class PlayerController : MonoBehaviour
{
    public IPlayerInput _playerInput;
    public ICharacterSensor _playerSensor;
    public ICharacterStateMachine _playerStateMachine;
    public ICharacterAnimation _playerAnimation;
    public ICharacterAudio _playerAudio;
    public Rigidbody2D _rb;
    public IWalkable _playerWalk;
    public IIdleable _playerIdle;
    public IClimbable _playerClimb;
    public IFallable _playerFall;
    public IJumpable _playerJump;
    public IPushable _playerPush;
    public IGoalable _playerGoal;
    public IMissable _playerMiss;

    /// <summary>
    /// 初期化処理を行う
    /// </summary>
    void Start()
    {
        // 先にすべてのコンポーネントを初期化
        _playerInput = GetComponent<IPlayerInput>() ?? gameObject.AddComponent<PlayerInput>();
        _playerSensor = GetComponent<ICharacterSensor>() ?? gameObject.AddComponent<PlayerSensor>();
        _playerAnimation = GetComponent<ICharacterAnimation>() ?? gameObject.AddComponent<PlayerAnimation>();
        _playerAudio = GetComponent<ICharacterAudio>();
        _playerStateMachine = GetComponent<ICharacterStateMachine>() ?? gameObject.AddComponent<PlayerStateMachine>();
        _rb = GetComponent<Rigidbody2D>() ?? gameObject.AddComponent<Rigidbody2D>();
        _playerWalk = GetComponent<IWalkable>() ?? gameObject.AddComponent<PlayerWalk>();
        _playerIdle = GetComponent<IIdleable>() ?? gameObject.AddComponent<PlayerIdle>();
        _playerClimb = GetComponent<IClimbable>();
        _playerFall = GetComponent<IFallable>() ?? gameObject.AddComponent<PlayerFall>();
        _playerJump = GetComponent<IJumpable>();
        _playerPush = GetComponent<IPushable>();
        _playerGoal = GetComponent<IGoalable>();
        _playerMiss = GetComponent<IMissable>();

        // その後で状態機械を初期化
        if (_playerStateMachine is PlayerStateMachine playerStateMachine)
        {
            playerStateMachine.InitializeStates(this);
            _playerStateMachine.Initialize(playerStateMachine.idleState);
            // Debug.Log("PlayerController initialized state machine.");
        }
    }

    /// <summary>
    /// 毎フレームの更新処理を行う
    /// </summary>
    void Update()
    {
        // Read Input
        _playerInput?.ReadInput();
        if (_playerInput != null)
        {
            // Debug.Log($"Input move={_playerInput.moveInput} jump={(_playerInput as PlayerInput)?.jumpInput}");
        }
    }

    /// <summary>
    /// 毎フレームの物理演算更新処理を行う
    /// </summary>
    void FixedUpdate()
    {
        // Update Sensor and State Machine
        _playerSensor?.SensorUpdate();
        _playerStateMachine?.Update();
        // if (_playerSensor is PlayerSensor sensor)
        // {
        //     Debug.Log($"Sensor grounded={sensor.IsGrounded()} onLadder={sensor.IsOnLadder()} inLadder={sensor.IsInLadder()}");
        // }
    }
}