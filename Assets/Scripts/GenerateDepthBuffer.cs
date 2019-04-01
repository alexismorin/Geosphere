using System;
using UnityEngine;
     
[ExecuteInEditMode]
public class GenerateDepthBuffer : MonoBehaviour
{
public void Start ()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }
}