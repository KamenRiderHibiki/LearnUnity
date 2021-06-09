using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public static float control = 0f;

    public delegate float Function(float x, float t);

    public enum FunctionNames
    {
        Wave, MultiWave, Ripple, Mixed
    }

    public static Function[] Functions = { Wave, MultiWave, Ripple, Mixed};

    public static Function GetFunction(FunctionNames name)
    {
        //int floorControl = (int)Floor(control);
        return Functions[(int)name];
    }
    // 静态类不能继承自 MonoBehaviour
    // 静态类不能声明 Start Update 方法

    // 正弦波
    public static float Wave(float x, float t)
    {
        return Sin(PI * (x + t));
    }

    // 多正弦波
    public static float MultiWave(float x, float t)
    {
        float y = Sin(PI * (x + 0.5f * t));
        y += Sin(2 * PI * (x + t)) * 0.5f;
        //乘法优于除法,且编译器会将常量表达式（例如1f / 2f以及2f * Mathf.PI）简化为单个数字
        return y * (2f / 3f);
    }

    public static float Ripple(float x, float t)
    {
        float d = Abs(x);
        float y = Sin(4f * PI * (d - t * 0.1f));
        return y / (1f + d * 10f);
    }

    // get mixed Y value by slide block
    public static float Mixed(float x ,float time)
    {
        int floorControl = (int)Floor(control);
        float conbineRate = control - (float)floorControl;
        float y = 0;
        switch (floorControl)
        {
            case 0:
                y = Sin(Wave(x, time)) * (1.0f - conbineRate) + Sin(MultiWave(x, time)) * conbineRate;
                break;
            case 1:
                y = Sin(MultiWave(x, time)) * (1.0f - conbineRate) + Sin(Ripple(x, time)) * conbineRate;
                break;
            default: // 2
                y = Sin(Ripple(x, time));
                break;
        }
        return y;
    }
}
