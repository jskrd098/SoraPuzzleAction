using UnityEngine;

public class PlayerIdle : MonoBehaviour, IIdleable
{
    /// <summary>
    /// Playerが停止時の処理を行う
    /// </summary>
    /// <param name="rb">Rigidbody2D コンポーネント</param>
    public void Idle(Rigidbody2D rb)
    {
        if (rb == null) return;

        // 水平方向の速度をリセット
        rb.linearVelocity = new Vector2(0, 0);
    }
}
