using System;
using UnityEngine;

[Serializable]
public class PillarSaveData
{
    [SerializeField]
    public Vector3 PillarPosition;

    [SerializeField]
    public Quaternion PillarRotation;
    
    [SerializeField]
    public int objNum;
}