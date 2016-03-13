using UnityEngine;
using System.Collections;
using UnityEditor;
namespace VRLODController
{ 
    [CustomEditor(typeof(LODGroupInfo))]
    public class LevelScriptEditor : Editor
    {
        GUIContent text;
        SerializedProperty camera;
        SerializedProperty customLODs;
        SerializedProperty screenBorderLeft;
        SerializedProperty screenBordeTop;
        SerializedProperty screenBorderRight;
        SerializedProperty screenBorderBottom;

        void OnEnable()
        {
            text = new GUIContent("");
            camera = serializedObject.FindProperty("cam");
            customLODs = serializedObject.FindProperty("customLODs");
            screenBorderLeft = serializedObject.FindProperty("screenPercentageBorder.x");
            screenBordeTop = serializedObject.FindProperty("screenPercentageBorder.y");
            screenBorderRight = serializedObject.FindProperty("screenPercentageBorder.z");
            screenBorderBottom = serializedObject.FindProperty("screenPercentageBorder.w");


        }

        public override void OnInspectorGUI()
        {
            text.text = "LOD Camera";

            LODGroupInfo info = (LODGroupInfo)target;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(camera, text);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            text.text = "CUstom LOD distances";
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(customLODs, text, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
       
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("Label"));
            style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField("Screen border percentage", style);

            EditorGUI.BeginChangeCheck();
            text.text = "Left border";
            EditorGUILayout.Slider(screenBorderLeft, 0, 0.5f, text);
            text.text = "Top border";
            EditorGUILayout.Slider(screenBordeTop, 0.5f, 1, text);
            text.text = "Right border";
            EditorGUILayout.Slider(screenBorderRight, 0.5f, 1, text);
            text.text = "Bottom border";
            EditorGUILayout.Slider(screenBorderBottom, 0, 0.5f, text);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

        }
    }
}