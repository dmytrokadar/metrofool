using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class IAchievementScreen : MonoBehaviour
{
    public static IAchievementScreen instance;

    [SerializeField] public GameObject popup;

    private bool initialized;
    
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");

        instance = this;
        
        Hide();
    }

    private void Start()
    {
        InitVisuals();
        initialized = true;
    }

    /// <summary>
    /// generate UI elements based on achievements
    /// </summary>
    protected abstract void InitVisuals();

    /// <summary>
    /// Update UI elements' values
    /// </summary>
    protected abstract void UpdateVisuals();

    public void UpdateIfShown()
    {
        if (!initialized)
            return;

        if (!gameObject.activeInHierarchy)
            return;
        
        UpdateVisuals();
    }
    
    public void Show()
    {
        popup.SetActive(true);

        // no update during visibility
        UpdateVisuals();
    }

    public void Hide()
    {
        popup.SetActive(false);
    }

    /// <summary>
    /// Reset all achievements w/ confirmation
    /// </summary>
    public void ResetAll()
    {
        Confirmator.instance.Show(
            () =>
            {
                AchievementsManager.instance.ResetAll();
                UpdateIfShown();
            },
            null,
            "Reset all achievements?",
            "All progress will be lost permanently."

        );
        
        
    }
}

// TODO
public class AchievementScreen : IAchievementScreen
{
    // [SerializeField] private TextMeshProUGUI textBoxTest; 

    [SerializeField] private RectTransform contentBox;
    [SerializeField] private GameObject achievementPrefab;

    [SerializeField] private float itemSize = 100.0f;
    [SerializeField] private float itemPadding = 20.0f;
    
    private List<GameObject> achievementBoxes;

    private List<Achievement> achievements => AchievementsManager.instance.achievements;
    
    protected override void InitVisuals()
    {
        achievementBoxes ??= new List<GameObject>();
        
        // TODO
        // textBoxTest.text = AchievementsManager.instance.achievements.ToString2();

        for (int i = 0; i < achievements.Count; i++)
        {
            GameObject goi = Instantiate(achievementPrefab, contentBox);
            RectTransform rt = goi.GetComponent<RectTransform>();
            
            float offset = i * (itemSize + itemPadding);
            rt.SetHeight(itemSize);
            rt.localPosition = new Vector3(0, offset, 0);
            
            achievementBoxes.Add(goi);
        }
        
        contentBox.SetHeight((itemSize + itemPadding) * achievements.Count);
        
        UpdateVisuals();
    }

    protected override void UpdateVisuals()
    {
        if (achievementBoxes == null)
        {
            Debug.LogWarning("Updating nonexistent visuals!");
            return;
        }
        
        if (achievementBoxes.Count != achievements.Count) 
            Debug.LogWarning("Mismatched list lengths!");

        for (int i = 0; i < achievementBoxes.Count; i++)
        {
            achievementBoxes[i].GetComponent<AchievementVisual>().UpdateAchievement(achievements[i]);
        }
        
    }
}

