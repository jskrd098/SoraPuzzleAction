using UnityEngine;

public class PlayerMove : MonoBehaviour, IWalkable, IClimbable
{
    [SerializeField] private float _walkSpeed = 5f;
    public float WalkSpeed => _walkSpeed;
    [SerializeField] private float _climbSpeed = 5f;
    public float ClimbSpeed => _climbSpeed;

    public void Walk(Rigidbody2D _rb, Vector2 direction)
    {
        _rb.linearVelocity = new Vector2(direction.x * _walkSpeed, _rb.linearVelocityY);

        // 入力方向に応じてキャラクターの向きを変える
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }

    public void Climb(Rigidbody2D _rb, Vector2 direction)
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, direction.y * _climbSpeed);
    }
}
