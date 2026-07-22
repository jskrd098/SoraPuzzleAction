using UnityEngine;

public class PlayerFall : MonoBehaviour, IFallable
{
    [SerializeField] private float _fallSpeed = 6f;
    public float fallSpeed => _fallSpeed;
    
    /// <summary>
    /// Playerの落下
    /// </summary>
    /// <param name="rb"></param>
    public void Fall(Rigidbody2D rb)
    {
        if (rb == null) return;
        // キャラの移動
        rb.linearVelocity = new Vector2(0.0f, -_fallSpeed);
        // X座標調整
        AlignPos(rb);
    }

    /// <summary>
    /// 座標を調整する
    /// </summary>
    /// <param name="rb"></param>
    private void AlignPos(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustXRound(ref pos, fallSpeed);
        rb.position = pos;
    }
}