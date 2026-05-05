using UnityEngine;
[RequireComponent(typeof(PlayerClimb))]

public class ClimbState : IState
{
    private PlayerController _player;
    private PlayerInput _playerInput;
    private PlayerCensor _playerCensor;
    private Rigidbody2D _rb;
    private PlayerClimb _playerClimb;

    private bool _isGrounded;
    private bool _isOnLadder;
    private bool _isInLadder;

    public ClimbState(PlayerController player)
    {
        _player = player;
        _playerInput = player._playerInput;
        _playerCensor = player._playerCensor;
        _rb = player._rb;
        _playerClimb = player.GetComponent<PlayerClimb>();
        _isGrounded = false;
        _isOnLadder = false;
        _isInLadder = false;
    }

    public void Enter()
    {
        //Debug.Log("State : ClimbState Enter");
        // Climbアニメーションへの切替
    }

    public void Update()
    {
        Debug.Log("State : ClimbState Update");

        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;
        _isInLadder = _playerCensor._isInLadder;

        // Climb
        if (_isInLadder)
        {
            _playerClimb.Climb(_rb, _playerInput.MoveInput);
        }
        else
        {
            // 梯子にいるが入力が無い場合は速度を止める
            _playerClimb.Climb(_rb, Vector2.zero);
        }

        if (_isGrounded || _isOnLadder)
        {
            // Idle
            if (_playerInput.MoveInput.x == 0)
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
            }
            // Walk
            else
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
            }
        }

        // Fall
        if (!_isGrounded && !_isOnLadder && !_isInLadder)
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.fallState);
        }
    }

    public void Exit()
    {
        //Debug.Log("State : ClimbState Exit");
    }
}