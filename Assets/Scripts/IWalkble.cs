using UnityEngine;

public interface IWalkable
{
    public float WalkSpeed { get; }

    public void Walk(Rigidbody2D rb, Vector2 direction);
}
