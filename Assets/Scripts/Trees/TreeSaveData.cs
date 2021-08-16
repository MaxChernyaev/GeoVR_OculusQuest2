using System;
using UnityEngine;

[Serializable]
public class TreeSaveData
{
    [SerializeField]
    public Vector3 TreePosition;

    [SerializeField]
    public Quaternion TreeRotation;
    
    [SerializeField]
    public int objNum;
}