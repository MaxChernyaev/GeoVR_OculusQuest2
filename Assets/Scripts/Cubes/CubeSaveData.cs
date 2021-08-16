using System;
using UnityEngine;

[Serializable]
public class CubeSaveData
{
    [SerializeField]
    public Vector3 cubePosition;

    [SerializeField]
    public Quaternion cubeRotation;
    
    [SerializeField]
    public int objNum;
}