using System;
using UnityEngine;

[Serializable]
public class RadarogramSaveData
{
    [SerializeField]
    public Vector3 RadarogramPosition;

    [SerializeField]
    public Quaternion RadarogramRotation;
    
    [SerializeField]
    public float scaleline;
}