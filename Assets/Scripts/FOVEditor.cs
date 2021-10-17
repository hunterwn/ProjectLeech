using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FOVCone))]

public class FOVEditor : Editor
{
    void OnSceneGUI()
    {
        FOVCone fow = (FOVCone)target;
        Handles.color = Color.white;
        //Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle (-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle (fow.viewAngle / 2, false);

        //Handles.DrawWireArc(fow.transform.position, Vector3.up, viewAngleA, fow.viewAngle, fow.viewRadius);
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.right, 360, fow.viewRadius);

        Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine (fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
        
        Handles.color = Color.red;

        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            Handles.DrawLine (fow.transform.position, visibleTarget.position);
        }
    }
}
