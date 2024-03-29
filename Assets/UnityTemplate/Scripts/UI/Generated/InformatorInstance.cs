using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformatorInstance : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI title;
    [SerializeField] public TextMeshProUGUI description;
    [SerializeField] public TextMeshProUGUI exitButtonDesc;
    
    public void Hide()
    {
        Destroy(gameObject);
    }
}
