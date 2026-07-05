using UnityEngine;

public interface IIdleable
{
    /// <summary>
    /// アイドル状態時の処理を行う
    /// </summary>
    /// <param name="rb">Rigidbody2D コンポーネント</param>
    public void Idle(Rigidbody2D rb);
}
