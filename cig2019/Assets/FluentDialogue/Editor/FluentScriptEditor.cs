using UnityEngine;
using UnityEditor;
using System;
using Fluent;
using System.Collections.Generic;

/// <summary>
/// This creates a tree of your FluentScript and displays it in the editor
/// </summary>
[CustomEditor(typeof(FluentScript), true)]
public class FluentScriptEditor : Editor
{
   
    /// <summary>
    /// Keep the expanded state of the tree items
    /// </summary>
    class TreeItemState
    {
        public bool Expanded { get; set; }
        public List<TreeItemState> Children { get; set; }

        public TreeItemState()
        {
            Children = new List<TreeItemState>();
        }
    }
    

    protected static bool showCameraSettings = true;
    static bool pinAlwaysOpen = false;
    static bool newPinAlwaysOpen = false;
    GUIStyle headerStyle;
    GUIStyle keywordStyle;
    GUIStyle errorStyle;
    GUIStyle richTextStyle;

    static TreeItemState rootTreeItemState;
    static FluentScript lastFluentScript;

    void OnEnable()
    {
    }
    
    private void Print(FluentNode node, TreeItemState treeItemState, bool isRoot = false)
    {
        if (node.HasChildren)
        {
            if (isRoot)
            {
                EditorGUILayout.BeginHorizontal();
                treeItemState.Expanded = EditorGUILayout.Foldout(treeItemState.Expanded, "Root", headerStyle) || pinAlwaysOpen;
                newPinAlwaysOpen = EditorGUILayout.Toggle(pinAlwaysOpen);
                if (pinAlwaysOpen != newPinAlwaysOpen)
                {
                    pinAlwaysOpen = newPinAlwaysOpen;
                }
                EditorGUILayout.EndHorizontal();
            } else
            {
                bool newExpandedState = EditorGUILayout.Foldout(treeItemState.Expanded, node.StringInEditor(), headerStyle) || pinAlwaysOpen;

                if (!newExpandedState)
                    pinAlwaysOpen = false;

                if (treeItemState.Expanded != newExpandedState)
                    treeItemState.Expanded = newExpandedState;
            }

            if (!treeItemState.Expanded)
                return;

            // Make sure the TreeItemState has enough children
            while (treeItemState.Children.Count < node.Children.Count)
                treeItemState.Children.Add(new TreeItemState());            

            // Print all the children
            EditorGUILayout.BeginVertical();
            for (int iChild = 0; iChild < node.Children.Count; iChild++)
            {
                FluentNode childNode = node.Children[iChild];
                TreeItemState childState = treeItemState.Children[iChild];
                EditorGUI.indentLevel++;
                Print(childNode, childState);
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
        }
        else
            EditorGUILayout.LabelField(node.StringInEditor(), richTextStyle);

    }
    bool showError = false;

    private void DisplayException(string errorString, string exceptionHeader, Exception ex)
    {
        showError = EditorGUILayout.Foldout(showError, errorString, errorStyle);
        if (showError)
            EditorGUILayout.HelpBox(exceptionHeader + ex.ToString(), MessageType.None, true);

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        headerStyle = new GUIStyle(EditorStyles.foldout);
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.richText = true;

        keywordStyle = new GUIStyle(EditorStyles.label);
        keywordStyle.normal.textColor = Color.blue;

        errorStyle = new GUIStyle(EditorStyles.foldout);
        errorStyle.normal.textColor = Color.red;

        richTextStyle = new GUIStyle(GUI.skin.label);
        richTextStyle.richText = true;

        FluentNode root = null;
        try
        {
            root = ((FluentScript)target).SequentialNode(((FluentScript)target).Create());
        }
        catch (Exception ex)
        {
            DisplayException("There was an exception creating dialogue tree in editor mode",
                             "This is expected if you have runtime dependant code in your FluentScript" + Environment.NewLine + Environment.NewLine,
                             ex);
            return;
        }

        // Print the tree
        try
        {
            if (rootTreeItemState == null || lastFluentScript != target)
            {
                rootTreeItemState = new TreeItemState();
                lastFluentScript = (FluentScript)target;
            }

            Print(root, rootTreeItemState, true);
        }
        catch (Exception ex)
        {
            DisplayException("There was an exception trying to show you the tree, please report this", "", ex);
        }
    }
}
