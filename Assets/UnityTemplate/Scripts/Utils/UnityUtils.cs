using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of Unity improvements
/// </summary>
public static class UnityUtils
{
    private static Camera _mainCamera;
    private static int _mcScene = -1;
    
    /// <summary>
    /// Get the current main camera. Updated ONCE per scene switch
    /// </summary>
    public static Camera mainCamera
    {
        get
        {
            // if old scene, OR never set before
            if (_mcScene != ISceneSwitcher.instance.currentSceneID || _mcScene == -1)
            {
                _mainCamera = Camera.main;
                _mcScene = ISceneSwitcher.instance.currentSceneID;
            }
            
            return _mainCamera;
        }
    }

    /// <summary>
    /// Set active if not already. This is to prevent double-setting scripts
    /// </summary>
    /// <param name="go"></param>
    /// <param name="active"></param>
    public static void TrySetActive(this GameObject go, bool active)
    {
        if (go.activeSelf != active)
        {
            go.SetActive(active);
        }
    }
    
    /// <summary>
    /// Where is my pointer? Fear no more.
    /// </summary>
    /// <param name="layerMask"></param>
    /// <returns></returns>
    public static Vector3 GetWorldMousePos(int layerMask = ~0) {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, ~layerMask)) {
            pos = raycastHit.point;
        }

        return pos;
    }

    public const string lower_chars = "abcdefghijklmnopqrstuvwxyz";
    public const string upper_chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public const string numeral_chars = "0123456789";
    public const string special_chars = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
    private static readonly string[] char_options = {lower_chars, upper_chars, numeral_chars, special_chars};

    /// <summary>
    /// Select a random char given probabilities. Probabilities default to 0 = never.
    /// If none are specified, all are equal.
    /// </summary>
    /// <param name="lower">the relative chance to select a lowercase letter</param>
    /// <param name="upper">...uppercase letter</param>
    /// <param name="numerals">...numeral</param>
    /// <param name="specials">...special ascii char</param>
    public static char RandomChar(float lower = 0.0f, float upper = 0.0f, float numerals = 0.0f, float specials = 0.0f)
    {
        float[] chances = {lower, upper, numerals, specials};
        return char_options.WeightedRandomChoice(chances, true).ChooseRandom();
    }

    /// <summary>
    /// Like RandomChar, but returns a string of given length
    /// </summary>
    public static string RandomString(int length, float lower = 0.0f, float upper = 0.0f, float numerals = 0.0f,
        float specials = 0.0f)
    {
        float[] chances = {lower, upper, numerals, specials};
        string s = "";

        for (int i = 0; i < length; i++) 
            s += char_options.WeightedRandomChoice(chances, true).ChooseRandom();

        return s;
    }


    /// <summary>
    /// Set a height for a recttransform
    /// </summary>
    public static void SetHeight(this RectTransform rt, float height)
    {
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);
    }

    /// <summary>
    /// Set a width for a recttransform
    /// </summary>
    public static void SetWidth(this RectTransform rt, float width)
    {
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
    }
    
    
    
}