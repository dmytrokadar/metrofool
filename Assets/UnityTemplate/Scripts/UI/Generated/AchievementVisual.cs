using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class IAchievementVisual : MonoBehaviour
{
    public abstract void UpdateAchievement(Achievement achievement);

    public abstract void ShowTooltip();
    public abstract void HideTooltip();
}

public class AchievementVisual : IAchievementVisual
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image incompleteImage;
    [SerializeField] private Image completeImage;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderPercentage;
    
    private string description;
    
    public override void UpdateAchievement(Achievement achievement)
    {
        // how much done
        float val = achievement.GetProgressRatio();
        slider.value = val;
        sliderPercentage.text = $"{val:0%}";
        
        incompleteImage.SetA(achievement.isDone ? 0 : 0.1f);
        completeImage.SetA(achievement.isDone ? 0.1f : 0f);

        nameText.text = achievement.currentName;
        description = achievement.currentDesc;

    }

    public override void ShowTooltip()
    {
        Tooltip.Show(description);
    }

    public override void HideTooltip()
    {
        Tooltip.Hide();
    }
}
