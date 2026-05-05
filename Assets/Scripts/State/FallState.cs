using UnityEngine;
[RequireComponent(typeof(PlayerFall))]

public class FallState : IState
{
    private PlayerController _player;
    [SerializeField] private PlayerFall _playerFall;
    private bool isGrounded;
    private bool isOnLadder;
    private bool isInLadder;
    private Rigidbody2D _rb;

    public FallState(PlayerController player)
    {
        _player = player;
        _playerFall = player.GetComponent<PlayerFall>();
        _rb = _player._rb;
        isGrounded = false;
        isOnLadder = false;
        isInLadder = false;
    }

    public void Enter()
    {
        //Debug.Log("State : FallState Enter");
        // Fallアニメーションへの切替
    }

    public void Update()
    {
        Debug.Log("State : FallState Update");
        isGrounded = _player._playerCensor.IsGrounded(_player._rb.position);
        isOnLadder = _player._playerCensor.IsOnLadder(_player._collider2D);
        isInLadder = _player._playerCensor.IsInLadder(_player._collider2D);

        if (isGrounded || isOnLadder)
        {
            // Idle
            if(_player._playerInput.MoveInput.x == 0)
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
            }
            // Walk
            else
            {
                _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
            }
        }
        else if (isInLadder)
        {
            // Climb
            _player._stateMachine.TransitionTo(_player._stateMachine.climbState);
        }
        else
        {
            _playerFall.Fall(_rb);
        }
    }

    public void Exit()
    {
        //Debug.Log("State : FallState Exit");
    }
}