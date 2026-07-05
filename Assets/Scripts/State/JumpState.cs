using UnityEngine;

public class JumpState : IState
{
    private readonly PlayerController _player;
    private readonly IPlayerInput _input;
    private readonly ICharacterSensor _sensor;
    private readonly ICharacterAnimation _animation;
    // private PlayerController _player;
    // private Animator _anim;

    private enum TransitionType
    {
        None,
        Idle,
        Walk,
        Climb,
        Fall,
        Push,
        Goal,
        Miss
    }

    public JumpState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _input = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _sensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _animation = player._playerAnimation ?? throw new System.ArgumentNullException(nameof(player._playerAnimation));
        // this.player = player;
        // _anim = player.GetComponent<Animator>();
    }

    public void Enter()
    {
        _animation?.SetJump(true);
        // _anim.SetBool("PlayerJump", true);
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
            case TransitionType.Climb:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.climbState);
                break;
            case TransitionType.Fall:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.fallState);
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
        _animation?.SetJump(false);
        // _anim.SetBool("PlayerJump", false);
    }

    private TransitionType EvaluateTransition()
    {
        if (ShouldTransitionToIdle()) return TransitionType.Idle;
        if (ShouldTransitionToWalk()) return TransitionType.Walk;
        if (ShouldTransitionToClimb()) return TransitionType.Climb;
        if (ShouldTransitionToFall()) return TransitionType.Fall;
        if (ShouldTransitionToPush()) return TransitionType.Push;
        if (ShouldTransitionToGoal()) return TransitionType.Goal;
        if (ShouldTransitionToMiss()) return TransitionType.Miss;
        return TransitionType.None;
    }

    private bool ShouldTransitionToIdle()
    {
        return _input.moveInput.x == 0 &&
               (_sensor.IsGrounded() || _sensor.IsOnLadder()) &&
               _sensor.CanMove(new Vector2(_input.moveInput.x, 0));
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
               (_sensor.IsInLadder() || _sensor.IsOnLadder()) &&
               _sensor.CanMove(new Vector2(_input.moveInput.y, 0));
    }

    private bool ShouldTransitionToFall()
    {
        return !_sensor.IsGrounded() &&
               !_sensor.IsOnLadder() &&
               !_sensor.IsInLadder();
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
