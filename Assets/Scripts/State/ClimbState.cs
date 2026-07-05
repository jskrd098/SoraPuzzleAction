using UnityEngine;

public class ClimbState : IState
{
    private readonly PlayerController _player;
    // private readonly PlayerInput _playerInput;
    // private readonly PlayerSensor _playerSensor;
    // private readonly PlayerAnimation _playerAnimation;
    // private readonly Rigidbody2D _rb;
    // private readonly PlayerClimb _playerClimb;

    // private bool _isGrounded;
    // private bool _isOnLadder;
    // private bool _isInLadderAnd;
    // private bool _isInLadderOr;

    private readonly IPlayerInput _input;
    private readonly ICharacterSensor _sensor;
    private readonly ICharacterAnimation _animation;
    // private readonly Rigidbody2D _rb;
    // private bool _isGrounded;
    // private bool _isOnLadder;
    // private bool _isInLadderAnd;

    private enum TransitionType
    {
        None,
        Idle,
        Walk,
        Fall,
        Jump,
        Push,
        Goal,
        Miss
    }

    public ClimbState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _input = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _sensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _animation = player._playerAnimation ?? throw new System.ArgumentNullException(nameof(player._playerAnimation));
        // _playerInput = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        // _playerSensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        // _rb = player._rb ?? throw new System.ArgumentNullException(nameof(player._rb));
        // _playerAnimation = player.GetComponent<PlayerAnimation>();
        // _playerClimb = player.GetComponent<PlayerClimb>();
    }

    public void Enter()
    {
        _animation?.SetClimb(true);
        // _playerAnimation?.SetClimb(true);
    }

    public void Update()
    {
        switch (EvaluateTransition())
        {
            case TransitionType.Idle:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.idleState);
                break;
            case TransitionType.Walk:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.walkState);
                break;
            case TransitionType.Fall:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.fallState);
                break;
            case TransitionType.Jump:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.jumpState);
                break;
            case TransitionType.Push:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.pushState);
                break;
            case TransitionType.Goal:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.goalState);
                break;
            case TransitionType.Miss:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.missState);
                break;
        }
    }

    public void Exit()
    {
        _animation?.SetClimb(false);
        // _playerAnimation?.Play();
        // _playerAnimation?.SetClimb(false);
    }

    private TransitionType EvaluateTransition()
    {
        if (ShouldTransitionToIdle()) return TransitionType.Idle;
        if (ShouldTransitionToWalk()) return TransitionType.Walk;
        if (ShouldTransitionToFall()) return TransitionType.Fall;
        if (ShouldTransitionToJump()) return TransitionType.Jump;
        if (ShouldTransitionToPush()) return TransitionType.Push;
        if (ShouldTransitionToGoal()) return TransitionType.Goal;
        if (ShouldTransitionToMiss()) return TransitionType.Miss;
        return TransitionType.None;
    }

    private bool ShouldTransitionToIdle()
    {
        return _input.moveInput.x == 0;
    }

    private bool ShouldTransitionToWalk()
    {
        return _input.moveInput.x != 0 &&
               _sensor.IsGrounded() &&
               _sensor.CanMove(new Vector2(_input.moveInput.x, 0));
    }

    private bool ShouldTransitionToClimb()
    {
        return _input.moveInput.y != 0 &&
               _sensor.IsInLadder() &&
               _sensor.CanMove(new Vector2(_input.moveInput.y, 0));
    }

    private bool ShouldTransitionToFall()
    {
        return !_sensor.IsGrounded() &&
               !_sensor.IsOnLadder() &&
               !_sensor.IsInLadder();
    }

    private bool ShouldTransitionToJump()
    {
        return _input.jumpInput &&
               _sensor.IsGrounded();
    }

    private bool ShouldTransitionToPush()
    {
        return false;
    }

    private bool ShouldTransitionToGoal()
    {
        // return _player.IsGoalReached;
        return false;
    }

    private bool ShouldTransitionToMiss()
    {
        // return _player.IsMissed;
        return false;
    }
}