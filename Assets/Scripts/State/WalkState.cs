using UnityEngine;
[RequireComponent(typeof(PlayerWalk))]

public class WalkState : IState
{
    private PlayerController _player;
    private PlayerInput _playerInput;
    private PlayerCensor _playerCensor;
    private Rigidbody2D _rb;
    private PlayerWalk _playerWalk;
    //private MovementUtils _movementUtils;

    private bool _isGrounded;
    private bool _isOnLadder;
    //private bool _isInLadder;
    private bool _isInLadderL;
    private bool _isInLadderR;

    public WalkState(PlayerController player)
    {
        _player = player;
        _playerInput = player._playerInput;
        _playerCensor = player._playerCensor;
        _rb = player._rb;
        _playerWalk = player.GetComponent<PlayerWalk>();
        _isGrounded = false;
        _isOnLadder = false;
        //_isInLadder = false;
    }

    public void Enter()
    {
        Debug.Log("State : WalkState Enter");
        // Walkアニメーションへの切替
    }

    public void Update()
    {
        //Debug.Log("State : WalkState Update");

        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;
        //_isInLadder = _playerCensor._isInLadder;
        _isInLadderL = _playerCensor._isInLadderL;
        _isInLadderR = _playerCensor._isInLadderR;

        // Walk
        if ((_playerInput.MoveInput.x != 0) && (_isGrounded || _isOnLadder))
        {
            _playerWalk.Walk(_rb, _playerInput.MoveInput);
            return;
        }

        // Idle
        if (_playerInput.MoveInput.x == 0)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
            return;
        }

        // Climb
        if (((_playerInput.MoveInput.y != 0) && _isInLadderL && _isInLadderR) ||
            ((_playerInput.MoveInput.y < 0) && _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.climbState);
            return;
        }

        // Jump
        if (_playerInput.JumpInput)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.jumpState);
            return;
        }

        // Fall
        if (!_isGrounded && !_isOnLadder && (!_isInLadderL && !_isInLadderR))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.fallState);
            return;
        }
    }

    public void Exit()
    {
        //Debug.Log("State : WalkState Exit");
    }
}