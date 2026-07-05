using UnityEngine;

public interface IPlayerInput
{
    public Vector2Int moveInput{ get; }
    public bool jumpInput{ get; }

    /// <summary>
    /// プレイヤーの入力を読み取るメソッド
    /// </summary>
    public void ReadInput();
}
