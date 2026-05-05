using UnityEngine;

public class PushState : IState
{
    private PlayerController player;

    public PushState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Pushアニメーションへの切替
    }

    public void Update(){}

    public void Exit(){}
}