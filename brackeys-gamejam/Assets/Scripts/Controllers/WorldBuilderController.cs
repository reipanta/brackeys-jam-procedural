using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using WorldGeneration;

public class WorldBuilderController : MonoBehaviour
{
    // These will be passed to the new BasicWorldFactory object constructor to change internal values
    public int xSize = 30;
    public int zSize = 30;
    public BasicWorldFactory Instance;

    private void Awake()
    {
        // Link to the mesh on this GameObject since the manager derives from MonoBehaviour
        Mesh _mesh = GetComponent<MeshFilter>().mesh;
        // Initializing Instance object with constructor values
        Instance = new BasicWorldFactory(
            mesh: _mesh,
            xSize: xSize,
            zSize: zSize);

        // Deploying functions to generate terrain
        Instance.CreateShape();
        Instance.UpdateMesh();
    }
}