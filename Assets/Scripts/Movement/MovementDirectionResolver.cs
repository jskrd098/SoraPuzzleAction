using UnityEngine;

public class MovementDirectionResolver : IMovementDirectionResolver
{
    /// <summary>
    /// プレイヤーの入力方向を解決する
    /// </summary>
    /// <param name="player"></param>
    /// <param name="input"></param>
    /// <returns> 解決された方向 </returns>
    public Vector2Int ResolveDirection(PlayerController player, Vector2 input)
    {
        int x = Mathf.RoundToInt(input.x);
        int y = Mathf.RoundToInt(input.y);

        if (x == 0 || y == 0)
        {
            return new Vector2Int(x, y);
        }

        if (player._playerSensor != null && player._playerSensor.IsInLadder())
        {
            return new Vector2Int(0, y);
        }

        if (player._playerSensor != null && player._playerSensor.IsGrounded())
        {
            return new Vector2Int(x, 0);
        }

        if (CanMoveVertical(player, y))
        {
            return new Vector2Int(0, y);
        }

        if (CanMoveHorizontal(player, x))
        {
            return new Vector2Int(x, 0);
        }

        return Vector2Int.zero;
    }

    private bool CanMoveVertical(PlayerController player, int dir)
    {
        return player._playerSensor.CanMove(Vector2Int.up * dir);
    }

    private bool CanMoveHorizontal(PlayerController player, int dir)
    {
        return player._playerSensor.CanMove(Vector2Int.right * dir);
    }
}

