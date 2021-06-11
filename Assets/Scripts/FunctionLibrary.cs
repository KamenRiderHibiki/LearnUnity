using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public static float control = 0f;

    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionNames
    {
        Wave, Wave2D, MultiWave, MultiWave2D, Ripple, Ripple2D, Mixed
    }

    public static Function[] Functions = { Wave, Wave2D, MultiWave, MultiWave2D, Ripple, Ripple2D, Mixed};

    public static Function GetFunction(FunctionNames name)
    {
        //int floorControl = (int)Floor(control);
        return Functions[(int)name];
    }
    // 静态类不能继承自 MonoBehaviour
    // 静态类不能声明 Start Update 方法

    // 正弦波
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + t));
        p.z = v;
        return p;
    }

    public static Vector3 Wave2D(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + t)) * Sin(PI * (v + t)) * 0.5f;
        p.z = v;
        return p;
    }

    // 多正弦波
    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        float y = Sin(PI * (u + 0.5f * t));
        y += Sin(2 * PI * (u + t)) * 0.5f;
        //乘法优于除法,且编译器会将常量表达式（例如1f / 2f以及2f * Mathf.PI）简化为单个数字
        p.y = y * (2f / 3f);
        p.z = v;
        return p;
    }

    // 多正弦波
    public static Vector3 MultiWave2D(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        float y = Sin(PI * (u + v + 0.5f * t)) * 4f;
        y += Sin(2 * PI * (v + 2f * t)) * 0.5f;
        y +=Sin(PI * (u + t));
        p.y = y * (2f / 11f);
        p.z = v;
        return p;
    }

    //多重波
    public static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        float d = Abs(u);
        float y = Sin(4f * PI * (d - t * 0.1f));
        p.y = y / (1f + d * 10f);
        p.z = v;
        return p;
    }

    //多重波2D
    public static Vector3 Ripple2D(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        float d = Sqrt(u * u + v * v);
        float y = Sin(PI * (4f * d - t));
        p.y = y / (1f + d * 10f);
        p.z = v;
        return p;
    }

    // get mixed Y value by slide block
    public static Vector3 Mixed(float u , float v, float time)
    {
        Vector3 p;
        int floorControl = (int)Floor(control);
        float conbineRate = control - (float)floorControl;
        float y = 0;
        switch (floorControl)
        {
            case 0:
                y = Sin(Wave(u, v, time)) * (1.0f - conbineRate) + Sin(MultiWave(u, v, time)) * conbineRate;
                break;
            case 1:
                y = Sin(MultiWave(u, v, time)) * (1.0f - conbineRate) + Sin(Ripple(u, v, time)) * conbineRate;
                break;
            default: // 2
                y = Sin(Ripple(u, v, time));
                break;
        }
        return y;
    }
}
