using UnityEngine;

public class PlayerWalk : MonoBehaviour, IWalkable
{
    [SerializeField] private float _walkSpeed = 5f;
    public float WalkSpeed => _walkSpeed;

    public void Walk(Rigidbody2D _rb, Vector2 direction)
    {
        Vector2 pos;
        
        _rb.linearVelocity = new Vector2(direction.x * _walkSpeed, _rb.linearVelocityY);
        if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        
        pos = _rb.position;
        MovementUtils.PosAdjustY(ref pos, WalkSpeed);
        _rb.position = pos;
    }
}
