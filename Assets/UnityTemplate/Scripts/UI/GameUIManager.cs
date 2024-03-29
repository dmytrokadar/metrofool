using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Baseline game UI things
/// </summary>
public class GameUIManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("ControlsHint"))
        {
            ControlsManager.instance.ShowOverlay();
        }

        if (Input.GetButtonUp("ControlsHint"))
        {
            ControlsManager.instance.Hide();
        }
    }

    public void RestartLevel()
    {
        ISceneSwitcher.instance.ReloadScene();
        // todo probably some events or smth
    }

    public void LoadLevel(int sceneID)
    {
        ISceneSwitcher.instance.GoToScene(sceneID);
        // todo probably some events or smth
    }

    public void ReturnToMenu()
    {
        ISceneSwitcher.instance.GoToScene(0);
        // todo probably some events or smth
    }
    
}
