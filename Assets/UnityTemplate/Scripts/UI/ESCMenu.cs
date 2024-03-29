using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the ESC menu
/// </summary>
public abstract class IESCMenu : MonoBehaviour
{
    /// <summary>
    /// whoops! all singletons!
    /// </summary>
    public static IESCMenu instance;
    
    /// <summary>
    /// true iff game is paused by this script (0 time)
    /// </summary>
    private bool paused = false; 
    
    /// <summary>
    /// what was the time scale before time was paused if paused
    /// </summary>
    private float unpausedTimeScale; 

    /// <summary>
    /// Make this true if ESC should pause game
    /// </summary>
    [Tooltip("Make this true if ESC should pause game")]
    [SerializeField] private bool pauseOnESC = false;
    
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    
    [SerializeField] [Tooltip("This will be displayed if game is PAUSED")]
    private string gamePausedText = "Game paused";
    
    [SerializeField] [Tooltip("This will be displayed if esc menu is active WITHOUT PAUSING")]
    private string gameOverlayedText = "Game Menu";
    
    [SerializeField] private Image escButtonImage;
    [SerializeField] public bool showESCButtonInGame = true;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");
        else
        {
            instance = this;
        }
        
        gameObject.SetActive(false);
        UpdateESCButton();
    }

    private void OnDestroy()
    {
        Unpause();
    }

    private void OnDisable()
    {
        Unpause();
    }


    /// <summary>
    /// Show ESC menu, optionally FORCE a pause in time
    /// </summary>
    public void Show(bool overridePauseGame = false)
    {
        
        gameObject.SetActive(true);

        if (pauseOnESC || overridePauseGame)
        {
            Pause();
            title.text = gamePausedText;
        }
        else
        {
            title.text = gameOverlayedText;
        }

        OnShow();
        UpdateESCButton();
    }

    /// <summary>
    /// Hide ESC menu
    /// </summary>
    public void Hide()
    {
        Unpause(); // to be sure
        gameObject.SetActive(false);
        UpdateESCButton();
    }

    /// <summary>
    /// Pause game and remember what happened
    /// Does NOT check
    /// </summary>
    private void Pause()
    {
        if (paused)
            return;
        
        paused = true;
        unpausedTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
    }
    
    /// <summary>
    /// Unpause game and restore time scale
    /// </summary>
    private void Unpause()
    {
        if (!paused)
            return;

        paused = false;
        Time.timeScale = unpausedTimeScale;
    }

    /// <summary>
    /// Hide the in-game ESC button if overlayed by something else or if disabled.
    /// </summary>
    private void UpdateESCButton()
    {
        // only show if not overlayed AND allowed at all
        bool showButton = showESCButtonInGame && !gameObject.activeInHierarchy;
        escButtonImage.SetA(showButton);
    }

    protected abstract void OnShow();
}


/// <summary>
/// Use this for example to show score, etc.
/// </summary>
public class ESCMenu : IESCMenu
{
    protected override void OnShow()
    {
        // do nothing by default
        
    }
}

    
    
