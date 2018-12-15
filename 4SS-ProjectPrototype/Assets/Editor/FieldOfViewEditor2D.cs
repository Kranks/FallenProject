using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[CustomEditor (typeof (Perspective))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI() {
        Perspective p = (Perspective) target;
        Handles.color = Color.white;
        Handles.DrawWireArc(p.transform.position, Vector3.up, Vector3.forward, 360, p.viewRadius);
        Vector3 viewAngleA = p.DirFromAngle(-p.viewAngle / 2, false);
        Vector3 viewAngleB = p.DirFromAngle(p.viewAngle / 2, false);
        Handles.DrawLine(p.transform.position, p.transform.position + viewAngleA * p.viewRadius);
        Handles.DrawLine(p.transform.position, p.transform.position + viewAngleB * p.viewRadius);
    }
}*/
