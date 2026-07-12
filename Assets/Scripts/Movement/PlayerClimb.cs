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
        
        if (direction.y != 0) 
        {
            // 上下移動中は左右移動を無効化
            direction.x = 0;
            // 座標調整
            AlignPosX(rb);
        }

        if (direction.x != 0) 
        {
            // 座標調整
            AlignPosY(rb);
            // 入力キーに応じてキャラの向きを変える
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }

        // キャラの移動
        rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);
    }

    /// <summary>
    /// 座標を調整する
    /// </summary>
    /// <param name="rb"></param>
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
