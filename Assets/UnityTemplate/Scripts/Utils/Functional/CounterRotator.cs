using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the object to always face the same direction, regardless of the rotation of its parent's parents.
/// </summary>
public class CounterRotator : MonoBehaviour
{
    private Transform parent;
    private Transform child;
    private Quaternion lastParentRotation;

    public float textHeightAboveNPC = 3;
    
    // Start is called before the first frame update
    private void Start()
    {
        child = transform;
        parent = child.parent.parent; // OR child.parent

        lastParentRotation = parent.localRotation;
        
        child.localPosition = 1 * Vector3.up;
    }

    // Update is called once per frame
    private void Update()
    {
        var parentRot = parent.localRotation;
        child.localRotation = Quaternion.Inverse(parentRot) * lastParentRotation * child.localRotation;
        child.position = parent.position + textHeightAboveNPC * Vector3.up;
        child.localPosition += 1 * Vector3.up;
        lastParentRotation = parentRot;
    }

}
