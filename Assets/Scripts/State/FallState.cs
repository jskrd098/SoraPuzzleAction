using UnityEngine;

public class FallState : IState
{
    private readonly PlayerController _player;
    private readonly IPlayerInput _input;
    private readonly ICharacterSensor _sensor;
    private readonly ICharacterAnimation _animation;
    private readonly IFallable _fallable;
    private enum TransitionType
    {
        Idle,
        Walk,
        Climb,
        Fall,
        Jump,
        Push,
        Goal,
        Miss
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="player"></param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public FallState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _input = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _sensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _animation = player._playerAnimation ?? throw new System.ArgumentNullException(nameof(player._playerAnimation));
        _fallable = player._playerFall ?? throw new System.ArgumentNullException(nameof(player._playerFall));
    }

    /// <summary>
    /// 状態に入った際の処理を行う
    /// </summary>
    public void Enter()
    {
        _animation?.SetFall(true);
    }

    /// <summary>
    /// 毎フレームの更新処理を行う
    /// </summary>
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
            case TransitionType.Fall:
                _fallable?.Fall(_player._rb);
                break;
        }
    }

    /// <summary>
    /// 状態から抜ける際の処理を行う
    /// </summary>
    public void Exit()
    {
        _animation?.SetFall(false);
    }

    /// <summary>
    /// 状態遷移の評価を行う
    /// </summary>
    /// <returns></returns>
    private TransitionType EvaluateTransition()
    {
        if (ShouldTransitionToIdle()) return TransitionType.Idle;
        if (ShouldTransitionToWalk()) return TransitionType.Walk;
        if (ShouldTransitionToClimb()) return TransitionType.Climb;
        if (ShouldTransitionToJump()) return TransitionType.Jump;
        if (ShouldTransitionToPush()) return TransitionType.Push;
        if (ShouldTransitionToGoal()) return TransitionType.Goal;
        if (ShouldTransitionToMiss()) return TransitionType.Miss;
        return TransitionType.Fall;
    }

    /// <summary>
    /// Idle状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToIdle()
    {
        return _sensor.IsGrounded() || _sensor.IsOnLadder();
    }

    /// <summary>
    /// Walk状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToWalk()
    {
        return _input.moveInput.x != 0 &&
               _sensor.IsGrounded() &&
               _sensor.CanMove(new Vector2(_input.moveInput.x, 0));
    }

    /// <summary>
    /// Climb状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToClimb()
    {
        return _input.moveInput.y != 0 &&
               (_sensor.IsInLadder() || _sensor.IsOnLadder()) &&
               _sensor.CanMove(new Vector2(_input.moveInput.y, 0));
    }

    /// <summary>
    /// Jump状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToJump()
    {
        return _input.jumpInput &&
               _sensor.IsGrounded();
    }

    /// <summary>
    /// Push状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToPush()
    {
        return false;
    }

    /// <summary>
    /// Goal状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToGoal()
    {
        // return _player.IsGoalReached;
        return false;
    }

    /// <summary>
    /// Miss状態へ遷移する条件を評価する
    /// </summary>
    /// <returns></returns>
    private bool ShouldTransitionToMiss()
    {
        // return _player.IsMissed;
        return false;
    }
}