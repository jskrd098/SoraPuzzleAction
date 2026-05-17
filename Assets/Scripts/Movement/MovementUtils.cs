using UnityEngine;

public static class MovementUtils
{
    //// Rigidbody2D の位置を参照して X 軸を最も近い整数に丸める目標まで
    //// 指定速度で FixedUpdate 毎に移動させる拡張メソッド
    //public static void PosAdjustToNearestX(this Rigidbody2D rb, float speed)
    //{
    //    // 例外処理
    //    if (rb == null) return;

    //    // 目標となる X 座標を計算（現在の X 座標を最も近い整数に丸める）
    //    float targetX = Mathf.Round(rb.position.x);

    //    // すでに目標位置に近い場合は移動しない
    //    if (Mathf.Approximately(rb.position.x, targetX)) return;

    //    // 目標位置に向かって移動するための方向と距離を計算
    //    float direction = Mathf.Sign(targetX - rb.position.x);
    //    float delta = speed * Time.fixedDeltaTime * direction;
    //    float nextX = rb.position.x + delta;

    //    // 目標位置を超えないように調整
    //    if ((direction > 0f && nextX > targetX) || (direction < 0f && nextX < targetX))
    //    {
    //        nextX = targetX;
    //    }

    //    // 次の位置を計算して Rigidbody2D を移動
    //    Vector2 nextPos = new Vector2(nextX, rb.position.y);
    //    rb.MovePosition(nextPos);
    //}

    //// Rigidbody2D の位置とキャラの向きを参照して X 座標を進行方向の最も近い
    //// 整数に丸める目標まで指定速度で FixedUpdate 毎に移動させる拡張メソッド
    //public static void PosAdjustToNextX(this Rigidbody2D rb, float speed)
    //{
    //    // 例外処理
    //    if (rb == null) return;

    //    // 目標となる X 座標を計算（現在の X 座標を進行方向の最も近い整数に丸める）
    //    float direction = Mathf.Sign(rb.linearVelocity.x);
    //    float targetX = Mathf.Round(rb.position.x + direction * 0.5f);

    //    // すでに目標位置に近い場合は移動しない
    //    if (Mathf.Approximately(rb.position.x, targetX)) return;

    //    // 目標位置に向かって移動するための方向と距離を計算
    //    float delta = speed * Time.fixedDeltaTime * direction;
    //    float nextX = rb.position.x + delta;

    //    // 目標位置を超えないように調整
    //    if ((direction > 0f && nextX > targetX) || (direction < 0f && nextX < targetX))
    //    {
    //        nextX = targetX;
    //    }

    //    // 次の位置を計算して Rigidbody2D を移動
    //    Vector2 nextPos = new Vector2(nextX, rb.position.y);
    //    rb.MovePosition(nextPos);
    //}

    //// Rigidbody2D の位置を参照して Y 軸を最も近い整数に丸める目標まで
    //// 指定速度で FixedUpdate 毎に移動させる拡張メソッド
    //public static void PosAdjustToNearestY(this Rigidbody2D rb, float speed)
    //{
    //    // 例外処理
    //    if (rb == null) return;

    //    // 目標となる Y 座標を計算（現在の Y 座標を最も近い整数に丸める）
    //    float targetY = Mathf.Round(rb.position.y);

    //    // すでに目標位置に近い場合は移動しない
    //    if (Mathf.Approximately(rb.position.y, targetY)) return;

    //    // 目標位置に向かって移動するための方向と距離を計算
    //    float direction = Mathf.Sign(targetY - rb.position.y);
    //    float delta = speed * Time.fixedDeltaTime * direction;
    //    float nextY = rb.position.y + delta;

    //    // 目標位置を超えないように調整
    //    if ((direction > 0f && nextY > targetY) || (direction < 0f && nextY < targetY))
    //    {
    //        nextY = targetY;
    //    }

    //    // 次の位置を計算して Rigidbody2D を移動
    //    Vector2 nextPos = new Vector2(rb.position.x, nextY);
    //    rb.MovePosition(nextPos);
    //}


    // 水平移動から垂直移動へ切り替わる際に X 座標を「キャラの向いている方向」に沿って整数へスナップしながら移動する
    // facingDirection: -1（左） / +1（右） / 0（不明→最も近い整数）
    public static void PosAdjustToNearestXByFacing(this Rigidbody2D rb, float speed, float facingDirection)
    {
        if (rb == null) return;

        float sign = Mathf.Sign(facingDirection);
        float targetX;

        if (Mathf.Approximately(sign, 0f))
        {
            // 向き不明なら最も近い整数へ
            targetX = Mathf.Round(rb.position.x);
        }
        else
        {
            // 向きに沿った方向の整数（右向きなら ceil、左向きなら floor）
            targetX = sign > 0f ? Mathf.Ceil(rb.position.x) : Mathf.Floor(rb.position.x);
        }

        if (Mathf.Approximately(rb.position.x, targetX)) return;

        float direction = Mathf.Sign(targetX - rb.position.x);
        float delta = speed * Time.fixedDeltaTime * direction;
        float nextX = rb.position.x + delta;

        if ((direction > 0f && nextX > targetX) || (direction < 0f && nextX < targetX))
        {
            nextX = targetX;
        }

        Vector2 nextPos = new Vector2(nextX, rb.position.y);
        rb.MovePosition(nextPos);
    }

    // 垂直移動から水平移動へ切り替わる際に Y 座標を「キャラの向いている方向」に沿って整数へスナップしながら移動する
    // facingDirection: -1（下） / +1（上） / 0（不明→最も近い整数）
    public static void PosAdjustToNearestYByFacing(this Rigidbody2D rb, float speed, float facingDirection)
    {
        if (rb == null) return;

        float sign = Mathf.Sign(facingDirection);
        float targetY;

        if (Mathf.Approximately(sign, 0f))
        {
            targetY = Mathf.Round(rb.position.y);
        }
        else
        {
            // 上向きなら ceil、下向きなら floor
            targetY = sign > 0f ? Mathf.Ceil(rb.position.y) : Mathf.Floor(rb.position.y);
        }

        if (Mathf.Approximately(rb.position.y, targetY)) return;

        float direction = Mathf.Sign(targetY - rb.position.y);
        float delta = speed * Time.fixedDeltaTime * direction;
        float nextY = rb.position.y + delta;

        if ((direction > 0f && nextY > targetY) || (direction < 0f && nextY < targetY))
        {
            nextY = targetY;
        }

        Vector2 nextPos = new Vector2(rb.position.x, nextY);
        rb.MovePosition(nextPos);
    }
}