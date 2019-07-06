using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CoordsUtils
{
    private static float COS_60 = Mathf.Cos(60f * Mathf.Deg2Rad);
    private static float SIN_60 = Mathf.Sin(60f * Mathf.Deg2Rad);
    public static Vector2 SlopeToOrtho(Vector2 coords)
    {
        var y = coords.y / SIN_60;
        var x = coords.x - COS_60 * y;
        return new Vector2(x, y);
    }

    public static Vector2 OrthoToSlope(Vector2 coords)
    {
        return new Vector2(coords.x + COS_60 * coords.y, coords.y * SIN_60);
    }
}
