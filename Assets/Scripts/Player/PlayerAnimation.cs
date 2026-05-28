using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private int _hashPlayerIdle;
    private int _hashPlayerWalk;
    private int _hashPlayerClimb;
    private int _hashPlayerFall;
    private int _hashPlayerJump;
    private int _hashPlayerPush;
    private int _hashPlayerGoal;
    private int _hashPlayerMiss;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        // Animator.StringToHashを使用して、アニメーションのパラメータのハッシュ値を事前に計算
        _hashPlayerIdle = Animator.StringToHash("PlayerIdle");
        _hashPlayerWalk = Animator.StringToHash("PlayerWalk");
        _hashPlayerClimb = Animator.StringToHash("PlayerClimb");
        _hashPlayerFall = Animator.StringToHash("PlayerFall");
        _hashPlayerJump = Animator.StringToHash("PlayerJump");
        _hashPlayerPush = Animator.StringToHash("PlayerPush");
        _hashPlayerGoal = Animator.StringToHash("PlayerGoal");
        _hashPlayerMiss = Animator.StringToHash("PlayerMiss");
    }

    // アニメーションの状態を切り替えるためのメソッド
    public void SetIdle(bool value) =>_anim.SetBool(_hashPlayerIdle, value);
    public void SetWalk(bool value) => _anim.SetBool(_hashPlayerWalk, value);
    public void SetClimb(bool value) => _anim.SetBool(_hashPlayerClimb, value);
    public void SetFall(bool value) => _anim.SetBool(_hashPlayerFall, value);
    public void SetJump(bool value) => _anim.SetBool(_hashPlayerJump, value);
    public void SetPush(bool value) => _anim.SetBool(_hashPlayerPush, value);
    public void SetGoal(bool value) => _anim.SetBool(_hashPlayerGoal, value);
    public void SetMiss(bool value) => _anim.SetBool(_hashPlayerMiss, value);

    // アニメーションの再生と停止
    public void Play() => _anim.speed = 1;
    public void Stop() => _anim.speed = 0;

}