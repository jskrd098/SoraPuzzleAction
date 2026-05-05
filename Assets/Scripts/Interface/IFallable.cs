using UnityEngine;

public interface IFallable
{
    public float FallSpeed { get; }

    public void Fall(Rigidbody2D rb);
}
