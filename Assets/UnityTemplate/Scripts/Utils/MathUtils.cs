using System;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// A collection of extension methods for better maths
/// </summary>
public static class MathUtils
{

    /// <summary>
    /// Modulo but returns a positive result for negative numbers.
    /// </summary>
    public static void Mod(this ref int value, int modulo)
    {
        value = (value % modulo + modulo) % modulo;
    }
    
    /// <summary>
    /// Modulo but returns a positive result for negative numbers.
    /// </summary>
    public static void Mod(this ref float value, float modulo)
    {
        value = (value % modulo + modulo) % modulo;
    }
        
    /// <summary>
    /// Rounds a float to the nearest integer, up or down, but as an extension method.
    /// </summary>
    [Pure]
    public static int Round(this float value)
    {
        return Mathf.RoundToInt(value);
    }
        
        
    /// <summary>
    /// Check if float is NaN
    /// </summary>
    [Pure]
    public static bool IsNaN(this float value)
    {
        return float.IsNaN(value);
    }

    /// <summary>
    /// Check if float is NaN
    /// </summary>
    [Pure]
    public static bool IsNaN(this double value)
    {
        return double.IsNaN(value);
    }

    /// <summary>
    /// Clamp a value within a given range
    /// </summary>
    /// <param name="value">A numerical value</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <typeparam name="T">Any IComparable type</typeparam>
    /// <returns>Value between min and max</returns>
    [Pure]
    public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0)
            return min;
            
        if (value.CompareTo(max) > 0)
            return max;
            
        return value;
    }
    
    public static float Clamp01(this float value)
    {
        return value.Clamp(0, 1);
    }
    
    public static double Clamp01(this double value)
    {
        return value.Clamp(0, 1);
    }

    /// <summary>
    ///   <para>Linearly interpolates between a and b by t.</para>
    /// </summary>
    /// <param name="a">The start value.</param>
    /// <param name="b">The end value.</param>
    /// <param name="value">The interpolation value between the two floats.</param>
    /// <returns>
    ///   <para>The interpolated float result <b>between the two float values.</b></para>
    /// </returns>
    [Pure]
    public static float Lerp(this float value, float a, float b)
    {
        return Mathf.Lerp(value, a, b);
    }

    /// <summary>
    ///   <para>Linearly interpolates between a and b by t with no limit to t.</para>
    /// </summary>
    /// <param name="a">The start value.</param>
    /// <param name="b">The end value.</param>
    /// <param name="value">The interpolation between the two floats.</param>
    /// <returns>
    ///   <para>The float value as a result from the linear interpolation.</para>
    /// </returns>
    [Pure]
    public static float LerpUnclamped(this float value, float a, float b)
    {
        return Mathf.LerpUnclamped(value, a, b);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="maxIndex"></param>
    /// <param name="minValue"></param>
    /// <param name="maxValue"></param>
    /// <returns></returns>
    public static float IndexLerp(int index, int maxIndex, float minValue, float maxValue)
    {
        return Mathf.Lerp(minValue, maxValue, index / (maxIndex - 1f));
    }

    /// <summary>
    /// Finds a point (x,y) on a line defined as passing through (x1,y1) and (x2, y2).
    /// Returns y given any x.
    /// </summary>
    [Pure]
    public static float LerpOnLine(this float x, float x1, float y1, float x2, float y2)
    {
        return Mathf.LerpUnclamped(y1, y2, (x - x1) / (x2 - x1));
    }

}