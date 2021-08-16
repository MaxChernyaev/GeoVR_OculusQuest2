using System;
using UnityEngine;

[Serializable]
public class RedLineSaveData
{
    [SerializeField]
    public Vector3 RedLinePosition;

    [SerializeField]
    public Quaternion RedLineRotation;
    
    [SerializeField]
    public float sizeRedline;
}