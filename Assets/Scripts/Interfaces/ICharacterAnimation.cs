public interface ICharacterAnimation
{
    public void SetIdle(bool value);
    public void SetWalk(bool value);
    public void SetClimb(bool value);
    public void SetFall(bool value);
    public void SetJump(bool value);
    public void SetPush(bool value);
    public void SetGoal(bool value);
    public void SetMiss(bool value);

    public void Play();
    public void Stop();
}