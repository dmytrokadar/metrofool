using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Encapsulates switching between scenes, returning to menu, or reloading a scene.
/// </summary>
public abstract class ISceneSwitcher : MonoBehaviour
{
    public static ISceneSwitcher instance;

    public int currentSceneID
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");

        instance = this;
        currentSceneID = 0;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            // debug controls
            for (KeyCode i = KeyCode.Keypad0; i < KeyCode.Keypad9; i++)
            {
                if (Input.GetKeyDown(i))
                {
                    GoToScene(i - KeyCode.Keypad0);
                }
            }
        }
    }

    /// <summary>
    /// True if no 1+ scene is loaded
    /// </summary>
    public bool isInMenu => currentSceneID == 0;

    /// <summary>
    /// Reload the current scene
    /// </summary>
    public void ReloadScene()
    {
        GoToScene(currentSceneID);
    }

    /// <summary>
    /// Go to any scene. 0 = menu, else new scene
    /// </summary>
    public void GoToScene(int sceneID)
    {
        Debug.Log($"Loading scene #{sceneID} from #{currentSceneID}.");
        
        if (sceneID < 0 || sceneID >= SceneManager.sceneCountInBuildSettings)
            Debug.LogWarning("You are trying to load a scene that does not exist?");

        if (currentSceneID == 0 && sceneID == 0)
            Debug.LogWarning("Reloading menu scene is useless. Why would you do that?");

        if (currentSceneID == 0 && sceneID > 0)
        {
            // entering game from menu
            EnterGameScene(sceneID);
        }

        else if (currentSceneID > 0 && sceneID > 0)
        {
            // switching levels or restarting level
            SwitchGameScene(currentSceneID, sceneID);
        }
        
        else if (sceneID == 0)
        {
            // back to menu
            ExitGameScene(currentSceneID);
        }

        else
        {
            Debug.LogWarning("How did we get here.");
        }

        currentSceneID = sceneID;
    }


    /// <summary>
    /// Move from MENU (0) to a game scene (1+)
    /// </summary>
    protected abstract void EnterGameScene(int sceneID);

    /// <summary>
    /// Move from level (1+) to same or different level (1+)
    /// </summary>
    protected abstract void SwitchGameScene(int oldSceneID, int newSceneID);

    /// <summary>
    /// Move from level (1+) menu (0)
    /// </summary>
    protected abstract void ExitGameScene(int oldSceneID);
}

public class SceneSwitcher : ISceneSwitcher
{
    /// <summary>
    /// trigger loading screen
    /// load new scene
    /// switch HUD mode
    /// finish loading screen
    /// </summary>
    protected override void EnterGameScene(int sceneID)
    {
        ILoadingScreen.instance.StartLoadingScreen(() =>
        {
            // once everything is hidden

            StartCoroutine(LoadSceneAsync(sceneID, () =>
            {
                // new scene is loaded
                
                IHUDManager.instance.SetHUDMode(IHUDManager.HUDMode.GAME);
                
                ILoadingScreen.instance.EndLoadingScreen(() =>
                {
                    // once everything is NOT hidden anymore
                    // probably trigger an ev*nt or something
                    Debug.Log("Done loading.");
                });
            }));
        });
    }

    /// <summary>
    /// trigger loading screen
    /// unload old scene
    /// load new scene
    /// finish loading screen
    /// </summary>
    protected override void SwitchGameScene(int oldSceneID, int newSceneID)
    {
        ILoadingScreen.instance.StartLoadingScreen(() =>
        {
            // once everything is hidden
            StartCoroutine(UnloadSceneAsync(oldSceneID, () =>
            {
                // old scene is gone
                StartCoroutine(LoadSceneAsync(newSceneID, () =>
                {
                    // re-set to game from LOADING
                    IHUDManager.instance.SetHUDMode(IHUDManager.HUDMode.GAME);
                    
                    // new scene is loaded
                    ILoadingScreen.instance.EndLoadingScreen(() =>
                    {
                        // once everything is NOT hidden anymore
                        // probably trigger an ev*nt or something
                        Debug.Log("Done loading.");
                    });
                }));
            }));
        });
    }

    protected override void ExitGameScene(int oldSceneID)
    {
        ILoadingScreen.instance.StartLoadingScreen(() =>
        {
            // once everything is hidden
            StartCoroutine(UnloadSceneAsync(oldSceneID, () =>
            {
                // old scene is gone
                IHUDManager.instance.SetHUDMode(IHUDManager.HUDMode.MENU);
                ILoadingScreen.instance.EndLoadingScreen(() =>
                {
                    // once everything is NOT hidden anymore
                    // probably trigger an ev*nt or something
                    Debug.Log("Done loading.");
                });
            }));
        });
    }
    
    /// <summary>
    /// LoadSceneAsync wrapper
    /// </summary>
    private IEnumerator LoadSceneAsync(int sceneInt, Action onDone)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneInt, LoadSceneMode.Additive);
        yield return ao;
        onDone();
    }
    
    /// <summary>
    /// UnloadSceneAsync wrapper
    /// </summary>
    protected IEnumerator UnloadSceneAsync(int sceneInt, Action onDone)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(sceneInt);
        yield return ao;
        onDone();
    }
}
