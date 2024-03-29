using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Prefab contained script. 
/// Singletons as far as the eye can see
/// </summary>
public class SidePanelButtons : MonoBehaviour
{
    public void OnExit()
    {
        MenuManager.instance.OnQuit();
    }

    public void OnSettings()
    {
        ISettingsManager.instance.Show();
    }

    public void OnCredits()
    {
        ICreditsManager.instance.Show();
    }

    public void OnControls()
    {
        ControlsManager.instance.ShowFull();
    }

    public void OnAchievements()
    {
        IAchievementScreen.instance.Show();
    }
}
