using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(YellHandler))]
[AddComponentMenu("")]
public class YellConversationResponseHandlerEditor : Editor
{
    SerializedProperty YellUI;
    SerializedProperty FaceCamera;

    SerializedProperty Camera;

    protected static bool showCameraSettings = true;
 
    void OnEnable()
    {
        YellUI = serializedObject.FindProperty("YellUI");
        FaceCamera = serializedObject.FindProperty("FaceCamera");
        Camera = serializedObject.FindProperty("Camera");
    }
    

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        // Show the custom GUI controls
        EditorGUILayout.PropertyField(YellUI, new GUIContent("Yell Canvas", "The unity canvas with a text component that will be SetActive to handle a Yell"), new GUILayoutOption[] { });

        FaceCamera.boolValue = EditorGUILayout.Toggle("Is Billboard", FaceCamera.boolValue);
        if (FaceCamera.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(Camera, new GUIContent("The Camera", "The camera the dialog will face so that it always appears head on"), new GUILayoutOption[] { });
            EditorGUI.indentLevel--;
        } 

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}
