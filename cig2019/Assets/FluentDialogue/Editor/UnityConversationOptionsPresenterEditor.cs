using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(UnityOptionsPresenter))]
[AddComponentMenu("")]
public class UnityConversationOptionsPresenterEditor : Editor
{
    SerializedProperty dialogUI;
    SerializedProperty optionsPanel;

    SerializedProperty itemUIPrefab;

    protected static bool showDialogSettings = true;
    protected static bool showOptionItemSettings = true;
 
    void OnEnable()
    {
        dialogUI = serializedObject.FindProperty("DialogUI");
        optionsPanel = serializedObject.FindProperty("OptionsPanel");
        itemUIPrefab = serializedObject.FindProperty("OptionItemUI");
    }
    

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        // Show the custom GUI controls
        showDialogSettings = EditorGUILayout.Foldout(showDialogSettings, "Dialog");

        if (showDialogSettings)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(dialogUI, new GUIContent("UI Control", "The unity UI control to show when this talk action is invoked"), new GUILayoutOption[] { });
            EditorGUILayout.PropertyField(optionsPanel, new GUIContent("Options Element", "The path to the UI element that lists the options inside the dialog"), new GUILayoutOption[] { });
            EditorGUI.indentLevel--;
        }

        // Show the option item control
        showOptionItemSettings = EditorGUILayout.Foldout(showOptionItemSettings, "Dialog Item");

        if (showOptionItemSettings)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(itemUIPrefab, new GUIContent("Prefab", "The unity UI prefab that will represent options in the dialog"), new GUILayoutOption[] { });
            EditorGUI.indentLevel--;
        }

        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}
