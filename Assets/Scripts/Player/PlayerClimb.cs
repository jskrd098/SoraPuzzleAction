using UnityEngine;

public class PlayerClimb : MonoBehaviour, IClimbable
{
    [SerializeField] private float _climbSpeed = 5f;
    public float ClimbSpeed => _climbSpeed;

    public void Climb(Rigidbody2D _rb, Vector2 direction)
    {
        // ’òq‚Å‚ÌˆÚ“®(Î‚ß“ü—Í‚Íã‰º•ûŒü‚ğ—Dæ)
        if (direction.y != 0) direction.x = 0; // ‰¡“ü—Í‚ğ–³Œø‰»
        _rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);
    }
}
