using UnityEngine;

public class FallState : IState
{
    private readonly PlayerController _player;
    private readonly PlayerCensor _playerCensor;
    private readonly PlayerAnimation _playerAnimation;
    private readonly Rigidbody2D _rb;
    private readonly PlayerFall _playerFall;

    private bool _isGrounded;
    private bool _isOnLadder;

    public FallState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _playerCensor = player._playerCensor ?? throw new System.ArgumentNullException(nameof(player._playerCensor));
        _rb = player._rb ?? throw new System.ArgumentNullException(nameof(player._rb));
        _playerAnimation = player.GetComponent<PlayerAnimation>();
        _playerFall = player.GetComponent<PlayerFall>();
    }

    public void Enter()
    {
        _playerAnimation?.SetFall(true);
    }

    public void Update()
    {
        _isGrounded = _playerCensor._isGrounded;
        _isOnLadder = _playerCensor._isOnLadder;

        float moveX = _player._playerInput.MoveInput.x;

        // Fall
        if (!_isGrounded && !_isOnLadder)
        {
            _playerFall.Fall(_rb);
            // _playerFall.AlignPosY(_rb);
        }

        // Idle
        if ((Mathf.Abs(moveX) == 0) && (_isGrounded || _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.idleState);
        }

        // Walk
        if ((Mathf.Abs(moveX) > 0) && (_isGrounded || _isOnLadder))
        {
            _player._stateMachine.TransitionTo(_player._stateMachine.walkState);
        }
    }

    public void Exit()
    {
        _playerAnimation?.SetFall(false);
    }
}