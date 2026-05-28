using UnityEngine;

public class PlayerClimb : MonoBehaviour, IClimbable
{
    [SerializeField] private float _climbSpeed = 5f;
    public float ClimbSpeed => _climbSpeed;

    public void Climb(Rigidbody2D _rb, Vector2 direction)
    {
        if (direction.y != 0) direction.x = 0;
        _rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);
        if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

    }

    public void AlignPosX(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustX(ref pos, ClimbSpeed);
        rb.position = pos;
    }
    public void AlignPosY(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustY(ref pos, ClimbSpeed);
        rb.position = pos;
    }
}
