using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A collection of extension methods for better Image and Color manipulation
/// </summary>
public static class ColorUtils
{
        
    [Pure]
    public static float GetA(this Image image)
    {
        return image.color.a;
    }
        
    [Pure]
    public static float GetR(this Image image)
    {
        return image.color.r;
    }
        
    [Pure]
    public static float GetG(this Image image)
    {
        return image.color.g;
    }
        
    [Pure]
    public static float GetB(this Image image)
    {
        return image.color.b;
    }

    public static void SetA(this Image image, float a)
    {
        Color creditsColor = image.color;
        creditsColor.a = a.Clamp01();
        image.color = creditsColor;
    }

    public static void SetA(this Image image, bool a)
    {
        image.SetA(a ? 1 : 0);
    }
    
    public static void SetR(this Image image, float r)
    {
        Color creditsColor = image.color;
        creditsColor.r = r.Clamp01();;
        image.color = creditsColor;
    }
    
    public static void SetG(this Image image, float g)
    {
        Color creditsColor = image.color;
        creditsColor.g = g.Clamp01();;
        image.color = creditsColor;
    }
    
    public static void SetB(this Image image, float b)
    {
        Color creditsColor = image.color;
        creditsColor.b = b.Clamp01();;
        image.color = creditsColor;
    }

    public static void IncrementA(this Image image, float a_diff)
    {
        Color creditsColor = image.color;
        creditsColor.a = (creditsColor.a + a_diff).Clamp01();;
        image.color = creditsColor;
    }

    public static void IncrementR(this Image image, float r_diff)
    {
        Color creditsColor = image.color;
        creditsColor.r = (creditsColor.r + r_diff).Clamp01();;
        image.color = creditsColor;
    }

    public static void IncrementG(this Image image, float g_diff)
    {
        Color creditsColor = image.color;
        creditsColor.g = (creditsColor.g + g_diff).Clamp01();;
        image.color = creditsColor;
    }

    public static void IncrementB(this Image image, float b_diff)
    {
        Color creditsColor = image.color;
        creditsColor.b = (creditsColor.b + b_diff).Clamp01();;
        image.color = creditsColor;
    }
    
    public static void SetGray(this Image image, float gray)
    {
        float grayVal = gray.Clamp01();
            
        Color creditsColor = image.color;
        creditsColor.r = grayVal;
        creditsColor.g = grayVal;
        creditsColor.b = grayVal;
        image.color = creditsColor;
    }

    /// <summary>
    /// Calculate gray value from image<br />
    /// Simple 21-72-7 % 
    /// </summary>
    [Pure]
    public static float GetGray(this Image image)
    {
        return .2126f * image.GetR() + 
               .7152f * image.GetG() +
               .0722f * image.GetB();
    }

 
    [Pure]
    public static float GetA(this Color color)
    {
        return color.a;
    }
        
    [Pure]
    public static float GetR(this Color color)
    {
        return color.r;
    }
        
    [Pure]
    public static float GetG(this Color color)
    {
        return color.g;
    }
        
    [Pure]
    public static float GetB(this Color color)
    {
        return color.b;
    }

    public static void SetA(this ref Color color, float a)
    {
        color.a = a.Clamp01();
    }
    
    public static void SetR(this ref Color color, float r)
    {
        color.r = r.Clamp01();
    }
    
    public static void SetG(this ref Color color, float g)
    {
        color.g = g.Clamp01();
    }
    
    public static void SetB(this ref Color color, float b)
    {
        color.b = b.Clamp01();
    }
    
    public static void SetGray(this ref Color color, float gray)
    {
        float grayVal = gray.Clamp01();
        color.r = grayVal;
        color.g = grayVal;
        color.b = grayVal;
    }

    /// <summary>
    /// Calculate gray value from image<br />
    /// Simple 21-72-7 % 
    /// </summary>
    [Pure]
    public static float GetGray(this Color color)
    {
        return .2126f * color.GetR() + 
               .7152f * color.GetG() +
               .0722f * color.GetB();
    }
        
    /// <summary>
    /// I didnt like how all the colors got a static preinitialized value but this bad boy didn't :((
    /// </summary>
    public static readonly Color transparent = new Color(0, 0, 0, 0);
}