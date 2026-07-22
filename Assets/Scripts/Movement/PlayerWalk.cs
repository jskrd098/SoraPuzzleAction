using UnityEngine;

public class PlayerWalk : MonoBehaviour, IWalkable
{
    [SerializeField] private float WalkSpeed = 5f;
    public float walkSpeed => WalkSpeed;

    /// <summary>
    /// Playerの左右移動
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="direction"></param>
    public void Walk(Rigidbody2D rb, Vector2 direction)
    {
        if (rb == null) return;
        // キャラの移動
        rb.linearVelocity = new Vector2(direction.x * walkSpeed, rb.linearVelocityY);
        // 入力キーに応じてキャラの向きを変える
        if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        // 座標調整
        AlignPos(rb);
    }

    /// <summary>
    /// 座標を調整する
    /// </summary>
    /// <param name="rb"></param>
    private void AlignPos(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustYRound(ref pos, walkSpeed);
        rb.position = pos;
    }
}
