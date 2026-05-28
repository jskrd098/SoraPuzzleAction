using UnityEngine;

public class WalkState : IState
{
    private readonly PlayerController _player;
    private readonly PlayerInput _playerInput;
    private readonly PlayerCensor _playerCensor;
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
        _playerCensor = player._playerCensor ?? throw new System.ArgumentNullException(nameof(player._playerCensor));
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
        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;
        _isInLadderAnd = _playerCensor._isInLadderAnd;
        _isInLadderOr = _playerCensor._isInLadderOr;

        float moveX = _playerInput.MoveInput.x;
        float moveY = _playerInput.MoveInput.y;

        // Climb
        if ((Mathf.Abs(moveY) > 0) && _isInLadderAnd)
        {
            if (Mathf.Approximately(_rb.position.x, Mathf.Round(_rb.position.x)))
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.climbState);
            }
        }

        // Walk
        if ((Mathf.Abs(moveX) > 0) && (_isGrounded || _isOnLadder))
        {
            _playerWalk.Walk(_rb, _playerInput.MoveInput);
        }

        // Idle
        if ((Mathf.Abs(moveX) == 0) && (_isGrounded || _isOnLadder))
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