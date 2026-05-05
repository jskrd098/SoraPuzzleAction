using System;
//using UnityEngine;

[Serializable] public class StateMachine
{
    public IState CurrentState { get; private set; }
    public IdleState idleState { get; private set; }
    public WalkState walkState { get; private set; }
    public ClimbState climbState { get; private set; }
    public JumpState jumpState { get; private set; }
    public FallState fallState { get; private set; }
    public PushState pushState { get; private set; }

    public StateMachine(PlayerController player)
    {
        idleState = new IdleState(player);
        walkState = new WalkState(player);
        climbState = new ClimbState(player);
        jumpState = new JumpState(player);
        fallState = new FallState(player);
        pushState = new PushState(player);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState?.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}
