using System;
using UnityEngine;

[Serializable]
public class PlaneSaveData
{
    [SerializeField]
    public Vector3 position;
    
    [SerializeField]
    public Quaternion rotate;
}
