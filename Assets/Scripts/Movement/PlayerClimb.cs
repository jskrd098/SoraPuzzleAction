using UnityEngine;

public class PlayerClimb : MonoBehaviour, IClimbable
{
    [SerializeField] private float _climbSpeed = 5f;
    public float ClimbSpeed => _climbSpeed;

    /// <summary>
    /// Playerの梯子移動
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="direction"></param>
    public void Climb(Rigidbody2D rb, Vector2 direction)
    {
        if (rb == null) return;
        // 上下移動中は左右移動を無効化
        if (direction.y != 0) direction.x = 0;
        // キャラの移動
        rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);
        // 入力キーに応じてキャラの向きを変える
        // if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        // 座標調整
        AlignPos(rb);
    }

    /// <summary>
    /// 座標を調整する
    /// </summary>
    /// <param name="rb"></param>
    public void AlignPos(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustX(ref pos, ClimbSpeed);
        MovementUtils.PosAdjustY(ref pos, ClimbSpeed);
        rb.position = pos;
    }
}
