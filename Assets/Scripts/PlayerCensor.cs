using UnityEngine;

public class PlayerCensor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _ladderLayer;

    public bool IsGrounded(Vector2 position) => Physics2D.OverlapCircle(position, 0.6f, _groundLayer);

    public bool IsOnLadder(Collider2D bodyCollider) => bodyCollider.IsTouchingLayers(_ladderLayer);
}