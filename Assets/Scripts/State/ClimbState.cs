using UnityEngine;

public class ClimbState : IState
{
    private readonly PlayerController _player;
    private readonly PlayerInput _playerInput;
    private readonly PlayerSensor _playerSensor;
    private readonly PlayerAnimation _playerAnimation;
    private readonly Rigidbody2D _rb;
    private readonly PlayerClimb _playerClimb;

    private bool _isGrounded;
    private bool _isOnLadder;
    private bool _isInLadderAnd;
    private bool _isInLadderOr;

    public ClimbState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _playerInput = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _playerSensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _rb = player._rb ?? throw new System.ArgumentNullException(nameof(player._rb));
        _playerAnimation = player.GetComponent<PlayerAnimation>();
        _playerClimb = player.GetComponent<PlayerClimb>();
    }

    public void Enter()
    {
        _playerAnimation?.SetClimb(true);
    }

    public void Update()
    {
        _isGrounded = _playerSensor._isGrounded;
        _isOnLadder = _playerSensor._isOnLadder;
        _isInLadderAnd = _playerSensor._isInLadderAnd;
        _isInLadderOr = _playerSensor._isInLadderOr;

        float moveX = _playerInput.MoveInput.x;
        float moveY = _playerInput.MoveInput.y;

        // Animation
        if ((Mathf.Abs(moveX) == 0) && (Mathf.Abs(moveY) == 0))
        {
            _playerAnimation?.Stop();
        }
        else
        {
            _playerAnimation?.Play();
        }

        // Climb
        if (_isInLadderAnd)
        {
            _playerClimb.Climb(_rb, _playerInput.MoveInput);
            // Align Position
            if (Mathf.Abs(moveX) > 0) _playerClimb.AlignPosY(_rb);
            if (Mathf.Abs(moveY) > 0) _playerClimb.AlignPosX(_rb);
        }

        // Idle
        if ((Mathf.Abs(moveX) == 0) && (Mathf.Abs(moveY) == 0) && (_isGrounded || _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
        }

        // Walk
        else if ((Mathf.Abs(moveX) > 0) && (_isGrounded || _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
        }

        // Fall
        if (!_isGrounded && !_isOnLadder && !_isInLadderOr)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.fallState);
        }
    }

    public void Exit()
    {
        _playerAnimation?.Play();
        _playerAnimation?.SetClimb(false);
    }
}