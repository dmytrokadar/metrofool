using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnClickExit(){
        if (Application.isEditor)
            EditorApplication.ExitPlaymode();
        else
            Application.Quit();
    }

    public void ChangeScene(string scName){
        SceneManager.LoadScene(scName, LoadSceneMode.Additive);
    }
}
