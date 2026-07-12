using UnityEngine;

public interface IPlayerInput
{
    public Vector2Int moveInput{ get; }
    public bool jumpInput{ get; }

    /// <summary>
    /// 入力値を外部から強制的に設定する
    /// </summary>
    public void SetMoveInput(Vector2Int input);

    /// <summary>
    /// プレイヤーの入力を読み取るメソッド
    /// </summary>
    public void ReadInput();
}
