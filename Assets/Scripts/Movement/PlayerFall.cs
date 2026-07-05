using UnityEngine;

public class PlayerFall : MonoBehaviour, IFallable
{
    [SerializeField] private float _fallSpeed = 6f;
    public float fallSpeed => _fallSpeed;
    
    public void Fall(Rigidbody2D rb)
    {
        if (rb == null) return;
        // キャラの移動
        rb.linearVelocity = new Vector2(0.0f, -_fallSpeed);
        // X座標調整
        AlignPos(rb);
    }

    private void AlignPos(Rigidbody2D rb)
    {
        Vector2 pos = rb.position;
        MovementUtils.PosAdjustX(ref pos, fallSpeed);
        rb.position = pos;
    }
}