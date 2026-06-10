using UnityEngine;

public interface IMovementDirectionResolver
{
    Vector2Int ResolveDirection(PlayerController player, Vector2 input);
}
