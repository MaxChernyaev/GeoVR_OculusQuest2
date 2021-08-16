using System;
using UnityEngine;

[Serializable]
public class RedFlagSaveData
{
    [SerializeField]
    public Vector3 RedFlagPosition;

    [SerializeField]
    public Quaternion RedFlagRotation;
    
    [SerializeField]
    public int objNum;
}