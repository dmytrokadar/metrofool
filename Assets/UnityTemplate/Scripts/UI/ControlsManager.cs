using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager instance;

    [SerializeField] private GameObject body;
    [SerializeField] private GameObject button;
    
        
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");

        instance = this;
        
        Hide();
    }

    public void ShowOverlay()
    {
        body.SetActive(true);
    }

    public void ShowFull()
    {
        body.SetActive(true);
        button.SetActive(true);
    }

    public void Hide()
    {
        body.SetActive(false);
        button.SetActive(false);
    }
    
}
