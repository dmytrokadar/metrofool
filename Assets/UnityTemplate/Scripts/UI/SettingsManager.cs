using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Encapsulates the settings popup.
/// Saving and loading and SHOWING 
/// </summary>
public abstract class ISettingsManager : MonoBehaviour
{
    public static ISettingsManager instance;

    [SerializeField] private bool anyChange;

    [SerializeField] private GameObject popup;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Instance already exists! Check if the script isn't present multiple times.");

        instance = this;

        popup.SetActive(false);
    }

    private void Start()
    {
        anyChange = false;
    }

    /// <summary>
    /// Update ALL values in the UI
    /// </summary>
    protected abstract void UpdateVisuals();

    /// <summary>
    /// Set the effects of values in memory
    /// </summary>
    protected abstract void Propagate();


    /// <summary>
    /// Commit the UI values to memory
    /// </summary>
    protected abstract void SaveValues();

    /// <summary>
    /// Show the popup and load values from memory to UI
    /// </summary>
    public void Show()
    {
        popup.SetActive(true);

        UpdateVisuals();
    }

    /// <summary>
    /// Exit w/ save + actualization
    /// </summary>
    public void SaveAndQuit()
    {
        Save();   
        popup.SetActive(false);
    }

    /// <summary>
    /// Exit w/o save
    /// </summary>
    public void Cancel()
    {
        if (anyChange)
        {
            Confirmator.instance.Show(() => {
                    popup.SetActive(false);
                    anyChange = false;
                }, 
            SaveAndQuit, 
                "Settings not saved",
                "", 
                "Discard changes", 
                "Save and exit");
        }
        else
        {
            SaveAndQuit();
        }
    }

    /// <summary>
    /// Just save values
    /// </summary>
    protected void Save()
    {
        SaveValues();
        Propagate();
    }

    private void OnEnable()
    {
        UpdateVisuals();
    }

    private void OnDisable()
    {
        Save();
    }

    private void OnDestroy()
    {
        Save();
    }
}

/// <summary>
/// Fills in the actual logic.
/// </summary>
public class SettingsManager : ISettingsManager
{
    protected override void UpdateVisuals()
    {
        Debug.Log("TODO");
    }

    protected override void Propagate()
    {
        Debug.Log("TODO");
    }

    protected override void SaveValues()
    {
        Debug.Log("TODO");
    }
}
