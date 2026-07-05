using UnityEngine;

public class PlayerIdle : MonoBehaviour, IIdleable
{
    /// <summary>
    /// アイドル状態時の処理を行う
    /// </summary>
    /// <param name="rb">Rigidbody2D コンポーネント</param>
    public void Idle(Rigidbody2D rb)
    {
        if (rb == null) return;

        // 水平方向の速度をリセット（垂直方向は重力の影響を受けるため保持）
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }
}
