using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Spawns a new Information popup.
/// Functionally equivalent to a Tooltip, but visually similar to Confirmation.
/// </summary>
public class Informator : MonoBehaviour
{
    [SerializeField] 
    private GameObject informatorPrefab;
    
    public static Informator instance;

    // private List<GameObject> popups;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (!Application.isEditor) 
            return;
        
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Show("Sometimes I think", 
                "Is this all there is? Is life just a sick joke without a punchline and we're all waiting for the sweet, sweet release of death?",
                "Cry in the corner");
        }

        // example
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Tooltip.Show("I disagree strongly with whatever work this quote is attached to. -XKCD");
        }
        //
        // if (Input.GetKeyDown(KeyCode.KeypadPlus))
        // {
        //     Tooltip.Show(Tooltip.lastText + "test ");
        // }

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            Tooltip.Hide();
        }
    }

    /// <summary>
    /// Shows a popup if not already present. Defines custom actions on confirmal or denial.
    /// </summary>
    public void Show(
        string title_s = "Attention!", string info_s = null, string confirm_s = "Okay")
    {
        GameObject go = Instantiate(informatorPrefab, transform);
        InformatorInstance ii = go.GetComponent<InformatorInstance>();

        ii.title.text = title_s;
        ii.description.text = info_s;
        ii.exitButtonDesc.text = confirm_s;
    }
    
}
