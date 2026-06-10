using UnityEngine;

public class IdleState : IState
{
    private readonly PlayerController _player;
    private readonly PlayerInput _playerInput;
    private readonly PlayerSensor _playerSensor;
    private readonly PlayerAnimation _playerAnimation;
    private readonly Rigidbody2D _rb;

    private bool _isGrounded;
    private bool _isOnLadder;
    private bool _isInLadderAnd;

    public IdleState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _playerInput = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _playerSensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _rb = player._rb ?? throw new System.ArgumentNullException(nameof(player._rb));
        _playerAnimation = player.GetComponent<PlayerAnimation>();
    }

    public void Enter()
    {
        _playerAnimation?.SetIdle(true);
    }

    public void Update()
    {
        _isGrounded = _playerSensor._isGrounded;
        _isOnLadder = _playerSensor._isOnLadder;
        _isInLadderAnd = _playerSensor._isInLadderAnd;
        float moveX = _playerInput.MoveInput.x;
        float moveY = _playerInput.MoveInput.y;

        // Idle
        if (Mathf.Abs(moveX) == 0 && Mathf.Abs(moveY) == 0)
        {
            _rb.linearVelocity = new Vector2(0.0f, _rb.linearVelocityY);
        }

        // Walk
        if ((Mathf.Abs(moveX) > 0 && (_isGrounded || _isOnLadder)) ||
            Mathf.Abs(moveY) > 0 && _isInLadderAnd && (!Mathf.Approximately(_rb.position.x, Mathf.Round(_rb.position.x))))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
        }

        // Climb
        if (Mathf.Abs(moveY) > 0 && _isInLadderAnd && Mathf.Approximately(_rb.position.x, Mathf.Round(_rb.position.x)))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.climbState);
        }

        // Fall
        if (!_isGrounded && !_isOnLadder && !_isInLadderAnd)
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
        _playerAnimation?.SetIdle(false);
    }
}
