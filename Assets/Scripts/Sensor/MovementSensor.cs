using UnityEngine;

public class MovementSensor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Settings")]
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField]
    [Range(0.8f, 1.0f)]
    private float overlapSizeRate = 0.9f;
    [SerializeField]
    private float gridSize = 1f;

    /// <summary>
    /// 指定方向へ移動可能か判定
    /// </summary>
    public bool CanMove(Vector2 direction)
    {
        Vector2 targetPosition = (Vector2)transform.position + direction.normalized * gridSize;
        Debug.Log($"Checking movement to {targetPosition}");
        return CanMoveTo(targetPosition);
    }

    /// <summary>
    /// 指定座標へ移動可能か判定
    /// </summary>
    public bool CanMoveTo(Vector2 targetPosition)
    {
        Vector2 size = boxCollider.size * overlapSizeRate;
        Collider2D hit = Physics2D.OverlapBox(targetPosition, size, 0f, obstacleLayer);
        return hit == null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (boxCollider == null) return;
        Vector2 size = boxCollider.size * overlapSizeRate;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
#endif
}
