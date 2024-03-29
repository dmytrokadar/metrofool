using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provide an interface for the Loading screen
/// </summary>
public abstract class ILoadingScreen : MonoBehaviour
{
    public static ILoadingScreen instance;

    [SerializeField] private GameObject _gameObject;

    /// <summary>
    /// True iff the loading screen enter animation is active
    /// </summary>
    [SerializeField] private bool isAnimatingEnter;
    /// <summary>
    /// True iff the loading screen exit animation is active
    /// </summary>
    [SerializeField] private bool isAnimatingExit;
    /// <summary>
    /// True iff the loading screen is active but done animating (e.g. waiting for other process)
    /// </summary>
    [SerializeField] private bool isBetweenState;
    
    /// <summary>
    /// True iff the loading screen enter animation is active or animating
    /// </summary>
    private bool isActive => isAnimatingEnter || isAnimatingExit || isBetweenState;
        
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");
        else
        {
            instance = this;
        }
        
        _gameObject.SetActive(false);
    }
    
    private void OnValidate()
    {
        isAnimatingEnter = false;
        isAnimatingExit = false;
        isBetweenState = false;
    }

    private void Update()
    {
        if (!Application.isEditor)
            return;

        if (Input.GetKeyDown(KeyCode.KeypadDivide))
        {
            Debug.Log("Starting loading...");
            StartLoadingScreen(() => Debug.Log("Done starting loading."));
        }

        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            Debug.Log("Starting ending loading...");
            EndLoadingScreen(() => Debug.Log("Done ending loading."));
        }


    }

    /// <summary>
    /// Starts loading screen animation.
    /// </summary>
    /// <param name="onFinishedAnimation">Called when ENTER animation of loading screen ends and Loadin screen is fully ON</param>
    /// <param name="waitForSeconds">Optionally specify how many seconds MINIMUM to have loading screen loaded</param>
    public void StartLoadingScreen(Action onFinishedAnimation, float waitForSeconds = 0.0f)
    {
        // TODO check if already doing something, if yes, IDK? queue i guess?
        if (isActive)
            return;
        
        IHUDManager.instance.SetHUDMode(IHUDManager.HUDMode.LOADING);

        _gameObject.SetActive(true);

        isAnimatingEnter = true;
        
        StartCoroutine(StartLoadingAsync(onFinishedAnimation, waitForSeconds));
    }
    
    private IEnumerator StartLoadingAsync(Action onDone, float waitForSeconds)
    {        
        yield return AnimationStartAsync();
        
        isBetweenState = true;
        isAnimatingEnter = false;

        onDone?.Invoke();

        yield return new WaitForSeconds(waitForSeconds);

        
        yield break;
    }

    /// <summary>
    /// Starts end-of-loading screen animation.<br />
    /// Automatically waits until start animation finishes.
    /// </summary>
    /// <param name="onFinishedAnimation">Called when exit animation of loading screen ends and loadin screen is fully OFF</param>
    public void EndLoadingScreen(Action onFinishedAnimation)
    {
        // TODO check if already doing something, if yes, IDK? queue i guess?
        if (!isBetweenState)
            return;

        isAnimatingExit = true;
        isBetweenState = false;

        StartCoroutine(EndLoadingAsync(onFinishedAnimation));
    }

    private IEnumerator EndLoadingAsync(Action onDone)
    {
        // while (isAnimating)
        // {
        //     yield return null;
        // }
        //
        // isAnimating = true;
        
        yield return AnimationEndAsync();
        
        onDone?.Invoke();
        
        _gameObject.SetActive(false);
        isAnimatingExit = false;
        
        
        yield break;
    }

    

    /// <summary>
    /// Show loading screen enter animation, then call onDone.
    /// </summary>
    protected abstract IEnumerator AnimationStartAsync();
    /// <summary>
    /// Show loading screen exit animation, then call onDone.
    /// </summary>
    protected abstract IEnumerator AnimationEndAsync();

}


public class LoadingScreen : ILoadingScreen
{
    [SerializeField] private Image background;

    // lower = slower
    [SerializeField] private float animationSpeed = 1.0f;
    
    // edit this
    protected override IEnumerator AnimationStartAsync()
    {
        
        // set to transparent at the beginning
        background.SetA(0);

        // until fully opaque:
        while (background.GetA() < 1)
        {
            // increment opacity
            background.IncrementA(Time.deltaTime * animationSpeed);
            
            // stall 1 frame
            yield return null;
        }

        // finish
        yield break;
    }

    // and this too
    protected override IEnumerator AnimationEndAsync()
    {
        // set to opaque at the beginning
        background.SetA(1);

        // until fully transparent
        while (background.GetA() > 0)
        {
            // decrement opacity
            background.IncrementA( - Time.deltaTime * animationSpeed);
            
            // stall 1 frame
            yield return null;
        }

        // finish when done
        yield break;
    }
}