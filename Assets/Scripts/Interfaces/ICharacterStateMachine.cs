public interface ICharacterStateMachine
{
    public IState idleState { get; }
    public IState walkState { get; }
    public IState climbState { get; }
    public IState fallState { get; }
    public IState jumpState { get; }
    public IState pushState { get; }
    public IState goalState { get; }
    public IState missState { get; }
    public void Initialize(IState startingState);
    public void TransitionTo(IState nextState);
    public void Update();
}
