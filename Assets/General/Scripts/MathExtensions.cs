using UnityEngine;

public static class MathExtensions
{
    public static Vector3 FlatY(this Vector3 v)
    {
        v.y = 0f;
        return v;
    }

    public static float EasingCurve_InOutSine(this float _time)
    {
        return -(Mathf.Cos(Mathf.PI * _time) - 1f) * .5f;
    }
}
