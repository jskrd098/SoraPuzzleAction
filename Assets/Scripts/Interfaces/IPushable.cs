using UnityEngine;

public interface IPushable
{
    public float pushSpeed { get; }

    public void Push(Rigidbody2D rb, Vector2 direction);
}
