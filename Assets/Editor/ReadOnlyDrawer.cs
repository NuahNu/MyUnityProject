using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 클릭 방지
        GUI.enabled = false;

        EditorGUI.PropertyField(position, property, label);

        GUI.enabled = true;

        //base.OnGUI(position, property, label);
    }
}
