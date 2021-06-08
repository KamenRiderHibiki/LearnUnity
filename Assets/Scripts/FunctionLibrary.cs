using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.Mathf;

public static class FunctionLibrary
{
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
        return Abs(x);
    }
}
