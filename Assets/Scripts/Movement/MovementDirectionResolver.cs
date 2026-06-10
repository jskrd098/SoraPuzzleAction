using UnityEngine;

public class MovementDirectionResolver : IMovementDirectionResolver
{
    public Vector2Int ResolveDirection(PlayerController player, Vector2 input)
    {
        int x = Mathf.RoundToInt(input.x);
        int y = Mathf.RoundToInt(input.y);
        // 単軸入力
        if (x == 0 || y == 0) return new Vector2Int(x, y);
        // 斜め入力時
        if (CanMoveVertical(player, y))        return new Vector2Int(0, y);
        else if (CanMoveHorizontal(player, x)) return new Vector2Int(x, 0);
        return Vector2Int.zero;
    }

    private bool CanMoveVertical(PlayerController player, int dir)
    {
        return player._movementSensor.CanMove(Vector2Int.up * dir);
    }

    private bool CanMoveHorizontal(PlayerController player, int dir)
    {
        return player._movementSensor.CanMove(Vector2Int.right * dir);
    }
}

