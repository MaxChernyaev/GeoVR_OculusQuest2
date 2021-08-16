using System;
using UnityEngine;

[Serializable]
public class HatchwaySaveData
{
    [SerializeField]
    public Vector3 HatchwayPosition;

    [SerializeField]
    public Quaternion HatchwayRotation;
    
    [SerializeField]
    public int objNum;
}