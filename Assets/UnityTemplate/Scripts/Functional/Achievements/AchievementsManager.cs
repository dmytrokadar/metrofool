using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager instance;

    /// <summary>
    /// its public but pls no modify
    /// </summary>
    [SerializeField] public List<Achievement> achievements;

    private Dictionary<string, Achievement> achievementsByKey;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");

        instance = this;

        achievementsByKey = achievements.ToDictionary(a => a.key, a => a);

    }

    private void Start()
    {
        foreach (Achievement achievement in achievements)
        {
            RegisterAchievement(achievement);
        }
    }

    private void Update()
    {
        if (!Application.isEditor)
            return;

        if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            AchievementGet("TEST", 1.0f);
        }
        
    }

    /// <summary>
    /// Declare an achievement.
    /// If not exisits, initialize value in Memory
    /// If exists, fetch value from Memory
    /// </summary>
    private void RegisterAchievement(Achievement achievement)
    {
        if (!ISettings.instance.HasValue(achievement.key))
        {
            ISettings.instance.SetValue(achievement.key, achievement.defaultValue);
        }
    }

    /// <summary>
    /// !!! DANGER !!!
    /// Resets ALL achievements to default values.
    /// </summary>
    public void ResetAll()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.SetProgress(achievement.defaultValue);
        }
    }

    /// <summary>
    /// Update the value of an achievement
    /// Optionally include amount -- how much to add to total. Else automatically approved
    /// </summary>
    public void AchievementGet(string key, float amount = float.PositiveInfinity)
    {
        float newVal = ISettings.instance.GetValue(key, float.NaN) + amount;

        achievementsByKey[key].SetProgress(newVal);
        
        if (newVal >= achievementsByKey[key].goalValue)
        {
            OnAchievementGot(achievementsByKey[key]);
        }
        
        IAchievementScreen.instance.UpdateIfShown(); 
    }

    protected virtual void OnAchievementGot(Achievement achievement)
    {
        // TODO
        Debug.Log($"Got achievement: {achievement.ToString()}!");
    }
    
}
