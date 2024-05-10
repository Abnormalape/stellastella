using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TreeMovement : MonoBehaviour
{
    public float X;
    public float Y;
    public float Z;
    public TreeMovement(Vector3 treeposition)
    {
        X = MathF.Round(treeposition.x);
        if (X > treeposition.x) { X = X - 0.5f; }
        else if (X <= treeposition.x) { X = X + 0.5f; }
        Y = treeposition.y;
        if (Y > treeposition.y) { Y = Y - 0.5f; }
        else if (Y <= treeposition.y) { Y = Y + 0.5f; }
        Z = treeposition.z;
        if (Z > treeposition.z) { Z = Z - 0.5f; }
        else if (Z <= treeposition.z) { Z = Z + 0.5f; }
    }
}
