using System;
using UnityEngine;

[Serializable] public class PlayerStateMachine : MonoBehaviour, ICharacterStateMachine
{
    public IState CurrentState { get; private set; }
    public IState idleState { get; private set; }
    public IState walkState { get; private set; }
    public IState climbState { get; private set; }
    public IState fallState { get; private set; }
    public IState jumpState { get; private set; }
    public IState pushState { get; private set; }
    public IState goalState { get; private set; }
    public IState missState { get; private set; }

    /// <summary>
    /// 依存関係の構築を行う
    /// </summary>
    /// <param name="player"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void InitializeStates(PlayerController player)
    {
        if (player == null)
        {
            throw new ArgumentNullException(nameof(player));
        }

        idleState = new IdleState(player);
        walkState = new WalkState(player);
        climbState = new ClimbState(player);
        jumpState = new JumpState(player);
        fallState = new FallState(player);
        pushState = new PushState(player);
    }

    /// <summary>
    /// 実行開始時の設定を行う
    /// </summary>
    /// <param name="startingState"></param>
    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState?.Enter();
    }

    /// <summary>
    /// 現在の状態から指定された状態に遷移する
    /// </summary>
    /// <param name="nextState"></param>
    public void TransitionTo(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState?.Enter();
    }

    /// <summary>
    /// 毎フレームの更新を行う
    /// </summary>
    public void Update()
    {
        CurrentState?.Update();
    }
}
