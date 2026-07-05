using UnityEngine;

public interface ICharacterSensor
{
    public bool IsGrounded();
    public bool IsOnLadder();
    public bool IsInLadder();
    public bool CanMove(Vector2 direction);
    public void SensorUpdate();
}
