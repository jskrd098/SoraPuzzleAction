using UnityEngine;

public class PlayerFall : MonoBehaviour, IFallable
{
    [SerializeField] private float _fallSpeed = 6f;
    public float FallSpeed => _fallSpeed;
    
    public void Fall(Rigidbody2D _rb)
    {
        // 落下（垂直移動）
        _rb.linearVelocity = new Vector2(0.0f, -_fallSpeed);

        // 垂直移動時は X を整数へ向かってスナップ
        _rb.PosAdjustToNearestXByFacing(_fallSpeed, -1f);
    }
}