using System;
using UnityEngine;

/// <summary>
/// Defines an interface for all setting managers. 
/// Is a singleton.
/// </summary>
public abstract class ISettings : MonoBehaviour
{
    public static ISettings instance;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("HUD Manager instance already exists! Check if the script isn't present multiple times.");

        instance = this;
    }
    
    /// <summary>
    /// Returns true if there is such a value.
    /// </summary>
    public abstract bool HasValue(string label);
    
    /// <summary>
    /// Get the value of a restart-stable variable.
    /// Default value applies ONLY if the value doesn't exist (e.g. was never set) yet.
    /// </summary>
    public abstract string GetValue(string label, string default_value);
    /// <summary>
    /// Get the value of a restart-stable variable.
    /// Default value applies ONLY if the value doesn't exist (e.g. was never set) yet.
    /// </summary>
    public abstract float GetValue(string label, float default_value);
    /// <summary>
    /// Get the value of a restart-stable variable.
    /// Default value applies ONLY if the value doesn't exist (e.g. was never set) yet.
    /// </summary>
    public abstract int GetValue(string label, int default_value);
    /// <summary>
    /// Get the value of a restart-stable variable.
    /// Default value applies ONLY if the value doesn't exist (e.g. was never set) yet.
    /// </summary>
    public abstract bool GetValue(string label, bool default_value);
    
    /// <summary>
    /// Set the value of a restart-stable variable
    /// Returns true if value was set.
    /// </summary>
    public abstract void SetValue(string label, string default_value);
    /// <summary>
    /// Set the value of a restart-stable variable
    /// Returns true if value was set.
    /// </summary>
    public abstract void SetValue(string label, float default_value);
    /// <summary>
    /// Set the value of a restart-stable variable
    /// Returns true if value was set.
    /// </summary>
    public abstract void SetValue(string label, int default_value);
    /// <summary>
    /// Set the value of a restart-stable variable
    /// Returns true if value was set.
    /// </summary>
    public abstract void SetValue(string label, bool new_value);

    /// <summary>
    /// Delete all saved values. 
    /// </summary>
    public abstract void ResetAll();

}

public class Settings : ISettings
{
    public override bool HasValue(string label)
    {
        return PlayerPrefs.HasKey(label);
    }

    public override string GetValue(string label, string default_value)
    {
        return PlayerPrefs.GetString(label, default_value);
    }

    public override float GetValue(string label, float default_value)
    {
        return PlayerPrefs.GetFloat(label, default_value);
    }

    public override int GetValue(string label, int default_value)
    {
        return PlayerPrefs.GetInt(label, default_value);
    }

    public override bool GetValue(string label, bool default_value)
    {
        return PlayerPrefs.GetInt(label, default_value ? 1 : 0) == 1;
    }

    public override void SetValue(string label, string new_value)
    {
        PlayerPrefs.SetString(label, new_value);
    }

    public override void SetValue(string label, float new_value)
    {
        PlayerPrefs.SetFloat(label, new_value);
    }

    public override void SetValue(string label, int new_value)
    {
        PlayerPrefs.SetInt(label, new_value);
    }

    public override void SetValue(string label, bool new_value)
    {
        PlayerPrefs.SetInt(label, new_value ? 1 : 0);
    }

    public override void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
    
    
}


