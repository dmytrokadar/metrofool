using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public enum PickableType{
        card,
        keyRed,
        keyGreen,
        keyYellow
    }

    public PickableType PType;

    // private void OnTriggerEnter(Collider other){
    //     UnityEngine.Debug.Log("collision ");
    // }
}
