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
            // direction.x = 0;
            // 座標調整
            Debug.Log($"direction={direction}");
            AlignPosX(rb, direction);
        }

        if (direction.x != 0) 
        {
            // 座標調整
            AlignPosY(rb, direction);
            // 入力キーに応じてキャラの向きを変える
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }

        // キャラの移動
        rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);
    }

    /// <summary>
    /// X座標を調整する
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="direction"></param>
    public void AlignPosX(Rigidbody2D rb, Vector2 direction)
    {
        Vector2 pos = rb.position;
        if (direction.x > 0)
        {
            Debug.Log($"AlignPosXCeil: direction.x > 0, pos={pos}");
            MovementUtils.PosAdjustXCeil(ref pos, ClimbSpeed);
        }
        else if (direction.x < 0)
        {
            Debug.Log($"AlignPosXFloor: direction.x < 0, pos={pos}");
            MovementUtils.PosAdjustXFloor(ref pos, ClimbSpeed);
        }
        else
        {
            MovementUtils.PosAdjustXRound(ref pos, ClimbSpeed);
        }
        rb.position = pos;
    }

    /// <summary>
    /// Y座標を調整する
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="direction"></param>
    public void AlignPosY(Rigidbody2D rb, Vector2 direction)
    {
        Vector2 pos = rb.position;
        if (direction.y > 0)
        {
            MovementUtils.PosAdjustYCeil(ref pos, ClimbSpeed);
        }
        else if (direction.y < 0)
        {
            MovementUtils.PosAdjustYFloor(ref pos, ClimbSpeed);
        }
        else
        {
            MovementUtils.PosAdjustYRound(ref pos, ClimbSpeed);
        }
        rb.position = pos;
    }
}
