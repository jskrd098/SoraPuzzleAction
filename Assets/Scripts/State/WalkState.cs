using UnityEngine;

public class WalkState : IState
{
    private readonly PlayerController _player;
    private readonly IPlayerInput _input;
    private readonly ICharacterSensor _sensor;
    private readonly ICharacterAnimation _animation;
    private readonly IWalkable _walkable;
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
    public WalkState(PlayerController player)
    {
        _player = player ?? throw new System.ArgumentNullException(nameof(player));
        _input = player._playerInput ?? throw new System.ArgumentNullException(nameof(player._playerInput));
        _sensor = player._playerSensor ?? throw new System.ArgumentNullException(nameof(player._playerSensor));
        _animation = player._playerAnimation ?? throw new System.ArgumentNullException(nameof(player._playerAnimation));
        _walkable = player._playerWalk ?? throw new System.ArgumentNullException(nameof(player._playerWalk));
    }

    /// <summary>
    /// 状態に入った際の処理を行う
    /// </summary>
    public void Enter()
    {
        _animation?.SetWalk(true);
    }

    /// <summary>
    /// 毎フレームの更新処理を行う
    /// </summary>
    public void Update()
    {
        // Debug.Log($"WalkState update: moveInput={_input.moveInput}, IsGrounded={_sensor.IsGrounded()}, IsOnLadder={_sensor.IsOnLadder()}");
        switch (EvaluateTransition())
        {
            case TransitionType.Idle:
                _player._playerStateMachine.TransitionTo(_player._playerStateMachine.idleState);
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
            case TransitionType.Walk:
                _walkable?.Walk(_player._rb, _input.moveInput);
                break;
        }
    }

    /// <summary>
    /// 状態から抜ける際の処理を行う
    /// </summary>
    public void Exit()
    {
        _animation?.SetWalk(false);
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
        if (ShouldTransitionToClimb()) return TransitionType.Climb;
        if (ShouldTransitionToFall()) return TransitionType.Fall;
        if (ShouldTransitionToJump()) return TransitionType.Jump;
        if (ShouldTransitionToPush()) return TransitionType.Push;
        if (ShouldTransitionToGoal()) return TransitionType.Goal;
        if (ShouldTransitionToMiss()) return TransitionType.Miss;
        return TransitionType.Walk;
    }

    /// <summary>
    /// Idle状態へ遷移する条件を評価する
    /// </summary>
    /// <returns>
    /// bool: 遷移するかどうかを示す真偽値
    /// </returns>
    private bool ShouldTransitionToIdle()
    {
        // bool moveInputZero = _input.moveInput.x == 0;
        // bool isGrounded = _sensor.IsGrounded();
        // bool result = moveInputZero && isGrounded;
        
        // Debug.Log($"ShouldTransitionToIdle: moveInput.x={_input.moveInput.x} (zero={moveInputZero}), IsGrounded={isGrounded} | Result={result}");
        
        // return result;

        return _input.moveInput.x == 0 &&
               (_sensor.IsGrounded() || _sensor.IsOnLadder());
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