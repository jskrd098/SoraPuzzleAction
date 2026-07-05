using UnityEngine;

public interface IJumpable
{
    public float jumpPower { get; }

    public void Jump(Rigidbody2D rb);
}
