using UnityEngine;
using System.Collections;
using UnityEditor;
namespace VRLODController
{ 
    [CustomEditor(typeof(LODGroupInfo))]
    public class LevelScriptEditor : Editor
    {
        GUIContent text;
        
        void OnEnable()
        {
            text = new GUIContent("");
        }

        public override void OnInspectorGUI()
        {
            SerializedProperty customLODs = serializedObject.FindProperty("customLODs");
            text.text = "CUstom LOD distances";
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(customLODs, text, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            SerializedProperty thresholdType = serializedObject.FindProperty("thresholdType");
            text.text = "Threshold function";
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(thresholdType, text, true);
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();

            if(thresholdType.intValue == (int)LODGroupInfo.ThresholdType.Square)
            { 
                GUIStyle style = new GUIStyle(GUI.skin.GetStyle("Label"));
                style.alignment = TextAnchor.MiddleCenter;
                EditorGUILayout.LabelField("Screen border percentage", style);

                EditorGUI.BeginChangeCheck();

                text.text = "Left border";
                SerializedProperty screenBorderLeft = serializedObject.FindProperty("screenPercentageBorder.x");
                EditorGUILayout.Slider(screenBorderLeft, 0, 0.5f, text);

                text.text = "Top border";
                SerializedProperty screenBorderTop = serializedObject.FindProperty("screenPercentageBorder.y");
                EditorGUILayout.Slider(screenBorderTop, 0.5f, 1, text);

                text.text = "Right border";
                SerializedProperty screenBorderRight = serializedObject.FindProperty("screenPercentageBorder.z");
                EditorGUILayout.Slider(screenBorderRight, 0.5f, 1, text);

                text.text = "Bottom border";
                SerializedProperty screenBorderBottom = serializedObject.FindProperty("screenPercentageBorder.w");
                EditorGUILayout.Slider(screenBorderBottom, 0, 0.5f, text);

                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
            }
            else if(thresholdType.intValue == (int)LODGroupInfo.ThresholdType.Circle)
            {
                text.text = "Distance from center";
                SerializedProperty distanceThreshold = serializedObject.FindProperty("distanceFromCenter");
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(distanceThreshold, text, true);
                if (EditorGUI.EndChangeCheck())
                    serializedObject.ApplyModifiedProperties();
            }
        }
    }
}