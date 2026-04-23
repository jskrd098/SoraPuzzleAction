using UnityEngine;

public interface IWalkable
{
    public float WalkSpeed { get; }
    public bool OnGround { get; }
    public bool OnLadder { get; }
    public bool InLadder { get; }

    public void Walk(Rigidbody2D rb, Vector2 direction);
}
