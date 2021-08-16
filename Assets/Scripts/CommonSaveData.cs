using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CommonSaveData
{
    // [SerializeField]
    // public List<PlaneSaveData> planesList;
    
    // [SerializeField]
    // public List<CubeSaveData> cubesList;
    
    //и так далее для каждого вида данных

    [SerializeField]
    public List<PillarSaveData> PillarList;

    [SerializeField]
    public List<RadarogramSaveData> RadarogramList;

    [SerializeField]
    public List<RedLineSaveData> RedLineList;

    [SerializeField]
    public List<GreenLineSaveData> GreenLineList;

    [SerializeField]
    public List<RedFlagSaveData> RedFlagList;

    [SerializeField]
    public List<TreeSaveData> TreeList;

    [SerializeField]
    public List<WhiteFlagSaveData> WhiteFlagList;

    [SerializeField]
    public List<HatchwaySaveData> HatchwayList;
}