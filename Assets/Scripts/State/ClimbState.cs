using UnityEngine;
[RequireComponent(typeof(PlayerClimb))]

public class ClimbState : IState
{
    private PlayerController _player;
    private PlayerInput _playerInput;
    private PlayerCensor _playerCensor;
    private Rigidbody2D _rb;
    private PlayerClimb _playerClimb;
    private Animator _anim;

    private bool _isGrounded;
    private bool _isOnLadder;
    //private bool _isInLadder;
    private bool _isInLadderL;
    private bool _isInLadderR;

    public ClimbState(PlayerController player)
    {
        _player = player;
        _playerInput = player._playerInput;
        _playerCensor = player._playerCensor;
        _rb = player._rb;
        _playerClimb = player.GetComponent<PlayerClimb>();
        _anim = player.GetComponent<Animator>();
        _isGrounded = false;
        _isOnLadder = false;
        //_isInLadder = false;
        _isInLadderL = false;
        _isInLadderR = false;
    }

    public void Enter()
    {
        Debug.Log("State : ClimbState Enter");
        // Climbアニメーションへの切替
        _anim.SetBool("PlayerClimb", true);

    }

    public void Update()
    {
        //Debug.Log("State : ClimbState Update");

        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;
        //_isInLadder = _playerCensor._isInLadder;
        _isInLadderL = _playerCensor._isInLadderL;
        _isInLadderR = _playerCensor._isInLadderR;

        // Climb
        if (_isInLadderL && _isInLadderR)
        {
            _playerClimb.Climb(_rb, _playerInput.MoveInput);
            return;
        }

        if (_isGrounded || _isOnLadder)
        {
            // Idle
            if (_playerInput.MoveInput.x == 0)
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
                return;
            }
            // Walk
            else
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
                return;
            }
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
        //Debug.Log("State : ClimbState Exit");
        _anim.SetBool("PlayerClimb", false);
    }
}