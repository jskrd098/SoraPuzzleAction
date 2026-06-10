using UnityEngine;

public class WalkState : IState
{
    private readonly PlayerController _player;
    private readonly PlayerInput _playerInput;
    private readonly PlayerSensor _playerSensor;
    private readonly MovementDirectionResolver _directionResolver;
    private readonly PlayerAnimation _playerAnimation;
    private readonly Rigidbody2D _rb;
    private readonly PlayerWalk _playerWalk;

    private bool _isGrounded;
    private bool _isOnLadder;
    private bool _isInLadderAnd;
    private bool _isInLadderOr;

    public WalkState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _playerInput = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _playerSensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _directionResolver = new MovementDirectionResolver();
        _rb = player._rb ?? throw new System.ArgumentNullException(nameof(player._rb));
        _playerAnimation = player.GetComponent<PlayerAnimation>();
        _playerWalk = player.GetComponent<PlayerWalk>();
    }

    public void Enter()
    {
        _playerAnimation?.SetWalk(true);
    }

    public void Update()
    {
        _isGrounded = _playerSensor._isGrounded;
        _isOnLadder = _playerSensor._isOnLadder;
        _isInLadderAnd = _playerSensor._isInLadderAnd;
        _isInLadderOr = _playerSensor._isInLadderOr;

        Vector2Int moveDir = _directionResolver.ResolveDirection(_player, _playerInput.MoveInput);

        // Climb
        if (moveDir.y > 0 && _isInLadderAnd)
        {
            if (Mathf.Approximately(_rb.position.x, Mathf.Round(_rb.position.x)))
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.climbState);
            }
        }

        // Walk
        if (moveDir.x != 0 && (_isGrounded || _isOnLadder))
        {
            _playerWalk.Walk(_rb, _playerInput.MoveInput);
        }

        // Idle
        if (moveDir.x == 0 && (_isGrounded || _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
        }

        // Fall
        if (!_isGrounded && !_isOnLadder && !_isInLadderOr)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.fallState);
        }

        // Jump
        if (_playerInput.JumpInput)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.jumpState);
        }
    }

    public void Exit()
    {
        _playerAnimation?.SetWalk(false);
    }
}