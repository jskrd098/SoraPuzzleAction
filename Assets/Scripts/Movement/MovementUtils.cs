using UnityEngine;

public static class MovementUtils
{
    private static Vector2 tgtPos; // 目的地となる座標

    /// <summary>
    /// X座標を四捨五入で調整する
    /// </summary>
    /// <param name="fwdPos"></param>
    /// <param name="speed"></param>
    public static void PosAdjustXRound(ref Vector2 fwdPos, float speed)
    {
        tgtPos = new Vector2(Mathf.Round(fwdPos.x), fwdPos.y);
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// X座標を切り上げで調整する
    /// </summary>
    /// <param name="fwdPos"></param>
    /// <param name="speed"></param>
    public static void PosAdjustXCeil(ref Vector2 fwdPos, float speed)
    {
        tgtPos = new Vector2(Mathf.Ceil(fwdPos.x), fwdPos.y);
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// X座標を切り捨てで調整する
    /// </summary>
    /// <param name="fwdPos"></param>
    /// <param name="speed"></param>
    public static void PosAdjustXFloor(ref Vector2 fwdPos, float speed)
    {
        tgtPos = new Vector2(Mathf.Floor(fwdPos.x), fwdPos.y);
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// Y座標を四捨五入で調整する
    /// </summary>
    /// <param name="fwdPos"></param>
    /// <param name="speed"></param>
    public static void PosAdjustYRound(ref Vector2 fwdPos, float speed)
    {
        tgtPos = new Vector2(fwdPos.x, Mathf.Round(fwdPos.y));
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// Y座標を切り上げで調整する
    /// </summary>
    /// <param name="fwdPos"></param>
    /// <param name="speed"></param>
    public static void PosAdjustYCeil(ref Vector2 fwdPos, float speed)
    {
        tgtPos = new Vector2(fwdPos.x, Mathf.Ceil(fwdPos.y));
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }

    /// <summary>
    /// Y座標を切り捨てで調整する
    /// </summary>
    /// <param name="fwdPos"></param>
    /// <param name="speed"></param>
    public static void PosAdjustYFloor(ref Vector2 fwdPos, float speed)
    {
        tgtPos = new Vector2(fwdPos.x, Mathf.Floor(fwdPos.y));
        fwdPos = Vector2.MoveTowards(fwdPos, tgtPos, speed * Time.deltaTime);
    }
}