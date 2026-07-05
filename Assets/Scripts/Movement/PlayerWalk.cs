using UnityEngine;

public class PlayerWalk : MonoBehaviour, IWalkable
{
    [SerializeField] private float WalkSpeed = 5f;
    public float walkSpeed => WalkSpeed;

    public void Walk(Rigidbody2D rb, Vector2 direction)
    {
        if (rb == null) return;
        // キャラの移動
        rb.linearVelocity = new Vector2(direction.x * walkSpeed, rb.linearVelocityY);
        // 入力キーに応じてキャラの向きを変える
        if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        // Y座標調整
        AlignPos(rb);
    }

    private void AlignPos(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustY(ref pos, walkSpeed);
        rb.position = pos;
    }
}
