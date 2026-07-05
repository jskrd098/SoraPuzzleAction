using UnityEngine;

public class IdleState : IState
{
    private readonly PlayerController _player;
    private readonly IPlayerInput _input;
    private readonly ICharacterSensor _sensor;
    private readonly ICharacterAnimation _animation;
    private readonly Rigidbody2D _rb;
    // private bool _isGrounded;
    // private bool _isOnLadder;
    // private bool _isInLadderAnd;

    private enum TransitionType
    {
        None,
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
    public IdleState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _input = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _sensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _animation = player._playerAnimation ?? throw new System.ArgumentNullException(nameof(player._playerAnimation));
        _rb = player._rb ?? throw new System.ArgumentNullException(nameof(player._rb));
    }

    /// <summary>
    /// 状態に入った際の処理を行う
    /// </summary>
    public void Enter()
    {
        _player._playerIdle?.Idle(_player._rb);
        _animation?.SetIdle(true);
    }

    /// <summary>
    /// 毎フレームの更新処理を行う
    /// </summary>
    public void Update()
    {
        // Debug.Log($"IdleState update: move={_input.moveInput} grounded={_sensor.IsGrounded()} onLadder={_sensor.IsOnLadder()} inLadder={_sensor.IsInLadder()}");
        switch (EvaluateTransition())
        {
            case TransitionType.Walk:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.walkState);
                break;
            case TransitionType.Climb:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.climbState);
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

    /// <summary>
    /// 状態から抜ける際の処理を行う
    /// </summary>
    public void Exit()
    {
        _animation?.SetIdle(false);
    }

    /// <summary>
    /// 状態遷移の評価を行う
    /// </summary>
    /// <returns>
    /// TransitionType: 遷移先の状態を示す列挙型
    /// </returns>
    private TransitionType EvaluateTransition()
    {
        if (ShouldTransitionToWalk()) return TransitionType.Walk;
        if (ShouldTransitionToClimb()) return TransitionType.Climb;
        if (ShouldTransitionToFall()) return TransitionType.Fall;
        if (ShouldTransitionToJump()) return TransitionType.Jump;
        if (ShouldTransitionToPush()) return TransitionType.Push;
        if (ShouldTransitionToGoal()) return TransitionType.Goal;
        if (ShouldTransitionToMiss()) return TransitionType.Miss;
        return TransitionType.None;
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
    /// Climb状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToClimb()
    {
        return _input.moveInput.y != 0 &&
               _sensor.IsInLadder() &&
               _sensor.CanMove(new Vector2(_input.moveInput.y, 0));
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
               !_sensor.IsInLadder();
    }

    /// <summary>
    /// Jump状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToJump()
    {
        return _input.jumpInput &&
               _sensor.IsGrounded();
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
