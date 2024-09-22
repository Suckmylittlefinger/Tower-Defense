using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BatchReNamer : EditorWindow
{
    private static readonly Vector2Int size = new Vector2Int(250, 100);
    private string childrenPrefix;
    private int startIndex;
    [MenuItem("GameObject/Batch Re-namer")] public static void ShowWindow() {
        EditorWindow window = GetWindow<BatchReNamer>();
        window.minSize = size;
        window.maxSize = size;
    }
    private void OnGUI() {
        childrenPrefix = EditorGUILayout.TextField("Children prefix", childrenPrefix);
        startIndex = EditorGUILayout.IntField("Start index", startIndex);
        if (GUILayout.Button("Batch Re-namer")) {
            GameObject[] selectedObjects = Selection.gameObjects;
            for (int objectI = 0; objectI < selectedObjects.Length; objectI++) {
                Transform selectedObjectT = selectedObjects[objectI].transform;
                for (int childI = 0, i = startIndex; childI < selectedObjectT.childCount; childI++) selectedObjectT.GetChild(childI).name = $"{childrenPrefix}{i++}";
            }
        }
    }
}
