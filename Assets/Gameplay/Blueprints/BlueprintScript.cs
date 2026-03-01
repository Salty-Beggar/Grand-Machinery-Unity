

using System;
using UnityEngine;

public class BlueprintScript : MonoBehaviour {
    public bool executeCreators = true;
    void Awake() {
        Debug.Log("Penis");
    }

    void OnValidate() {
        Debug.Log("Penisss");
    }
}