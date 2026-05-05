using UnityEngine;

public class IdleState : IState
{
    private PlayerController _player;
    private PlayerInput _playerInput;
    private PlayerCensor _playerCensor;
    private Rigidbody2D _rb;

    private bool _isGrounded;
    private bool _isOnLadder;
    private bool _isInLadder;

    public IdleState(PlayerController player)
    {
        _player = player;
        _playerInput = player._playerInput;
        _playerCensor = player._playerCensor;
        _rb = player._rb;
        _isGrounded = false;
        _isOnLadder = false;
        _isInLadder = false;
    }

    public void Enter()
    {
        //Debug.Log("State : IdleState Enter");
        // Idleアニメーションへの切替
    }

    public void Update()
    {
        Debug.Log("State : IdleState Update");

        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;
        _isInLadder = _playerCensor._isInLadder;

        // Idle
        if (_playerInput.MoveInput.x == 0)
        {
            _rb.linearVelocity = new Vector2(0.0f, 0.0f);
        }

        // Walk
        if ((_playerInput.MoveInput.x != 0) && (_isGrounded || _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
        }

        // Climb
        if (_playerInput.MoveInput.y != 0 && _isInLadder)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.climbState);
        }

        // Jump
        if (_playerInput.JumpInput)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.jumpState);
        }

        // Fall
        if (!_isGrounded && !_isOnLadder && !_isInLadder)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.fallState);
        }
    }

    public void Exit()
    {
        //Debug.Log("State : IdleState Exit");
    }
}
