#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(InspectAnnotationScript))]
public class InspectAnnotationScriptEditor : Editor
{
    SerializedProperty descriptionProp;
    
    private void OnEnable()
    {
        descriptionProp = serializedObject.FindProperty("description");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        descriptionProp.stringValue = EditorGUILayout.TextArea(descriptionProp.stringValue, EditorStyles.textArea);
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif