using UnityEngine;

public class PushState : IState
{
    private PlayerController player;
    private Animator _anim;

    public PushState(PlayerController player)
    {
        this.player = player;
        _anim = player.GetComponent<Animator>();
    }

    public void Enter()
    {
        // Pushアニメーションへの切替
        _anim.SetBool("PlayerPush", true);
    }

    public void Update(){}

    public void Exit()
    {
        _anim.SetBool("PlayerPush", false);
    }
}