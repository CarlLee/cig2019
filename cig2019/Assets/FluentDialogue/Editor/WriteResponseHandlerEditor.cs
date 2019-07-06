using UnityEngine;
using System.Collections;
using UnityEditor;
using Fluent;

[CustomEditor(typeof(WriteHandler))]
[AddComponentMenu("")]
public class WriteHandlerEditor : Editor
{
    SerializedProperty textUIProperty;
    SerializedProperty secondsPerCharacterProperty;
    SerializedProperty ButtonProperty;


    void OnEnable()
    {
        textUIProperty = serializedObject.FindProperty("TextUI");
        secondsPerCharacterProperty = serializedObject.FindProperty("CharacterPauseSeconds");
        ButtonProperty = serializedObject.FindProperty("Button");
    }    

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        EditorGUILayout.PropertyField(textUIProperty, new GUIContent("Text UI", "The Text element"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(secondsPerCharacterProperty, new GUIContent("Seconds / Character", "Seconds per character"), new GUILayoutOption[] { });
        EditorGUILayout.PropertyField(ButtonProperty, new GUIContent("Button Element", "Optional: The UI button that needs to be pressed to continue the text"), new GUILayoutOption[] { });

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}
