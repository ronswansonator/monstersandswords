using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        return dir + pivot;
    }

    public static Vector3 Clamp(this Vector3 clamp, Vector3 min, Vector3 max)
    {
        clamp.x = Mathf.Clamp(clamp.x, min.x, max.x);
        clamp.y = Mathf.Clamp(clamp.y, min.y, max.y);
        clamp.z = Mathf.Clamp(clamp.z, min.z, max.z);
        return clamp;
    }

    public static Vector2 ToVec2XZ(this Vector3 src)
    {
        return new Vector2(src.x, src.z);
    }

    public static Vector2 ToVec2XY(this Vector3 src)
    {
        return new Vector2(src.x, src.y);
    }

    public static Vector2 ToVec2YZ(this Vector3 src)
    {
        return new Vector2(src.y, src.z);
    }

    public static Vector3 ToVec3XZ(this Vector2 src, float y = 0)
    {
        return new Vector3(src.x, y, src.y);
    }

    public static Vector3 ToVec3XY(this Vector2 src, float z = 0)
    {
        return new Vector3(src.x, src.y, z);
    }

    public static Vector3 ToVec3YZ(this Vector2 src, float x = 0)
    {
        return new Vector3(x, src.x, src.y);
    }
}
