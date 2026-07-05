using UnityEngine;

public interface IFallable
{
    public float fallSpeed { get; }

    public void Fall(Rigidbody2D rb);
}
