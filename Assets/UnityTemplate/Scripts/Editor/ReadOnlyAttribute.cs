#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute { }
 
#if UNITY_EDITOR
/// <summary>
/// https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
/// by It3ration
/// Shows fields as read-only. Helps with orientation and allows to show useful values that shouldn't be edited.
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
        GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
 
    public override void OnGUI(Rect position,
        SerializedProperty property,
        GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif