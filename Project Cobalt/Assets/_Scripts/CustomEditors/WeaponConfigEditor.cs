using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Weapons;

[CustomEditor(typeof(WeaponConfig))]
//[CanEditMultipleObjects]
public class WeaponConfigEditor : Editor
{

    //SerializedObject abilityConfig;
    //SerializedProperty floatValue;
    WeaponConfig script;

    private void OnEnable()
    {
        //floatValue = serializedObject.FindProperty("floatValue");
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //DrawDefaultInspector();
        //serializedObject.Update();
        script = (WeaponConfig)target;
		Undo.RecordObject(script,"Changes to values");


        // Float dictionary
        // Displaying the editable fields for the dictionary 
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Custom Float Values");
        List<CustomKVP<ValueName, float>> fkvp = new List<CustomKVP<ValueName, float>>();
        foreach (var kvp in script.FloatValue)
        {
            EditorGUILayout.BeginHorizontal();
            ValueName key = (ValueName)EditorGUILayout.EnumPopup(kvp.Key);
            float value = EditorGUILayout.FloatField(kvp.Value);
            Rect floatRemoveButtonRect = EditorGUILayout.BeginVertical();
            if (!GUI.Button(floatRemoveButtonRect, GUIContent.none))
            {
                fkvp.Add(new CustomKVP<ValueName, float>(key, value));
            }
            GUILayout.Label("Remove");
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        // Saving the changes
        script.FloatValue.Clear();
        for (int i = 0; i < fkvp.Count; i++)
        {
            script.FloatValue.Add(fkvp[i].key, fkvp[i].value);
        }
        // Button for adding a new value to the dictionary
        Rect floatAddButtonRect = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(floatAddButtonRect, GUIContent.none))
        {
            script.FloatValue.Add((ValueName)0, 0);
        }
        GUILayout.Label("Add");
        EditorGUILayout.EndHorizontal();


        EditorUtility.SetDirty(script);
		//serializedObject.ApplyModifiedProperties();
    }

    




}
