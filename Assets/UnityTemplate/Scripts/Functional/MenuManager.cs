using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Instance already exists! Check if the script isn't present multiple times.");

        instance = this;
    }

    public void OnQuit()
    {
        Confirmator.instance.Show(() =>
            {
                if (Application.isEditor)
                    EditorApplication.ExitPlaymode();
                else
                    Application.Quit();
            }, () => {},
            title_s: "Quit game?",
            info_s: "We ran the numbers and this is a net negative for you.",
            confirm_s: "Quit",
            reject_s: "Stay"
        );
    }
    
    
    
}
