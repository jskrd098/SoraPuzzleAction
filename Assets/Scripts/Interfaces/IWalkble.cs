using UnityEngine;

public interface IWalkable
{
    public float walkSpeed { get; }

    public void Walk(Rigidbody2D rb, Vector2 direction);
}
