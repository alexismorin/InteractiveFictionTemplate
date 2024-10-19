using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StripPrefix : MonoBehaviour {

    [MenuItem("Tools/Strip gameobjects of SM_ Prefix")]
    static void Strip() {
        GameObject[] itemSelection = Selection.gameObjects;

        for (int i = 0; i < itemSelection.Length; i++) {
            itemSelection[i].name = itemSelection[i].name.Replace("SM_", "");
        }
    }
}