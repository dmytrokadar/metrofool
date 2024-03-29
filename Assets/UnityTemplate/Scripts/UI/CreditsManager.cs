using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines an interface for credits things
/// </summary>
public abstract class ICreditsManager : MonoBehaviour
{
    public static ICreditsManager instance;

    [SerializeField] private GameObject _gameObject;

    private bool isActive;
    private bool isEnding;
    
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");

        instance = this;
        
        _gameObject.SetActive(false);
    }

    /// <summary>
    /// Start show animation, if not already
    /// </summary>
    public void Show()
    {
        // dont re-show
        if (isActive)
        {
            return;
        }
        
        _gameObject.SetActive(true);
        isActive = true;
        StartShow();
    }

    /// <summary>
    /// Start hide animation, if not already
    /// </summary>
    public void Hide()
    {
        if (isEnding)
        {
            return;
        }

        isEnding = true;
        
        EndShow(() =>
        {
            _gameObject.SetActive(false);
            isActive = false;
            isEnding = false;
        });
    }

    protected abstract void StartShow();
    protected abstract void EndShow(Action onDone);
}



public class CreditsManager : ICreditsManager
{
    protected override void StartShow()
    {
        // TODO something w coroutines
    }

    protected override void EndShow(Action onDone)
    {
        // TODO too
        onDone();
    }
}
