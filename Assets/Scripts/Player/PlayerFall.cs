using UnityEngine;

public class PlayerFall : MonoBehaviour, IFallable
{
    [SerializeField] private float _fallSpeed = 6f;
    public float FallSpeed => _fallSpeed;
    
    public void Fall(Rigidbody2D _rb)
    {
        // —Ž‰º
        _rb.linearVelocity = new Vector2(0.0f, -_fallSpeed);
    }
}