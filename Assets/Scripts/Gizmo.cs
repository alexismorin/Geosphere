using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour {

    public string gizmoColor;

    void OnDrawGizmos () {
        Gizmos.DrawIcon (transform.position, gizmoColor + ".png", true);
    }
}