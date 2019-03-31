using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class simulateEditorPhysics : MonoBehaviour {

    [MenuItem ("Tools/Simulate Editor Physics - Start")]
    static void editorSimStart () {
        Physics.autoSimulation = false;
        EditorApplication.update += Update;
    }

    [MenuItem ("Tools/Simulate Editor Physics - Stop")]
    static void editorSimStop () {
        Physics.autoSimulation = true;
        EditorApplication.update -= Update;
    }

    static void Update () {
        Physics.Simulate (Time.fixedDeltaTime);
    }

}