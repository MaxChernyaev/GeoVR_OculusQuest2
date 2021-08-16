using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// скрипт для сбора объектов и их инфы
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [SerializeField] private Saver _saver;
    //private Plane[] planes;
    private Cube[] cubes;
    private Pillar[] pillars;
    private Radarogram[] radarograms;
    private RedLine[] redlines;
    private GreenLine[] greenlines;
    private RedFlag[] redFlags;
    private MyTree[] trees;
    private WhiteFlag[] whiteFlags;
    private Hatchway[] hatchways;
    //public Text TextLog;  // лог на canvas
    private GameObject ObjectToSpawn; // объект, который будем ставить на сцену
    [SerializeField] private GameObject PillarPrefab;
    [SerializeField] private GameObject RadarogramPrefab;
    [SerializeField] private GameObject RedLinePrefab;
    [SerializeField] private GameObject GreenLinePrefab;
    [SerializeField] private GameObject RedFlagPrefab;
    [SerializeField] private GameObject TreePrefab;
    [SerializeField] private GameObject WhiteFlagPrefab;
    [SerializeField] private GameObject HatchwayPrefab;
    private GameObject FindObject;

    private void Start()
    {
        //planes = GetComponentsInChildren<Plane>();
        //cubes = GetComponentsInChildren<Cube>();

        //LoadObject();
    }

    public void SaveButton()
    {
        // GameObject[] allGo = FindObjectsOfType<GameObject>();
        // cubes = allGo.Select(go => go.GetComponent<Cube>()).Where(i => i != null).ToArray();

        GameObject[] allGo = FindObjectsOfType<GameObject>();
        pillars = allGo.Select(go => go.GetComponent<Pillar>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        radarograms = allGo.Select(go => go.GetComponent<Radarogram>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        redlines = allGo.Select(go => go.GetComponent<RedLine>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        greenlines = allGo.Select(go => go.GetComponent<GreenLine>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        redFlags = allGo.Select(go => go.GetComponent<RedFlag>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        trees = allGo.Select(go => go.GetComponent<MyTree>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        whiteFlags = allGo.Select(go => go.GetComponent<WhiteFlag>()).Where(i => i != null).ToArray();

        allGo = FindObjectsOfType<GameObject>();
        hatchways = allGo.Select(go => go.GetComponent<Hatchway>()).Where(i => i != null).ToArray();
        SaveObjects();
    }
    public void LoadButton()
    {
        LoadObject();
    }

    // private void OnDisable()
    // {
    //     SaveObjects();
    // }

    // void OnDestroy()
    // {
    //     SaveObjects();
    // }

    public JsonRadarogramReader.CommonData LoadDataJson()
    {
        JsonRadarogramReader.CommonData JsonData = _saver.LoadJsonRadarogram();
        return JsonData;
    }

    private void LoadObject()
    {
        CommonSaveData commonSaveData = _saver.Load();
        FindObject = GameObject.Find("BasePlane(Clone)");

        if (commonSaveData == null)
        {
            Debug.Log("commonSaveData is null");
            //TextLog.text = "commonSaveData is null";
            return;
        }


        // for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.cubesList.Count; i++)
        // {
        //     Instantiate(CubeToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        // }
        // GameObject[] allGo = FindObjectsOfType<GameObject>();
        // cubes = allGo.Select(go => go.GetComponent<Cube>()).Where(i => i != null).ToArray();
        // for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.cubesList.Count; i++)
        // {
        //     cubes[i].SetData(commonSaveData.cubesList[i]);
        // }


        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.PillarList.Count; i++)
        {
            ObjectToSpawn = PillarPrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        GameObject[] allGo = FindObjectsOfType<GameObject>();
        pillars = allGo.Select(go => go.GetComponent<Pillar>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.PillarList.Count; i++)
        {
            pillars[i].SetData(commonSaveData.PillarList[i]);
        }


        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.RadarogramList.Count; i++)
        {
            ObjectToSpawn = RadarogramPrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        radarograms = allGo.Select(go => go.GetComponent<Radarogram>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.RadarogramList.Count; i++)
        {
            radarograms[i].SetData(commonSaveData.RadarogramList[i]);
        }

        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.RedLineList.Count; i++)
        {
            ObjectToSpawn = RedLinePrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        redlines = allGo.Select(go => go.GetComponent<RedLine>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.RedLineList.Count; i++)
        {
            redlines[i].SetData(commonSaveData.RedLineList[i]);
        }

        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.GreenLineList.Count; i++)
        {
            ObjectToSpawn = GreenLinePrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        greenlines = allGo.Select(go => go.GetComponent<GreenLine>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.GreenLineList.Count; i++)
        {
            greenlines[i].SetData(commonSaveData.GreenLineList[i]);
        }

        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.RedFlagList.Count; i++)
        {
            ObjectToSpawn = RedFlagPrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        redFlags = allGo.Select(go => go.GetComponent<RedFlag>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.RedFlagList.Count; i++)
        {
            redFlags[i].SetData(commonSaveData.RedFlagList[i]);
        }


        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.TreeList.Count; i++)
        {
            ObjectToSpawn = TreePrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        trees = allGo.Select(go => go.GetComponent<MyTree>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.TreeList.Count; i++)
        {
            trees[i].SetData(commonSaveData.TreeList[i]);
        }


        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.WhiteFlagList.Count; i++)
        {
            ObjectToSpawn = WhiteFlagPrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        whiteFlags = allGo.Select(go => go.GetComponent<WhiteFlag>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.WhiteFlagList.Count; i++)
        {
            whiteFlags[i].SetData(commonSaveData.WhiteFlagList[i]);
        }


        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.HatchwayList.Count; i++)
        {
            ObjectToSpawn = HatchwayPrefab;
            Instantiate(ObjectToSpawn, Vector3.zero, Quaternion.identity, FindObject.transform);
        }
        allGo = FindObjectsOfType<GameObject>();
        hatchways = allGo.Select(go => go.GetComponent<Hatchway>()).Where(i => i != null).ToArray();
        for (int i = 0; /*i < cubes.Length &&*/ i < commonSaveData.HatchwayList.Count; i++)
        {
            hatchways[i].SetData(commonSaveData.HatchwayList[i]);
        }
    }

    private void SaveObjects()
    {
        //собираем данные со всех объектов
        //LINQ цикл в одну строчку чтобы со всех взять GetData()
        CommonSaveData commonSaveData = new CommonSaveData()
        {
            PillarList = pillars.Select(pillar => pillar.GetData()).ToList(),
            RadarogramList = radarograms.Select(radarogram => radarogram.GetData()).ToList(),
            RedLineList = redlines.Select(redline => redline.GetData()).ToList(),
            RedFlagList = redFlags.Select(redFlag => redFlag.GetData()).ToList(),
            TreeList = trees.Select(tree => tree.GetData()).ToList(),
            WhiteFlagList = whiteFlags.Select(whiteFlag => whiteFlag.GetData()).ToList(),
            HatchwayList = hatchways.Select(hatchway => hatchway.GetData()).ToList(),
            GreenLineList = greenlines.Select(greenline =>  greenline.GetData()).ToList()
        };
        _saver.Save(commonSaveData);
    }
}