using UnityEngine;

public interface ICharacterSensor
{
    public bool IsGrounded();
    public bool IsOnLadder();
    public bool IsInLadder();
    // public bool IsInLadderAnd();
    // public bool IsInLadderOr();
    // public bool CheckOverlap();
    public bool CanMove(Vector2 direction);
    public void SensorUpdate();
}
