using UnityEngine;

public static class MovementUtils
{
    private static Vector2 tgtPos; // 目的地となる座標

    public static void PosAdjustX(ref Vector2 fwdPos, float speed)
    {
        Debug.Log($"PosAdjustX: fwdPos={fwdPos}, speed={speed}");
        tgtPos = new Vector2(Mathf.Round(fwdPos.x), fwdPos.y);
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }

    public static void PosAdjustY(ref Vector2 fwdPos, float speed)
    {
        Debug.Log($"PosAdjustY: fwdPos={fwdPos}, speed={speed}");
        tgtPos = new Vector2(fwdPos.x, Mathf.Round(fwdPos.y));
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }
}