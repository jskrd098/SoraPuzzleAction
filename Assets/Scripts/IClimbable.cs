using UnityEngine;

public interface IClimbable
{
    public float ClimbSpeed { get; }

    public void Climb(Rigidbody2D rb, Vector2 direction);
}