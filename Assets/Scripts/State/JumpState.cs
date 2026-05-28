using UnityEngine;

public class JumpState : IState
{
    private PlayerController player;
    private Animator _anim;

    public JumpState(PlayerController player)
    {
        this.player = player;
        _anim = player.GetComponent<Animator>();
    }

    public void Enter()
    {
        _anim.SetBool("PlayerJump", true);
    }

    public void Update(){}

    public void Exit()
    {
        _anim.SetBool("PlayerJump", false);
    }
}
