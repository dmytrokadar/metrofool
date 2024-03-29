using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCButtonManager : MonoBehaviour
{
    protected static ESCButtonManager instance;
    
    protected static LinkedList<ESCButton> buttons = new();

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("instance already exists! Check if the script isn't present multiple times.");

        instance = this;
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            FindESCAction();
        }
    }

    public static void AddESCAction(ESCButton button)
    {
        buttons.AddLast(button);
    }
    
    // find last activated 
    private static void FindESCAction()
    {
        PruneStack();

        if (buttons.Count != 0)
        {
            ESCButton top = buttons.Last.Value;
            buttons.RemoveLast();
            
            // bingo
            top.onClick.Invoke();
                
            // burn any copies
            buttons.RemoveAll((button => (button == top)));
        }
        else
        {
            switch (IHUDManager.instance.hudMode)
            {
                case IHUDManager.HUDMode.MENU:
                    MenuManager.instance.OnQuit();
                    break;
                case IHUDManager.HUDMode.GAME:
                    IESCMenu.instance.Show();
                    break;
                case IHUDManager.HUDMode.NONE:
                case IHUDManager.HUDMode.LOADING:
                default:
                    break;
            }
        }

        ;
    }

    private static void PruneStack()
    {
        // burn all inactive things just to be sure
        buttons.RemoveAll(button => button == null || !button.gameObject.activeInHierarchy);
    }
}
