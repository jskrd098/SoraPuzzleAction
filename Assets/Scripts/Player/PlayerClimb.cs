using UnityEngine;

public class PlayerClimb : MonoBehaviour, IClimbable
{
    [SerializeField] private float _climbSpeed = 5f;
    public float ClimbSpeed => _climbSpeed;

    public void Climb(Rigidbody2D _rb, Vector2 direction)
    {
        // ’òŽq‚Å‚ÌˆÚ“®
        _rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);
    }
}
