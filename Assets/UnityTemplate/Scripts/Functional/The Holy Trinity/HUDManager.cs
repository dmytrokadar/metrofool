using System;
using UnityEngine;


/// <summary>
/// Defines an interface for HUD switchers. 
/// Is a singleton.
/// </summary>
public abstract class IHUDManager : MonoBehaviour
{
    public enum HUDMode
    {
        MENU, // only MENU, COMMON and LOADING is active
        GAME, // only GAME, COMMON and LOADING is active.
        LOADING, // serves as a dummy in-between state where only COMMON and LOADING are active.
        NONE // only COMMON is active
    }

    [SerializeField] protected GameObject menuCanvas;
    [SerializeField] protected GameObject gameCanvas;
    [SerializeField] protected GameObject loadingCanvas;
    [SerializeField] protected GameObject commonCanvas;
    
    public static IHUDManager instance;

    public HUDMode hudMode
    {
        get;
        private set;
    }
    
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("HUD Manager instance already exists! Check if the script isn't present multiple times.");

        instance = this;
        
        SetHUDMode(HUDMode.NONE);
    }

    public void SetHUDMode(HUDMode newMode)
    {
        OnSwitchHUDMode(newMode, hudMode);
        hudMode = newMode;
    }

    protected abstract void OnSwitchHUDMode(HUDMode newMode, HUDMode oldMode);

}



/// <summary>
/// HUD Manager takes care of switching states
/// Also implements a singleton access point.
/// Defaults to NONE shown
/// </summary>
public class HUDManager : IHUDManager
{
    
    /// <summary>
    /// Prevent inconsistent states by keeping track of UI toggling.
    /// </summary>
    private bool isToggledUI = false;
    
    private void Start()
    {
        SetHUDMode(HUDMode.MENU);
    }

    private void Update()
    {
        if (Input.GetButtonDown("PhotoMode"))
        {
            ToggleUI();
        }
    }

    /// <summary>
    /// Photo mode!
    /// </summary>
    private void ToggleUI()
    {
        if (hudMode == HUDMode.GAME && !isToggledUI)
        {
            SetHUDMode(HUDMode.NONE);
            isToggledUI = true;
        }
        
        else if (hudMode == HUDMode.NONE && isToggledUI)
        {
            SetHUDMode(HUDMode.GAME);
            isToggledUI = false;
        }
    }

    protected override void OnSwitchHUDMode(HUDMode newMode, HUDMode oldMode)
    {
        if (newMode == oldMode)
            return;

        // dont forget to turn off photo mode
        if (oldMode == HUDMode.NONE && newMode != HUDMode.GAME) 
            isToggledUI = false;

        switch (newMode)
        {
            case HUDMode.MENU:
                menuCanvas.TrySetActive(true);
                gameCanvas.TrySetActive(false);
                loadingCanvas.TrySetActive(true);
                break;
            case HUDMode.GAME:
                menuCanvas.TrySetActive(false);
                gameCanvas.TrySetActive(true);
                loadingCanvas.TrySetActive(true);
                break;
            case HUDMode.LOADING:
                // just make sure loading canvas is okay
                loadingCanvas.TrySetActive(true);
                break;
            case HUDMode.NONE: 
                // dont set anything active, except commonCanvas. Never deactivate CommonCanvas
                menuCanvas.TrySetActive(false);
                gameCanvas.TrySetActive(false);
                loadingCanvas.TrySetActive(false);
                break;
            default:
                Debug.LogError("what");
                break;
        }
        
    }
}
