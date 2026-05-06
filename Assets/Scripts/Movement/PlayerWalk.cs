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
        _rb.PosAdjustToNearestY(_walkSpeed);
    }

    //public void PosAdjust(Rigidbody2D _rb)
    //{
    //    // 現在の位置を取得
    //    Vector2 prevPos = _rb.position;

    //    // 移動後の位置を取得
    //    Vector2 nextPos = new Vector2(prevPos.x, (float)Mathf.Round(prevPos.y));

    //    // Y座標を整数に丸める
    //    transform.position = nextPos;
    //}
}
