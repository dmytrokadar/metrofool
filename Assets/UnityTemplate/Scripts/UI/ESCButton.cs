using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// main idea: esc AUTOMATICALLY binds to the action of the most visible ESC button. 
// each esc button AND esc-reciever, like QUIT GAME must have one of these!
public class ESCButton : Button
{

    private new void OnEnable()
    {
        base.OnEnable();
        ESCButtonManager.AddESCAction(this);
    }
    
}
