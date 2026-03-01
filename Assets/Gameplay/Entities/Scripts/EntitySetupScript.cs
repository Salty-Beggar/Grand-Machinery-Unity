

using System;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;

public class EntitySetupScript : MonoBehaviour {
    public GameObject[] prefabs;

    void Awake() {
        Game.EntityAssetSubmanager.SetEntitiesPrefabs(prefabs);
    }
}

[CustomEditor(typeof(EntitySetupScript))]
public class EntitySetupEditor : Editor {
    SerializedProperty prefabs;
    int length;
    void OnEnable() {
        prefabs = serializedObject.FindProperty("prefabs");
        length = Enum.GetNames(typeof(EntitySpecies)).Length;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Prefab for each entity species");
        for (int i = 0; i < length; i++) {
            EditorGUILayout.PropertyField(prefabs.GetArrayElementAtIndex(i), new GUIContent(((EntitySpecies)i).ToString()));
        }

        serializedObject.ApplyModifiedProperties();
    }
}