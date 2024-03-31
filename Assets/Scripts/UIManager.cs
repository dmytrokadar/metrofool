using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnClickExit(){
        // if (Application.isEditor)
        //     EditorApplication.ExitPlaymode();
        // else
            Application.Quit();
    }

    public void ChangeScene(string scName){
        SceneManager.LoadScene(scName, LoadSceneMode.Single);
    }

    public void OnClickOpenTutorial(GameObject gameObject){
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void OnClickCloseWindow(GameObject gameObject){
        gameObject.SetActive(false);    
    }
}
