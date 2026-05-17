using UnityEngine;

public class PlayerClimb : MonoBehaviour, IClimbable
{
    [SerializeField] private float _climbSpeed = 5f;
    public float ClimbSpeed => _climbSpeed;

    public void Climb(Rigidbody2D _rb, Vector2 direction)
    {
        // 梯子での移動(斜め入力時は上下方向を優先)
        if (direction.y != 0) direction.x = 0; // 横入力を無効化
        _rb.linearVelocity = new Vector2(direction.x * _climbSpeed, direction.y * _climbSpeed);

        // 入力方向に応じてキャラクターの向きを変える
        if (direction.x != 0) transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);

        // 垂直移動（上下）がある場合は X を整数へ向かってスナップ
        if (direction.y != 0)
        {
            _rb.PosAdjustToNearestXByFacing(_climbSpeed, direction.x);
        }
        //// 垂直移動がなく水平移動のみの場合は Y を整数へ向かってスナップ
        //else if (direction.x != 0)
        //{
        //    _rb.PosAdjustToNearestY(_climbSpeed);
        //}
    }
}
