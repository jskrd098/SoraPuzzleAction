using UnityEngine;
[RequireComponent(typeof(PlayerWalk))]

public class WalkState : IState
{
    private PlayerController _player;
    private PlayerInput _playerInput;
    private PlayerCensor _playerCensor;
    private Rigidbody2D _rb;
    private PlayerWalk _playerWalk;

    private bool _isGrounded;
    private bool _isOnLadder;
    private bool _isInLadder;

    public WalkState(PlayerController player)
    {
        _player = player;
        _playerInput = player._playerInput;
        _playerCensor = player._playerCensor;
        _rb = player._rb;
        _playerWalk = player.GetComponent<PlayerWalk>();
        _isGrounded = false;
        _isOnLadder = false;
        _isInLadder = false;
    }

    public void Enter()
    {
        //Debug.Log("State : WalkState Enter");
        // Y座標の正規化
        _playerWalk.PosAdjust(_rb);
        // Walkアニメーションへの切替
    }

    public void Update()
    {
        Debug.Log("State : WalkState Update");

        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;
        _isInLadder = _playerCensor._isInLadder;

        // Walk
        if ((_playerInput.MoveInput.x != 0) && (_isGrounded || _isOnLadder))
        {
            _playerWalk.Walk(_rb, _playerInput.MoveInput);
        }

        // Idle
        if (_playerInput.MoveInput.x == 0)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
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
        //Debug.Log("State : WalkState Exit");
    }
}