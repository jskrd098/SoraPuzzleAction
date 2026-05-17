using UnityEngine;

public class PlayerWalk : MonoBehaviour, IWalkable
{
    [SerializeField] private float _walkSpeed = 5f;
    public float WalkSpeed => _walkSpeed;

    public void Walk(Rigidbody2D _rb, Vector2 direction)
    {
        // 移動
        _rb.linearVelocity = new Vector2(direction.x * _walkSpeed, _rb.linearVelocityY);

        // 入力方向に応じてキャラクターの向きを変える
        if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

        // 水平移動中は Y を整数へ向かってスナップ
        _rb.PosAdjustToNearestYByFacing(_walkSpeed, direction.y);
    }
}
