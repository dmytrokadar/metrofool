using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "UnityTemplate/Achievement", fileName = "NewAchievement1")]
public class Achievement : ScriptableObject
{
    /// <summary>
    /// For internal use in editor ONLY
    /// </summary>
    [NonSerialized]
    private static HashSet<Achievement> achievements;

    [SerializeField]
    public string key = "CHANGE ME";
    [SerializeField]
    private string achievementName = "Hello World";
    [SerializeField]
    private string descriptionLocked = "unlock to find out more...";
    [SerializeField]
    private string descriptionUnlocked = "you got this achievement!!";
    [SerializeField]
    public float defaultValue = 0;
    [SerializeField]
    public float goalValue = 1;
    /// <summary>
    /// Goal is to find if any two DIFFERENT achievements have the same name. Doesnt always catch all.
    /// </summary>
    private void OnValidate()
    {
        achievements ??= new HashSet<Achievement>();

        if (!achievements.Contains(this))
        {
            achievements.Add(this);
        }

        if (goalValue <= defaultValue)
        {
            Debug.LogError("Goal value must be larger than default value!");
        }

        if (key == "CHANGE ME")
        {
            key = UnityUtils.RandomChar(upper:1) + UnityUtils.RandomString(5, lower:1);
        }

        foreach (var ach in achievements.Where(ach => ach.key == key && ach != this))
        {
            Debug.LogWarning($"There may not be two achievements with the same key: {ach.name} vs {name}. Please double-check.");
        }
    }

    /// <summary>
    /// Update the variable
    /// Does not check correctness
    /// </summary>
    public void SetProgress(float newVal)
    {
        ISettings.instance.SetValue(key, newVal);
    }

    /// <summary>
    /// Find out how much is done
    /// </summary>
    public float GetProgress()
    {
        return ISettings.instance.GetValue(key, defaultValue);
    }

    /// <summary>
    /// Find out how much is done as a ratio of total
    /// </summary>
    public float GetProgressRatio()
    {
        float progress = GetProgress();
        if (progress >= float.MaxValue)
        {
            return 1;
        }
        
        return progress / (goalValue - defaultValue);
    }

    
    /// <summary>
    /// True if achievement is done
    /// </summary>
    public bool isDone => GetProgress() >= goalValue;

    public string currentName => achievementName;
    public string currentDesc => isDone ? descriptionUnlocked : descriptionLocked;

    public override string ToString()
    {
        return $"{achievementName} @ {GetProgressRatio():.0%} in {defaultValue:.0f}->{goalValue:.0f}";
    }
}
