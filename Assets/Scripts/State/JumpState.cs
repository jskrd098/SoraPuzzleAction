using UnityEngine;

public class JumpState : IState
{
    private PlayerController player;

    public JumpState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        // Jumpアニメーションへの切替
    }

    public void Update(){}

    public void Exit(){}
}
