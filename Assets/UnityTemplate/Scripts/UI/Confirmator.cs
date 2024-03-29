using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines an interface for confirmation popups. 
/// </summary>
public abstract class IConfirmator : MonoBehaviour
{
    [SerializeField] protected GameObject popup;

    [SerializeField] protected TextMeshProUGUI confirm;
    [SerializeField] protected TextMeshProUGUI reject;
    [SerializeField] protected TextMeshProUGUI title;
    [SerializeField] protected TextMeshProUGUI description;

    /// <summary>
    /// Shows a popup if not already present. Defines custom actions on confirmal or denial.
    /// </summary>
    public abstract void Show(Action onConfirm, Action onDeny = null, 
        string title_s = null, string info_s = null, string confirm_s = null, string reject_s = null);
    
    /// <summary>
    /// Hides a popup if not already hidden.
    /// </summary>
    public abstract void Hide();
}

/// <summary>
/// Standard confirmator popup.
/// Updates any changes to displayed texts before showing.
/// </summary>
public class Confirmator : IConfirmator
{
    [SerializeField] public string titleText = "Do you really want to do this?";
    [SerializeField] public string infoText = "Please confirm or deny";
    [SerializeField] public string confirmText = "Confirm";
    [SerializeField] public string rejectText = "Deny";

    private Action confirmAction;
    private Action denyAction;

    public static Confirmator instance;
    
    private void Awake()
    {
        instance = this;
        Hide();
    }

    public override void Show(Action onConfirm, Action onDeny = null, 
        string title_s = null, string info_s = null, string confirm_s = null, string reject_s = null)
    {
        title.text = title_s ?? titleText;
        description.text = info_s ?? infoText;
        confirm.text = confirm_s ?? confirmText;
        reject.text = reject_s ?? rejectText;
        
        confirmAction = onConfirm ?? (() => {});
        denyAction = onDeny ?? (() => {});
        
        popup.SetActive(true);
    }

    public override void Hide()
    {
        popup.SetActive(false);
    }

    /// <summary>
    /// Call this method if confirmation was confirmed
    /// </summary>
    public void ConfirmClicked()
    {
        confirmAction();
        Hide();
    }

    /// <summary>
    /// Call this method if denied
    /// </summary>
    public void DenyClicked()
    {
        denyAction();
        Hide();
    }
}






