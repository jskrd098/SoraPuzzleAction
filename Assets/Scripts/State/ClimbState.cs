using UnityEngine;

public class ClimbState : IState
{
    private readonly PlayerController _player;
    private readonly IPlayerInput _input;
    private readonly ICharacterSensor _sensor;
    private readonly ICharacterAnimation _animation;
    private readonly IClimbable _climbable;
    private enum TransitionType
    {
        Idle,
        Walk,
        Climb,
        Fall,
        Push,
        Goal,
        Miss
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="player"></param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public ClimbState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _input = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _sensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _animation = player._playerAnimation ?? throw new System.ArgumentNullException(nameof(player._playerAnimation));
        _climbable = player._playerClimb ?? throw new System.ArgumentNullException(nameof(player._playerClimb));
    }

    /// <summary>
    /// 状態に入った際の処理を行う
    /// </summary>
    public void Enter()
    {
        _animation?.SetClimb(true);
    }

    /// <summary>
    /// 毎フレームの更新処理を行う
    /// </summary>
    public void Update()
    {
        // 停止時はAnimationを停止する
        if (_input.moveInput.x == 0 && _input.moveInput.y == 0) _animation.Stop();
        else _animation.Play();
        
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
            case TransitionType.Push:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.pushState);
                break;
            case TransitionType.Goal:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.goalState);
                break;
            case TransitionType.Miss:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.missState);
                break;
            case TransitionType.Climb:
                _climbable?.Climb(_player._rb, _input.moveInput);
                break;
        }
    }

    /// <summary>
    /// 状態から抜ける際の処理を行う
    /// </summary>
    public void Exit()
    {
        _animation?.SetClimb(false);
    }

    /// <summary>
    /// 状態遷移の評価を行う
    /// </summary>
    /// <returns>
    /// TransitionType: 遷移先の状態を示す列挙型
    /// </returns>
    private TransitionType EvaluateTransition()
    {
        if (ShouldTransitionToIdle()) return TransitionType.Idle;
        if (ShouldTransitionToWalk()) return TransitionType.Walk;
        if (ShouldTransitionToFall()) return TransitionType.Fall;
        if (ShouldTransitionToPush()) return TransitionType.Push;
        if (ShouldTransitionToGoal()) return TransitionType.Goal;
        if (ShouldTransitionToMiss()) return TransitionType.Miss;
        return TransitionType.Climb;
    }

    /// <summary>
    /// Idle状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToIdle()
    {
        return _input.moveInput.x == 0 &&
               _input.moveInput.y == 0 &&
               (_sensor.IsGrounded() || _sensor.IsOnLadder());
    }

    /// <summary>
    /// Walk状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToWalk()
    {
        return _input.moveInput.x != 0 &&
               (_sensor.IsGrounded() || _sensor.IsOnLadder()) &&
               _sensor.CanMove(new Vector2(_input.moveInput.x, 0));
    }

    /// <summary>
    /// Fall状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToFall()
    {
        return !_sensor.IsGrounded() &&
               !_sensor.IsOnLadder() &&
               !_sensor.IsInLadderOr();
    }

    /// <summary>
    /// Push状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToPush()
    {
        return false;
    }

    /// <summary>
    /// Goal状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToGoal()
    {
        // return _player.IsGoalReached;
        return false;
    }

    /// <summary>
    /// Miss状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToMiss()
    {
        // return _player.IsMissed;
        return false;
    }
}