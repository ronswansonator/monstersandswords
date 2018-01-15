using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public struct Transform_RTS
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 scale;
    }

    public static Transform_RTS ExtractRTS(this Transform me)
    {
        Transform_RTS rts;
        rts.pos = me.position;
        rts.rot = me.rotation;
        rts.scale = me.localScale;
        return rts;
    }

    public static void SetFromTransformRTS(this Transform me, Transform_RTS rts)
    {
        me.position = rts.pos;
        me.rotation = rts.rot;
        me.localScale = rts.scale;
    }
}
