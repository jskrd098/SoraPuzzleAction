using UnityEngine;

public static class MovementUtils
{
    // Rigidbody2D の位置を参照して X 軸を最も近い整数に丸める目標まで
    // 指定速度で FixedUpdate 毎に移動させる拡張メソッド
    // 呼び出しは FixedUpdate (または FixedUpdate 内で呼び出されるメソッド)から行ってください。
    public static void PosAdjustToNearestX(this Rigidbody2D rb, float speed)
    {
        // 例外処理
        if (rb == null) return;

        // 目標となる X 座標を計算（現在の X 座標を最も近い整数に丸める）
        float targetX = Mathf.Round(rb.position.x);

        // すでに目標位置に近い場合は移動しない
        if (Mathf.Approximately(rb.position.x, targetX)) return;

        // 目標位置に向かって移動するための方向と距離を計算
        float direction = Mathf.Sign(targetX - rb.position.x);
        float delta = speed * Time.fixedDeltaTime * direction;
        float nextX = rb.position.x + delta;

        // 目標位置を超えないように調整
        if ((direction > 0f && nextX > targetX) || (direction < 0f && nextX < targetX))
        {
            nextX = targetX;
        }

        // 次の位置を計算して Rigidbody2D を移動
        Vector2 nextPos = new Vector2(nextX, rb.position.y);
        rb.MovePosition(nextPos);
    }

    // Rigidbody2D の位置を参照して Y 軸を最も近い整数に丸める目標まで
    // 指定速度で FixedUpdate 毎に移動させる拡張メソッド
    public static void PosAdjustToNearestY(this Rigidbody2D rb, float speed)
    {
        // 例外処理
        if (rb == null) return;

        // 目標となる Y 座標を計算（現在の Y 座標を最も近い整数に丸める）
        float targetY = Mathf.Round(rb.position.y);

        // すでに目標位置に近い場合は移動しない
        if (Mathf.Approximately(rb.position.y, targetY)) return;

        // 目標位置に向かって移動するための方向と距離を計算
        float direction = Mathf.Sign(targetY - rb.position.y);
        float delta = speed * Time.fixedDeltaTime * direction;
        float nextY = rb.position.y + delta;

        // 目標位置を超えないように調整
        if ((direction > 0f && nextY > targetY) || (direction < 0f && nextY < targetY))
        {
            nextY = targetY;
        }
        
        // 次の位置を計算して Rigidbody2D を移動
        Vector2 nextPos = new Vector2(rb.position.x, nextY);
        rb.MovePosition(nextPos);
    }
}