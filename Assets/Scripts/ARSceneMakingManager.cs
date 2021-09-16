using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;
using System.Net;
using System.IO;
// ПРОЕКТ ДЛЯ OCULUS QUEST 2

/// <summary>
/// Этот скрипт создаёт плоскость, если найден якорь (базовая точка), затем
/// размешает маркер на плоскости и позволяет создавать объекты в нужных местах с линиями между ними
/// т.е. реализует процесс создания AR сцены
/// </summary>

public class ARSceneMakingManager : MonoBehaviour
{
    // public GameObject parentPlane; // плоскость-родитель для всех остальных объектов
    // private GameObject PlaneMarkerPrefab; // маркер (картинка круга)
    // [SerializeField] private GameObject MarkerCircle;
    // [SerializeField] private GameObject MarkerCircle_White;
    // [SerializeField] private GameObject MarkerPoint;
    // private bool SwapColorMarker = false;
    // private GameObject ObjectToSpawn; // объект, который будем ставить на сцену
    // [SerializeField] private GameObject PillarPrefab;
    // [SerializeField] private GameObject RedFlagPrefab;
    // [SerializeField] private GameObject WhiteFlagPrefab;
    // [SerializeField] private GameObject TreePrefab;
    // [SerializeField] private GameObject HatchwayPrefab;
    // [SerializeField] private GameObject DeleteObjPrefab;
    // private Vector3 point; // точка перечения луча и плоскости
    [SerializeField] private Text TextLog;  // лог на VRcanvas
    [SerializeField] public GameObject CanvasMenu;
    public bool CanvasMenuActive = false;
    private string SummTextLog;

    // private ARTrackedImageManager myARTrackedImageManager; // чтобы выключать/выключать компонент ARTrackedImageManager
    // private GameObject FindObject; // найденный объект (для переименования)
    private GameObject FindPlane; // найденная плоскость
    // private GameObject FindPlaneTEST; // найденная плоскость
    // private int Flag_white_num  = 0; // порядковый номер созданного белого флага
    // private int numObject = 0;
    // private float numObjectBoundaryRadarogramFlag = 0;
    // private float AddParallelNum = 0;
    // private bool PermissionIncrement = true;
    // LineRenderer lineRenderer; // для отрисовки линий между объектами (треки)
    // private bool SplitCameraON; // true - выбран VR режим, false - выбран обычный режим
    // private int RayParameter; // число (2/4) означающее часть экрана откуда начинается луч
    // private int NewI = 0;
    // private ARPlaneManager planeManager;
    // [SerializeField] private GameObject inscriptionTable; // надпись во всех экран в начале
    // [SerializeField] private GameObject radarogram; // радарограмма
    [SerializeField] private GameObject radarogramPrefab;
    // [SerializeField] private GameObject whiteLine; // белая линия между белыми флагами
    // [SerializeField] private GameObject redLine; // красная линия между красными флагами
    // private bool BoundaryRadarogramFlag = false;
    // private Vector3 euler;
    // private bool PermissionInst = false; // Разрешение на установку объекта
    // [SerializeField] private GameObject BasePointGizmo;
    // //int intcheck = 0;
    // private GameObject FindButton = null;
    // private GameObject LastFindButton;
    // // private int SelectObj; // Какой из объектов для установки на сцену выбран (1-столбик, 2-красный флаг, 3-радарограмма, 4-дерево)
    // // private int SelectTypeObj; // Какой тип установки объектов выбран (1-одиночные(отдельные)объекты, 2-комплекс объектов)
    // [SerializeField] private GameObject ARPlanePrefab;
    // // private int timeTracking = 0;
    // [SerializeField] private GameObject ARCamera;
    // [SerializeField] private ARMenu ARMenuScript;
    // private bool checkDeleteObj = false;
    // private RaycastHit hit;

    //[SerializeField] private GameObject Line; // поставь нужный префаб линии

    //[SerializeField] private GameObject CatObj;
    //[SerializeField] private GameObject TestText;

    //[SerializeField] private ObjectManager ObjectManagerScript;
    WebClient webClient = new WebClient();
    JsonRadarogramReader.CommonData myJsonRadarogramData;
    private double lastTime;

    // private int WhiteFlagNum1 = 0;
    // private int WhiteFlagNum2 = 0;
    // private int WhiteFlagNum3 = 0;
    // private int WhiteFlagNum4 = 0;


        // попытка переделать всё на массивы
        List<GameObject> FirstObgList = new List<GameObject>(); // список начальных точек
        List<GameObject> SecondObgList = new List<GameObject>(); // список конечных точек
        List<GameObject> RGList = new List<GameObject>(); // список радарограмм


    GameObject FirstObg_1, FirstObg_2, FirstObg_3, FirstObg_4, FirstObg_5, FirstObg_6, FirstObg_7, FirstObg_8, FirstObg_9, FirstObg_10;
    Vector3 FirstPosition_1, FirstPosition_2, FirstPosition_3, FirstPosition_4, FirstPosition_5, FirstPosition_6, FirstPosition_7, FirstPosition_8, FirstPosition_9, FirstPosition_10;
    GameObject SecondObg_1, SecondObg_2, SecondObg_3, SecondObg_4, SecondObg_5, SecondObg_6, SecondObg_7, SecondObg_8, SecondObg_9, SecondObg_10;
    Vector3 SecondPosition_1, SecondPosition_2, SecondPosition_3, SecondPosition_4, SecondPosition_5, SecondPosition_6, SecondPosition_7, SecondPosition_8, SecondPosition_9, SecondPosition_10;
    GameObject RG_1, RG_2, RG_3, RG_4, RG_5, RG_6, RG_7, RG_8, RG_9, RG_10;
    Vector3 normalizedDirection_1, normalizedDirection_2, normalizedDirection_3, normalizedDirection_4, normalizedDirection_5, normalizedDirection_6, normalizedDirection_7, normalizedDirection_8, normalizedDirection_9, normalizedDirection_10;
    Vector3 MyCenter_1, MyCenter_2, MyCenter_3, MyCenter_4, MyCenter_5, MyCenter_6, MyCenter_7, MyCenter_8, MyCenter_9, MyCenter_10;
    UnityEngine.XR.InputDevice RightDevice;
    UnityEngine.XR.InputDevice LeftDevice;
    [SerializeField] private GameObject InputFieldIP;
    private bool button_ban = false;

    // массив самих объектов - радарограмм
    private GameObject[] RGArray = new GameObject[11]; // используется с 1 до 10
    // массив флагов активности радарограмм
    private bool[] RGActiveArray = new bool[11]; // используется с 1 до 10

    // флаг, указывающий на то, что радарограммы были найдены
    private bool RadarogramWereFound = false;

    private int RadarogramCounter = 0;
    private string WebServerIP = "http://192.168.110.33:8000";
    //private string WebServerIP = "http://192.168.137.1:8000";
    //private string WebServerIP = "http://192.168.31.8:8000";
    private bool FlagSuccessfulConnect = false;
    private string CustomWebServerIP = null;

    private TouchScreenKeyboard overlayKeyboard;
    public static string inputTextIP = "";

    private int frames = 0;

    [SerializeField] private string teststring = "data.json";
    private string FULLteststring;
    public string newSaveDataPath {
        get
        {
            if (FULLteststring == null)
            {
                FULLteststring = Path.Combine(Application.persistentDataPath, teststring);
            }

            return FULLteststring;
        }
    }

    public void Init()
    {
        webClient.DownloadFile(WebServerIP + "/scene.json", Application.persistentDataPath + "/scene.json"); // скачиваю новый scene.json
        GameObject.Find("scripts").GetComponent<ObjectManager>().LoadButton(); // Загружаю сцену из файла scene.json
        Directory.CreateDirectory(Application.persistentDataPath + "/RadarogramTexture");

        if(File.Exists(Application.persistentDataPath + "/data.json"))
        {
            // DATA.JSON найден!
            File.Delete(newSaveDataPath);
            //myJsonRadarogramData = GameObject.Find("scripts").GetComponent<ObjectManager>().LoadDataJson(); // Читаю data.json
        }
        // Debug.Log(myJsonRadarogramData.time);
        // Debug.Log(myJsonRadarogramData.images.jpg1[0]);

        // Ищу основные объекты на сцене
        FindWhiteFlag();
        
        // Ставлю заготовки под радарограммы, которые потом можно текстурировать
        InstallBlank();
    }

    void Start()
    {   
        CanvasMenu.SetActive(false);

        for(int i = 1; i < 11; i++)
        {
            RGActiveArray[i] = true;
        }

        Init();
    }

    void Update()
    {
        OculusHandController(); // обработка VR контроллеров, которые в руках

        if (FlagSuccessfulConnect)
        {
            //SummTextLog += "Пытаюсь поставить радарограммы";
            //TextLog.text = SummTextLog;
            InstallRadarogram(); // установка радарограмм, скачанных с Ryven web-сервера
        }

        // ДЛЯ ПРОВЕДЕНИЯ ЛИНИЙ МЕЖДУ ОБЪЕКТАМИ
        // если найдены 2 границы из красных флагов
        // if (GameObject.Find("Flag_red(Clone)1") && GameObject.Find("Flag_red(Clone)2"))
        // {
        //     Vector3 FirstPosition = GameObject.Find("Flag_red(Clone)1").transform.position;
        //     Vector3 SecondPosition = GameObject.Find("Flag_red(Clone)2").transform.position;
        //     Vector3 DirectionVector = SecondPosition - FirstPosition;
        //     float MyMagnitude = DirectionVector.magnitude;
        //     Vector3 MyCenter = Vector3.Lerp(FirstPosition,SecondPosition,0.5f);
        //     GameObject newRLine = Instantiate(Line, MyCenter, Quaternion.identity, FindPlane.transform);
        //     newRLine.transform.LookAt(SecondPosition);
        //     newRLine.GetComponent<SpriteRenderer>().size = new Vector2(MyMagnitude ,0.31f);
        //     newRLine.transform.Rotate(90,newRLine.transform.rotation.y + 90,0);
        //     //newWLine.transform.localScale = new Vector3(0.01f, 1f, 0.1f * MyMagnitude);
        //     GameObject.Find("Flag_red(Clone)1").name = "Flag_red(Clone)";
        //     GameObject.Find("Flag_red(Clone)2").name = "Flag_red(Clone)";
        // }

        // ДЛЯ ИЗМЕРЕНИЯ РАССТОЯНИЯ МЕЖДУ ОБЪЕКТАМИ
        // if (GameObject.Find("point11") && GameObject.Find("point22"))
        // {
        //     Vector3 FirstPosition = GameObject.Find("point11").transform.position;
        //     Vector3 SecondPosition = GameObject.Find("point22").transform.position;
        //     Vector3 DirectionVector = SecondPosition - FirstPosition;
        //     float MyMagnitude = DirectionVector.magnitude;
        //     //Debug.Log("DirectionVector: " + DirectionVector.ToString());
        //     Debug.Log("MyMagnitude: " + MyMagnitude.ToString());
        // }
    }

    // Сбор основных объектов сцены
    private void FindWhiteFlag()
    {
        GameObject[] allGo = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allGo)
        {
            if (go.name == "Flag_white(Clone)")
            {
                if (go.transform.Find("Number").GetComponent<TextMesh>().text == "1")
                {
                    //SecondObgList.Insert(0, go);
                    RadarogramCounter++;
                    FirstObg_1 = go;
                    FirstPosition_1 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                    FlagSuccessfulConnect = true;
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "1,1")
                {
                    SecondObg_1 = go;
                    SecondPosition_1 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "2")
                {
                    RadarogramCounter++;
                    FirstObg_2 = go;
                    FirstPosition_2 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "2,1")
                {
                    SecondObg_2 = go;
                    SecondPosition_2 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "3")
                {
                    RadarogramCounter++;
                    FirstObg_3 = go;
                    FirstPosition_3 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "3,1")
                {
                    SecondObg_3 = go;
                    SecondPosition_3 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "4")
                {
                    RadarogramCounter++;
                    FirstObg_4 = go;
                    FirstPosition_4 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "4,1")
                {
                    SecondObg_4 = go;
                    SecondPosition_4 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "5")
                {
                    RadarogramCounter++;
                    FirstObg_5 = go;
                    FirstPosition_5 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "5,1")
                {
                    SecondObg_5 = go;
                    SecondPosition_5 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "6")
                {
                    RadarogramCounter++;
                    FirstObg_6 = go;
                    FirstPosition_6 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "6,1")
                {
                    SecondObg_6 = go;
                    SecondPosition_6 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "7")
                {
                    RadarogramCounter++;
                    FirstObg_7 = go;
                    FirstPosition_7 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "7,1")
                {
                    SecondObg_7 = go;
                    SecondPosition_7 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "8")
                {
                    RadarogramCounter++;
                    FirstObg_8 = go;
                    FirstPosition_8 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "8,1")
                {
                    SecondObg_8 = go;
                    SecondPosition_8 = go.transform.position;
                }

                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "9")
                {
                    RadarogramCounter++;
                    FirstObg_9 = go;
                    FirstPosition_9 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "9,1")
                {
                    SecondObg_9 = go;
                    SecondPosition_9 = go.transform.position;
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "10")
                {
                    RadarogramCounter++;
                    FirstObg_10 = go;
                    FirstPosition_10 = go.transform.position;
                    //go.transform.Find("Canvas").gameObject.SetActive(true);
                }
                else if(go.transform.Find("Number").GetComponent<TextMesh>().text == "10,1")
                {
                    SecondObg_10 = go;
                    SecondPosition_10 = go.transform.position;
                }

            }
            else if (go.name == "BasePlane(Clone)")
            {
                FindPlane = go;
            }
        }
    }

    // установка заготовок под радарограммы, которые потом можно текстурировать
    private void InstallBlank()
    {
        if(FirstObg_1 != null)
        {
            Vector3 DirectionVector_1 = SecondPosition_1 - FirstPosition_1;
            normalizedDirection_1 = DirectionVector_1.normalized;
            SecondPosition_1 = FirstPosition_1 + (normalizedDirection_1 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_1.transform.position = SecondPosition_1; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_1 = Vector3.Lerp(FirstPosition_1, SecondPosition_1, 0.5f);
            RG_1 = Instantiate(radarogramPrefab, MyCenter_1, Quaternion.identity, FindPlane.transform);
            RG_1.transform.LookAt(SecondPosition_1);
            RG_1.transform.Rotate(0,-90,0);
            RG_1.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_2 != null)
        {
            Vector3 DirectionVector_2 = SecondPosition_2 - FirstPosition_2;
            normalizedDirection_2 = DirectionVector_2.normalized;
            SecondPosition_2 = FirstPosition_2 + (normalizedDirection_2 * 5/* * myJsonRadarogramData.images.jpg1[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_2.transform.position = SecondPosition_2; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_2 = Vector3.Lerp(FirstPosition_2, SecondPosition_2, 0.5f);
            RG_2 = Instantiate(radarogramPrefab, MyCenter_2, Quaternion.identity, FindPlane.transform);
            RG_2.transform.LookAt(SecondPosition_2);
            RG_2.transform.Rotate(0,-90,0);
            RG_2.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_3 != null)
        {
            Vector3 DirectionVector_3 = SecondPosition_3 - FirstPosition_3;
            normalizedDirection_3 = DirectionVector_3.normalized;
            SecondPosition_3 = FirstPosition_3 + (normalizedDirection_3 * 5/* * myJsonRadarogramData.images.jpg2[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_3.transform.position = SecondPosition_3; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_3 = Vector3.Lerp(FirstPosition_3, SecondPosition_3, 0.5f);
            RG_3 = Instantiate(radarogramPrefab, MyCenter_3, Quaternion.identity, FindPlane.transform);
            RG_3.transform.LookAt(SecondPosition_3);
            RG_3.transform.Rotate(0,-90,0);
            RG_3.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_4 != null)
        {
            Vector3 DirectionVector_4 = SecondPosition_4 - FirstPosition_4;
            normalizedDirection_4 = DirectionVector_4.normalized;
            SecondPosition_4 = FirstPosition_4 + (normalizedDirection_4 * 5/* * myJsonRadarogramData.images.jpg3[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_4.transform.position = SecondPosition_4; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_4 = Vector3.Lerp(FirstPosition_4, SecondPosition_4, 0.5f);
            RG_4 = Instantiate(radarogramPrefab, MyCenter_4, Quaternion.identity, FindPlane.transform);
            RG_4.transform.LookAt(SecondPosition_4);
            RG_4.transform.Rotate(0,-90,0);
            RG_4.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_5 != null)
        {
            Vector3 DirectionVector_5 = SecondPosition_5 - FirstPosition_5;
            normalizedDirection_5 = DirectionVector_5.normalized;
            SecondPosition_5 = FirstPosition_5 + (normalizedDirection_5 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_5.transform.position = SecondPosition_5; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_5 = Vector3.Lerp(FirstPosition_5, SecondPosition_5, 0.5f);
            RG_5 = Instantiate(radarogramPrefab, MyCenter_5, Quaternion.identity, FindPlane.transform);
            RG_5.transform.LookAt(SecondPosition_5);
            RG_5.transform.Rotate(0,-90,0);
            RG_5.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_6 != null)
        {
            Vector3 DirectionVector_6 = SecondPosition_6 - FirstPosition_6;
            normalizedDirection_6 = DirectionVector_6.normalized;
            SecondPosition_6 = FirstPosition_6 + (normalizedDirection_6 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_6.transform.position = SecondPosition_6; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_6 = Vector3.Lerp(FirstPosition_6, SecondPosition_6, 0.5f);
            RG_6 = Instantiate(radarogramPrefab, MyCenter_6, Quaternion.identity, FindPlane.transform);
            RG_6.transform.LookAt(SecondPosition_6);
            RG_6.transform.Rotate(0,-90,0);
            RG_6.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_7 != null)
        {
            Vector3 DirectionVector_7 = SecondPosition_7 - FirstPosition_7;
            normalizedDirection_7 = DirectionVector_7.normalized;
            SecondPosition_7 = FirstPosition_7 + (normalizedDirection_7 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_7.transform.position = SecondPosition_7; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_7 = Vector3.Lerp(FirstPosition_7, SecondPosition_7, 0.5f);
            RG_7 = Instantiate(radarogramPrefab, MyCenter_7, Quaternion.identity, FindPlane.transform);
            RG_7.transform.LookAt(SecondPosition_7);
            RG_7.transform.Rotate(0,-90,0);
            RG_7.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_8 != null)
        {
            Vector3 DirectionVector_8 = SecondPosition_8 - FirstPosition_8;
            normalizedDirection_8 = DirectionVector_8.normalized;
            SecondPosition_8 = FirstPosition_8 + (normalizedDirection_8 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_8.transform.position = SecondPosition_8; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_8 = Vector3.Lerp(FirstPosition_8, SecondPosition_8, 0.5f);
            RG_8 = Instantiate(radarogramPrefab, MyCenter_8, Quaternion.identity, FindPlane.transform);
            RG_8.transform.LookAt(SecondPosition_8);
            RG_8.transform.Rotate(0,-90,0);
            RG_8.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_9 != null)
        {
            Vector3 DirectionVector_9 = SecondPosition_9 - FirstPosition_9;
            normalizedDirection_9 = DirectionVector_9.normalized;
            SecondPosition_9 = FirstPosition_9 + (normalizedDirection_9 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_9.transform.position = SecondPosition_9; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_9 = Vector3.Lerp(FirstPosition_9, SecondPosition_9, 0.5f);
            RG_9 = Instantiate(radarogramPrefab, MyCenter_9, Quaternion.identity, FindPlane.transform);
            RG_9.transform.LookAt(SecondPosition_9);
            RG_9.transform.Rotate(0,-90,0);
            RG_9.transform.position += new Vector3(0, 1.5f, 0);
        }

        if(FirstObg_10 != null)
        {
            Vector3 DirectionVector_10 = SecondPosition_10 - FirstPosition_10;
            normalizedDirection_10 = DirectionVector_10.normalized;
            SecondPosition_10 = FirstPosition_10 + (normalizedDirection_10 * 5/* * myJsonRadarogramData.images.jpg0[0]/100*/); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
            SecondObg_10.transform.position = SecondPosition_10; // отодвигаем второй белый флаг, чтобы влезла радарограмма
            MyCenter_10 = Vector3.Lerp(FirstPosition_10, SecondPosition_10, 0.5f);
            RG_10 = Instantiate(radarogramPrefab, MyCenter_10, Quaternion.identity, FindPlane.transform);
            RG_10.transform.LookAt(SecondPosition_10);
            RG_10.transform.Rotate(0,-90,0);
            RG_10.transform.position += new Vector3(0, 1.5f, 0);
        }
    }

    private void InstallRadarogram()
    {
        //TextLog.text = frames.ToString();
        frames++;
        if(frames == 10)
        {
            //frames = 0;
            if(File.Exists(Application.persistentDataPath + "/data.json"))
            {
                // try
                // {
                    lastTime = myJsonRadarogramData.time;            // запоминаю какое время было в прошлом data.json файле
                    //SummTextLog += " data+ ";
                    //TextLog.text = SummTextLog;
                // }
                // catch(Exception e)
                // {
                //     print("Exception thrown " + e.Message);
                // }
            }
            // try
            // {

            webClient.DownloadFileAsync(new Uri(WebServerIP + "/data.json"), Application.persistentDataPath + "/data.json"); // скачиваю новый data.json
            //SummTextLog += " Async ";
            //TextLog.text = SummTextLog;
        }
        if(frames == 30)
        {
            frames = 0;
            // }
            // catch(Exception e)
            // {
            //     print("Exception thrown " + e.Message);
            // }
            //myJsonRadarogramData = GameObject.Find("scripts").GetComponent<ObjectManager>().LoadDataJson(); // Читаю data.json
            if(File.Exists(Application.persistentDataPath + "/data.json"))
            {   
                //SummTextLog += " DC ";
                //TextLog.text = SummTextLog;
                myJsonRadarogramData = GameObject.Find("scripts").GetComponent<ObjectManager>().LoadDataJson(); // Читаю data.json
                //SummTextLog += " ReadD ";
                //TextLog.text = SummTextLog;

                if(myJsonRadarogramData.time != lastTime) // Если прилетели новые радарограммы
                {
                    //SummTextLog += " NR ";
                    //TextLog.text = SummTextLog;
                    
                    try
                    {
                        webClient.DownloadFile(WebServerIP + "/0.png", Application.persistentDataPath + "/RadarogramTexture/0.png");
                        webClient.DownloadFile(WebServerIP + "/1.png", Application.persistentDataPath + "/RadarogramTexture/1.png");
                        webClient.DownloadFile(WebServerIP + "/2.png", Application.persistentDataPath + "/RadarogramTexture/2.png");
                        webClient.DownloadFile(WebServerIP + "/3.png", Application.persistentDataPath + "/RadarogramTexture/3.png");
                        webClient.DownloadFile(WebServerIP + "/4.png", Application.persistentDataPath + "/RadarogramTexture/4.png");
                        webClient.DownloadFile(WebServerIP + "/5.png", Application.persistentDataPath + "/RadarogramTexture/5.png");
                        webClient.DownloadFile(WebServerIP + "/6.png", Application.persistentDataPath + "/RadarogramTexture/6.png");
                        webClient.DownloadFile(WebServerIP + "/7.png", Application.persistentDataPath + "/RadarogramTexture/7.png");
                        webClient.DownloadFile(WebServerIP + "/8.png", Application.persistentDataPath + "/RadarogramTexture/8.png");
                        webClient.DownloadFile(WebServerIP + "/9.png", Application.persistentDataPath + "/RadarogramTexture/9.png");
                    }
                    catch (System.Exception)
                    {
                        //TextLog.text = "Exception thrown " + e.Message;
                        //throw;
                    }

                    SecondPosition_1 = FirstPosition_1 + (normalizedDirection_1 * myJsonRadarogramData.images.jpg0[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    // Debug.Log("ПЕРЕДВИНУЛ на SecondPosition_1 :" + SecondPosition_1.ToString());
                    SecondObg_1.transform.position = SecondPosition_1; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_1 = Vector3.Lerp(FirstPosition_1, SecondPosition_1, 0.5f);
                    RG_1.transform.position = MyCenter_1;
                    RG_1.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/0.png");
                    RG_1.transform.position += new Vector3(0, 1.5f, 0);
                    RG_1.name = "radarogramPrefab_1";

                    SecondPosition_2 = FirstPosition_2 + (normalizedDirection_2 * myJsonRadarogramData.images.jpg1[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_2.transform.position = SecondPosition_2; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_2 = Vector3.Lerp(FirstPosition_2, SecondPosition_2, 0.5f);
                    RG_2.transform.position = MyCenter_2;
                    RG_2.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/1.png");
                    RG_2.transform.position += new Vector3(0, 1.5f, 0);
                    RG_2.name = "radarogramPrefab_2";

                    SecondPosition_3 = FirstPosition_3 + (normalizedDirection_3 * myJsonRadarogramData.images.jpg2[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_3.transform.position = SecondPosition_3; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_3 = Vector3.Lerp(FirstPosition_3, SecondPosition_3, 0.5f);
                    RG_3.transform.position = MyCenter_3;
                    RG_3.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/2.png");
                    RG_3.transform.position += new Vector3(0, 1.5f, 0);
                    RG_3.name = "radarogramPrefab_3";

                    SecondPosition_4 = FirstPosition_4 + (normalizedDirection_4 * myJsonRadarogramData.images.jpg3[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_4.transform.position = SecondPosition_4; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_4 = Vector3.Lerp(FirstPosition_4, SecondPosition_4, 0.5f);
                    RG_4.transform.position = MyCenter_4;
                    RG_4.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/3.png");
                    RG_4.transform.position += new Vector3(0, 1.5f, 0);
                    RG_4.name = "radarogramPrefab_4";

                    SecondPosition_5 = FirstPosition_5 + (normalizedDirection_5 * myJsonRadarogramData.images.jpg4[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_5.transform.position = SecondPosition_5; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_5 = Vector3.Lerp(FirstPosition_5, SecondPosition_5, 0.5f);
                    RG_5.transform.position = MyCenter_5;
                    RG_5.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/4.png");
                    RG_5.transform.position += new Vector3(0, 1.5f, 0);
                    RG_5.name = "radarogramPrefab_5";

                    SecondPosition_6 = FirstPosition_6 + (normalizedDirection_6 * myJsonRadarogramData.images.jpg5[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_6.transform.position = SecondPosition_6; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_6 = Vector3.Lerp(FirstPosition_6, SecondPosition_6, 0.5f);
                    RG_6.transform.position = MyCenter_6;
                    RG_6.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/5.png");
                    RG_6.transform.position += new Vector3(0, 1.5f, 0);
                    RG_6.name = "radarogramPrefab_6";

                    SecondPosition_7 = FirstPosition_7 + (normalizedDirection_7 * myJsonRadarogramData.images.jpg6[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_7.transform.position = SecondPosition_7; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_7 = Vector3.Lerp(FirstPosition_7, SecondPosition_7, 0.5f);
                    RG_7.transform.position = MyCenter_7;
                    RG_7.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/6.png");
                    RG_7.transform.position += new Vector3(0, 1.5f, 0);
                    RG_7.name = "radarogramPrefab_7";

                    SecondPosition_8 = FirstPosition_8 + (normalizedDirection_8 * myJsonRadarogramData.images.jpg7[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_8.transform.position = SecondPosition_8; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_8 = Vector3.Lerp(FirstPosition_8, SecondPosition_8, 0.5f);
                    RG_8.transform.position = MyCenter_8;
                    RG_8.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/7.png");
                    RG_8.transform.position += new Vector3(0, 1.5f, 0);
                    RG_8.name = "radarogramPrefab_8";

                    SecondPosition_9 = FirstPosition_9 + (normalizedDirection_9 * myJsonRadarogramData.images.jpg8[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_9.transform.position = SecondPosition_9; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_9 = Vector3.Lerp(FirstPosition_9, SecondPosition_9, 0.5f);
                    RG_9.transform.position = MyCenter_9;
                    RG_9.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/8.png");
                    RG_9.transform.position += new Vector3(0, 1.5f, 0);
                    RG_9.name = "radarogramPrefab_9";

                    SecondPosition_10 = FirstPosition_10 + (normalizedDirection_10 * myJsonRadarogramData.images.jpg9[0]/100); // умножается на количество метров (длина радарограммы). Делю на 100, потому что сейчас 100 пикселей на метр
                    SecondObg_10.transform.position = SecondPosition_10; // отодвигаем второй белый флаг, чтобы влезла радарограмма
                    MyCenter_10 = Vector3.Lerp(FirstPosition_10, SecondPosition_10, 0.5f);
                    RG_10.transform.position = MyCenter_10;
                    RG_10.GetComponent<SpriteRenderer>().sprite = LoadSprite(Application.persistentDataPath + "/RadarogramTexture/9.png");
                    RG_10.transform.position += new Vector3(0, 1.5f, 0);
                    RG_10.name = "radarogramPrefab_10";
                }
            }
        }
        // очистка логов
        // if (SummTextLog.Length > 120)
        // {
        //     SummTextLog = "";
        // }
    }

    private Sprite LoadSprite(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        if (System.IO.File.Exists(path))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        return null;
    }

    public void InputVRKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        if (overlayKeyboard != null)
        inputTextIP = overlayKeyboard.text;

        //SummTextLog += inputText;
        //TextLog.text = SummTextLog;

    }

    public void ENDInputVRKeyboard()
    {
        //TextLog.text = inputTextIP;
        CustomWebServerIP = inputTextIP;
    }
    public void ENDInputVRKeyboard_NameIP()
    {
        //TextLog.text = inputTextIP;
        GameObject.Find("SaveIPToJSON").GetComponent<AddNewIP>().inputNameIP.text = inputTextIP;
    }
    public void ENDInputVRKeyboard_IPAddress()
    {
        //TextLog.text = inputTextIP;
        GameObject.Find("SaveIPToJSON").GetComponent<AddNewIP>().inputIPAddress.text = inputTextIP;
    }

    public void RestartWithNewIP()
    {
        InputFieldIP.SetActive(false);
        //GameObject.Find("HideRadarogram1").GetComponent<VRButton>().buttonActive = false;
        //WebServerIP = "http://" + CustomWebServerIP + ":8000";
        WebServerIP = "http://" + GameObject.Find("Dropdown").GetComponent<DropDownMenu>().selectItem.text + ":8000";
        GameObject.Find("Main Camera").GetComponent<VRButton>().MainCanvas.SetActive(false); // закрываем оба канваса
        GameObject.Find("Main Camera").GetComponent<VRButton>().SetActiveButton.SetActive(false); // закрываем оба канваса
        StartCoroutine(NewIPCoroutine());
        Init();
    }

    IEnumerator NewIPCoroutine()
    {
        //TextLog.text = "Подключаюсь к Ryven серверу по новому IP: " + CustomWebServerIP;
        //TextLog.text = "Подключаюсь к Ryven серверу по новому IP: " + GameObject.Find("Dropdown").GetComponent<DropDownMenu>().selectItem.text;
        TextLog.text = "Подключаюсь к Ryven серверу по адресу: " + WebServerIP;
        yield return new WaitForSeconds(3f);
        TextLog.text  = "";
    }

    private void OculusHandController() // обработка VR контроллеров, которые в руках
    {
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);

        if(rightHandDevices.Count == 1)
        {
            RightDevice = rightHandDevices[0];
            //Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));
        }

        bool triggerRightValue;
        if (RightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick , out triggerRightValue) && triggerRightValue)
        {
            //TextLog.text =  "Trigger button is pressed.";
            if(button_ban == false)
            {
                CanvasMenuActive = !CanvasMenuActive;
                CanvasMenu.SetActive(CanvasMenuActive);
                StartCoroutine("Button_BAN_05sec");
            }
        }

        if (RightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton , out triggerRightValue) && triggerRightValue)
        {
            if(button_ban == false)
            {
                // скрыть в порядке возрастания
                if(RadarogramWereFound == false) FindRadarogram();
                for(int i = 1; i < 11; i++)
                {
                    if(RGArray[i] != null) //если такая радарограмма вообще существует
                    {
                        if(RGActiveArray[i] == true)
                        {
                            RGActiveArray[i] = false;
                            RGArray[i].SetActive(RGActiveArray[i]);
                            break;
                        }
                    }
                }
                StartCoroutine("Button_BAN_03sec");
            }
        }

        if (RightDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton , out triggerRightValue) && triggerRightValue)
        {
            if(button_ban == false)
            {
                // показывать в порядке убывания
                if(RadarogramWereFound == false) FindRadarogram();
                for(int i = 10; i > 0; i--)
                {
                    if(RGArray[i] != null) //если такая радарограмма вообще существует
                    {
                        if(RGActiveArray[i] == false)
                        {
                            RGActiveArray[i] = true;
                            RGArray[i].SetActive(RGActiveArray[i]);
                            break;
                        }
                    }
                }
                StartCoroutine("Button_BAN_03sec");
            }
        }



        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        if(leftHandDevices.Count == 1)
        {
            LeftDevice = leftHandDevices[0];
            //Debug.Log(string.Format("Device name '{0}' with role '{1}'", device.name, device.role.ToString()));
        }

        bool triggerLeftValue;
        if (LeftDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick , out triggerLeftValue) && triggerLeftValue)
        {
            //TextLog.text =  "Trigger button is pressed.";
            if(button_ban == false)
            {
                CanvasMenuActive = !CanvasMenuActive;
                CanvasMenu.SetActive(CanvasMenuActive);
                StartCoroutine("Button_BAN_05sec");
            }
        }
    }

    IEnumerator Button_BAN_05sec() // баним нажатие на кнопку trigger на 0.5 секунды, чтобы не нажималось много раз (костыль, пока не пойму как правильно)
    {
        button_ban = true;
        yield return new WaitForSeconds(0.5f);
        button_ban = false;
    }
    IEnumerator Button_BAN_03sec() // баним нажатие на кнопку trigger на 0.3 секунды, чтобы не нажималось много раз (костыль, пока не пойму как правильно)
    {
        button_ban = true;
        yield return new WaitForSeconds(0.3f);
        button_ban = false;
    }

    private void FindRadarogram()
    {
        for(int i = 1; i < 11; i++)
        {
            // TextLog.text = "попытка найти " + i.ToString();
            RGArray[i] = GameObject.Find("radarogramPrefab_" + i.ToString());
            if(RGArray[i] != null)
            {
                //TextLog.text = "нашёл радарограмму " + i.ToString();
                RadarogramWereFound = true;
            }
        }
    }












    // НИЖЕ КОД ОСТАВШИЙСЯ ОТ ПРОЕКТА AR


    // public void CatDownload()
    // {
    //     WebClient webClient = new WebClient();
    //     //webClient.DownloadFile("https://medialeaks.ru/wp-content/uploads/2017/10/catbread-03-600x400.jpg", "Assets/cat.jpg");
    //     webClient.DownloadFile("https://medialeaks.ru/wp-content/uploads/2017/10/catbread-04-600x400.jpg", "Assets/cat.jpg");
    // }

    // void FindGizmo()
    // {
    //     GameObject[] allGo = FindObjectsOfType<GameObject>();
    //     foreach (GameObject go in allGo)
    //     {
    //         if (go.name == "Number")
    //         {
    //             go.transform.LookAt(ARCamera.transform);
    //             // //go.transform.rotation = Quaternion.identity;
    //             // float rotateX = go.transform.rotation.eulerAngles.x;
    //             float rotateY = go.transform.rotation.eulerAngles.y;
    //             // float rotateZ = go.transform.rotation.eulerAngles.z;
    //             go.transform.rotation = Quaternion.Euler(0, rotateY+180, 0);
    //         }
    //         if(go.CompareTag("BaseGizmo"))
    //         {   
    //             if (go.GetComponent<ARAnchor>() == null)
    //             {
    //                 go.AddComponent<ARAnchor>();
    //             }
    //             // if (intcheck > 4 && intcheck < 7)
    //             // {
    //             //     Instantiate(BasePointGizmo, go.transform.position, go.transform.rotation);
    //             //     // secondGizmo.name = "NewBasePoint";
    //             // }
    //             // if (intcheck < 8)
    //             // {
    //             //     intcheck++;
    //             // }
    //             inscriptionTable.SetActive(false);
    //             planeManager.enabled = true;
    //             // if(timeTracking < 300)
    //             // {
    //             //     TextLog.text = timeTracking.ToString();
    //             //     timeTracking++;
    //             // }
    //             // if(timeTracking == 300)
    //             // {
    //             //     TextLog.text = "timeTracking == 300";
    //             //     timeTracking++;
    //             //     // GameObject.Find("ARPlane").GetComponent<ARPlaneMeshVisualizer>().enabled = false;
    //             //     // GameObject.Find("ARPlane").GetComponent<MeshRenderer>().enabled = false;
    //             //     // GameObject.Find("ARPlane(Clone)").GetComponent<ARPlaneMeshVisualizer>().enabled = false;
    //             //     // GameObject.Find("ARPlane(Clone)").GetComponent<MeshRenderer>().enabled = false;
    //             //     // ARPlanePrefab.GetComponent<ARPlaneMeshVisualizer>().enabled = false;
    //             //     // ARPlanePrefab.GetComponent<MeshRenderer>().enabled = false;
    //             // }
    //             if ((FindPlane = GameObject.Find("BasePlane(Clone)")) == true) // если плоскость уже создана позиционируем её относительно Gizmo
    //             {   
    //                 if ((DataHolder.CheckBox != true) && (NewI > 10)) // пользователь решил не перераспознавать QR-код
    //                 {
    //                     // включение/выключение компонента ARTrackedImageManager, т.е. отслеживания QR-кода
    //                     // myARTrackedImageManager.enabled = !myARTrackedImageManager.enabled;
    //                     myARTrackedImageManager.enabled = false;
    //                 }
    //                 else
    //                 {
    //                     FindPlane.transform.position = go.transform.position;
    //                     FindPlane.transform.rotation = go.transform.rotation;
    //                     if (NewI <= 10)
    //                     {
    //                         NewI++;
    //                     }
    //                 }
    //             }
    //             else
    //             {
    //                 FindPlaneTEST = Instantiate(parentPlane, go.transform.position, go.transform.rotation, go.transform); // в другом случае, создаём её
    //                 //FindPlaneTEST.AddComponent<ARAnchor>();
    //                 //FindPlaneTEST.GetComponent<ARAnchorManager>().AddAnchor();
    //             }
    //         }
    //     }
    // }

    // // отображение маркера (картинка круга)
    // void ShowMarker()
    // {
    //     //RaycastHit hit;
    //     Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / RayParameter, Screen.height / 2, 0)); // отправляем луч из центра экрана
        
    //     if(Physics.Raycast(ray, out hit) == true) // ели пересечение было записываем в hit
    //     {
    //         point = hit.point; // точка пересечения луча с объектом
    //         //TextLog.text = hit.collider.gameObject.name; // выводим имя объекта на который смотрим
    //         if (hit.collider.gameObject.name == "BasePlanePrefab")
    //         {
    //             PermissionInst = true;
    //             PlaneMarkerPrefab.SetActive(false);
    //             if (SwapColorMarker)
    //             {
    //                 PlaneMarkerPrefab = MarkerCircle;
    //             }
    //             else
    //             {
    //                 PlaneMarkerPrefab = MarkerCircle_White;
    //             }
    //             PlaneMarkerPrefab.SetActive(true);
    //         }
    //         else
    //         {
    //             PermissionInst = false;
    //             PlaneMarkerPrefab.SetActive(false);
    //             PlaneMarkerPrefab = MarkerPoint;
    //             PlaneMarkerPrefab.SetActive(true);

    //             string ObjName = hit.collider.gameObject.name;
    //             switch (ObjName)
    //             {
    //                 case "MenuPlane":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     break;
    //                 case "SaveButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         GameObject.Find("scripts").GetComponent<ObjectManager>().SaveButton();
    //                         // FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "LoadButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         GameObject.Find("scripts").GetComponent<ObjectManager>().LoadButton();
    //                         // FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "SendButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         GameObject.Find("scripts").GetComponent<Saver>().SendSaveJSON();
    //                         // FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "DeleteButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         checkDeleteObj = true;
    //                         ARMenuScript.ARMenuPlanePrefab.gameObject.SetActive(false);
    //                         ARMenuScript.checkMenuActive = false;
    //                     }
    //                     break;
    //                 case "PillarButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         ObjectToSpawn = PillarPrefab;
    //                         BoundaryRadarogramFlag = false;
    //                         GameObject.Find("RadarogramButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RedFlagButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("TreeButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("HatchwayButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "RedFlagButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         ObjectToSpawn = RedFlagPrefab;
    //                         BoundaryRadarogramFlag = false;
    //                         GameObject.Find("PillarButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RadarogramButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("TreeButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("HatchwayButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "RadarogramButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         ObjectToSpawn = WhiteFlagPrefab;
    //                         BoundaryRadarogramFlag = true;
    //                         GameObject.Find("PillarButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RedFlagButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("TreeButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("HatchwayButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "TreeButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         ObjectToSpawn = TreePrefab;
    //                         BoundaryRadarogramFlag = false;
    //                         GameObject.Find("PillarButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RadarogramButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RedFlagButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("HatchwayButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "HatchwayButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         ObjectToSpawn = HatchwayPrefab;
    //                         BoundaryRadarogramFlag = false;
    //                         GameObject.Find("PillarButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RadarogramButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("RedFlagButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         GameObject.Find("TreeButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "OneObjButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         PermissionIncrement = true;
    //                         GameObject.Find("SomeObjButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "SomeObjButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         PermissionIncrement = false;
    //                         numObject++;
    //                         GameObject.Find("OneObjButtonCube").GetComponent<Renderer>().material.color = Color.white;
    //                         FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                     }
    //                     break;
    //                 case "SwapColorMarkerButtonCube":
    //                     LastFindButton = FindButton;
    //                     FindButton = GameObject.Find(ObjName);
    //                     checkDeleteObj = false;
    //                     if ((LastFindButton != FindButton) && (LastFindButton.GetComponent<Renderer>().material.color == Color.grey))
    //                     {
    //                         LastFindButton.GetComponent<Renderer>().material.color = Color.white;
    //                     }
    //                     if (FindButton.GetComponent<Renderer>().material.color != Color.blue)
    //                     {
    //                         FindButton.GetComponent<Renderer>().material.color = Color.grey;
    //                     }
    //                     if (Input.GetKeyDown(KeyCode.LeftShift))
    //                     {
    //                         SwapColorMarker = !SwapColorMarker;
    //                         if (FindButton.GetComponent<Renderer>().material.color == Color.blue)
    //                         {
    //                             FindButton.GetComponent<Renderer>().material.color = Color.white;
    //                         }
    //                         else
    //                         {
    //                             FindButton.GetComponent<Renderer>().material.color = Color.blue;
    //                         }
    //                     }
    //                     break;
    //                 // default:
    //                 //     что-то сделать
    //                 //     break;
    //             }
    //         }
    //         PlaneMarkerPrefab.transform.position = point; // ставим в это место маркер (картинка круга)
    //         PlaneMarkerPrefab.SetActive(true); // показываем маркер
    //     }
    // }

    // // установка новой копии объекта на сцену
    // void InstantiateMyObject()
    // {
    //     // Нажали на нижнюю кнопку на VR контроллере под указательным пальцем
    //     if(/*(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)||*/ (Input.GetKeyDown(KeyCode.LeftShift)) && (PermissionInst == true))
    //     {
    //         // point = new Vector3(1, 1, 1);
    //         var InstObj = Instantiate(ObjectToSpawn, point, ObjectToSpawn.transform.rotation, FindPlane.transform); // ставим в определенное заранее место наш объект + делаем его потомком плоскости
    //         if ((PermissionIncrement == true) && (BoundaryRadarogramFlag == false))
    //         {
    //             numObject++;
    //             InstObj.transform.Find("Number").GetComponent<TextMesh>().text = numObject.ToString();
    //         }
    //         else if(PermissionIncrement == false && (BoundaryRadarogramFlag == false))
    //         {
    //             InstObj.transform.Find("Number").GetComponent<TextMesh>().text = numObject.ToString();
    //         }
    //         else if(BoundaryRadarogramFlag == true)
    //         {   
    //             numObjectBoundaryRadarogramFlag++;
    //             AddParallelNum++;
    //             if(AddParallelNum % 2 == 0)
    //             {
    //                 InstObj.transform.Find("Number").GetComponent<TextMesh>().text = (numObjectBoundaryRadarogramFlag - 0.9f).ToString();
    //                 InstObj.transform.Find("Number").GetComponent<MeshRenderer>().enabled = false; // Выключаю отображение вторых номерков у профилей!
    //                 numObjectBoundaryRadarogramFlag--;
    //             }
    //             else
    //             {
    //                 InstObj.transform.Find("Number").GetComponent<TextMesh>().text = numObjectBoundaryRadarogramFlag.ToString();
    //             }
    //         }
    //         if (InstObj.name == "Flag_white(Clone)") // если свежесозданный объект - граница радарограммы
    //         {
    //             Flag_white_num++;
    //             InstObj.name = ("BoundaryRadarogram" + Flag_white_num); // переименовываем его с добавлением порядкового номера
    //             if ((Flag_white_num % 2) == 0) // сбрасываем счетчик, если втрой флаг был установлен
    //             {
    //                 Flag_white_num = 0;
    //             }
    //         }
    //         if (InstObj.name == "Flag_red(Clone)") // если свежесозданный объект - граница искомого объекта
    //         {
    //             Flag_white_num++;
    //             InstObj.name = ("BoundaryFindObj" + Flag_white_num); // переименовываем его с добавлением порядкового номера
    //             if ((Flag_white_num % 2) == 0) // сбрасываем счетчик, если втрой флаг был установлен
    //             {
    //                 Flag_white_num = 0;
    //             }
    //         }
    //         //FindObject.AddComponent<ARAnchor>(); // вешаю на него компонент якоря в пространстве
    //     }
    // }

    // void PlanesManager()
    // {
    //     foreach (var plane in planeManager.trackables)
    //     {
    //         //TextLog.text = Mathf.Abs(plane.transform.position.y - FindPlaneTEST.transform.position.y).ToString();
    //         if (Mathf.Abs(plane.transform.position.y - FindPlaneTEST.transform.position.y) > 0.2)
    //         {
    //             plane.gameObject.SetActive(false);
    //         }
    //         else
    //         {
    //             // Появилась проблема с тем, что теперь сцена не поворачивается вместе с маркером
    //             euler = plane.transform.rotation.eulerAngles;
    //             euler.y = FindPlaneTEST.transform.rotation.eulerAngles.y;
    //             FindPlane.transform.rotation = Quaternion.Euler(euler);
    //         }
    //     }
    // }

    // void SpawnRadarogram()
    // {   
    //     // если найдены 2 границы из белых флагов
    //     if (GameObject.Find("BoundaryRadarogram1"))
    //     {
    //         // PlaneMarkerPrefab.SetActive(false);
    //         // PlaneMarkerPrefab = MarkerArrow;
    //         // PlaneMarkerPrefab.SetActive(true);
    //         if (GameObject.Find("BoundaryRadarogram2"))
    //         {
    //             // PlaneMarkerPrefab.SetActive(false);
    //             // PlaneMarkerPrefab = MarkerCircle;
    //             // PlaneMarkerPrefab.SetActive(true);
    //             Vector3 FirstPosition = GameObject.Find("BoundaryRadarogram1").transform.position;
    //             Vector3 SecondPosition = GameObject.Find("BoundaryRadarogram2").transform.position;
    //             Vector3 DirectionVector = SecondPosition - FirstPosition;
    //             float MyMagnitude = DirectionVector.magnitude;
    //             //Vector3 normalizedVector = DirectionVector / MyMagnitude;
    //             //float MyDistance = Vector3.Distance(FirstPosition,SecondPosition);
    //             //Vector3 normalizedDirection = DirectionVector.normalized;
    //             //SecondPosition = FirstPosition + (normalizedDirection * 6);
    //             //GameObject.Find("Boundary2").transform.position = SecondPosition;
    //             Vector3 MyCenter = Vector3.Lerp(FirstPosition,SecondPosition,0.5f);

    //             //GameObject RG = Instantiate(radarogram, MyCenter, Quaternion.identity, FindPlane.transform);
    //             GameObject newWLine = Instantiate(whiteLine, MyCenter, Quaternion.identity, FindPlane.transform);
    //             newWLine.transform.LookAt(SecondPosition);
    //             newWLine.transform.localScale = new Vector3(0.01f, 1f, 0.1f * MyMagnitude);
                
    //             //RG.transform.LookAt(SecondPosition);
    //             // Vector3 currentPos = RG.transform.position;
    //             //Vector3 testAddPos = new Vector3(0, 0.745f, 0);
    //             //RG.transform.position += testAddPos;
    //             //RG.transform.Rotate(0,-90,0);
    //             //RG.SetActive(false);
    //             // LineDrawingButton("Boundary1","Boundary2");
    //             GameObject.Find("BoundaryRadarogram1").name = "BoundaryRadarogram";
    //             GameObject.Find("BoundaryRadarogram2").name = "BoundaryRadarogram";
    //         }
    //     }
    //     // если найдены 2 границы из красных флагов
    //     if (GameObject.Find("BoundaryFindObj1") && GameObject.Find("BoundaryFindObj2"))
    //     {
    //         Vector3 FirstPosition = GameObject.Find("BoundaryFindObj1").transform.position;
    //         Vector3 SecondPosition = GameObject.Find("BoundaryFindObj2").transform.position;
    //         Vector3 DirectionVector = SecondPosition - FirstPosition;
    //         float MyMagnitude = DirectionVector.magnitude;
    //         Vector3 MyCenter = Vector3.Lerp(FirstPosition,SecondPosition,0.5f);
    //         GameObject newRLine = Instantiate(redLine, MyCenter, Quaternion.identity, FindPlane.transform);
    //         newRLine.transform.LookAt(SecondPosition);
    //         newRLine.GetComponent<SpriteRenderer>().size = new Vector2(MyMagnitude ,0.31f);
    //         newRLine.transform.Rotate(90,newRLine.transform.rotation.y + 90,0);
    //         //newWLine.transform.localScale = new Vector3(0.01f, 1f, 0.1f * MyMagnitude);
    //         GameObject.Find("BoundaryFindObj1").name = "BoundaryFindObj";
    //         GameObject.Find("BoundaryFindObj2").name = "BoundaryFindObj";
    //     }
    //     // if (GameObject.Find("Flag_white(Clone)1") && GameObject.Find("Flag_white(Clone)2"))
    //     // {
    //     //     Vector3 FirstPosition = GameObject.Find("Flag_white(Clone)1").transform.position;
    //     //     Vector3 SecondPosition = GameObject.Find("Flag_white(Clone)2").transform.position;
    //     //     Vector3 DirectionVector = SecondPosition - FirstPosition;
    //     //     float MyMagnitude = DirectionVector.magnitude;
    //     //     Debug.Log("DirectionVector: " + DirectionVector.ToString());
    //     //     Debug.Log("MyMagnitude: " + MyMagnitude.ToString());
    //     // }

    // }

    // void DeleteObject()
    // {
    //     // Если была нажата кнопка Delete в AR-меню
    //     if (checkDeleteObj)
    //     {
    //         if ((hit.collider.gameObject.name != "BasePlanePrefab") && (hit.collider.gameObject.name != "DeleteButtonCube"))
    //         {
    //             //TextLog.text = hit.collider.gameObject.name;
    //             DeleteObjPrefab.transform.position = hit.collider.gameObject.transform.position;
    //             DeleteObjPrefab.transform.LookAt(ARCamera.transform);
    //             DeleteObjPrefab.SetActive(true);
    //             if (Input.GetKeyDown(KeyCode.LeftShift))
    //             {
    //                 Destroy(hit.collider.gameObject);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         DeleteObjPrefab.SetActive(false);
    //     }
    // }

    // // создаю линию между двумя кубами
    // public void LineDrawingButton(string obj1, string obj2)
    // { 
    //     lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
    //     //lineRenderer.startColor = Color.white;
    //     lineRenderer.material.SetColor("color",Color.white);
    //     //lineRenderer.endColor = Color.white;
    //     lineRenderer.startWidth = 0.1f;
    //     lineRenderer.endWidth = 0.1f;
    //     lineRenderer.positionCount = 2;
    //     lineRenderer.useWorldSpace = true;    

    //     FindObject = GameObject.Find(obj1);
    //     lineRenderer.SetPosition(0, FindObject.transform.position);
    //     FindObject = GameObject.Find(obj2);
    //     lineRenderer.SetPosition(1, FindObject.transform.position);
    // }

    // выводит какие команды посылает VR контроллер
    // void OnGUI()
    // {
    //     if (Event.current.isKey && Event.current.type == EventType.KeyDown)
    //     {
    //         Debug.Log(Event.current.keyCode);
    //         TextLog.text = Event.current.keyCode.ToString();
    //     }
    // }
    // // для тестирования какие команды посылает VR контроллер
    // void MyVRControllerTEST()
    // {
    //     // фазы нажатия ЛКМ
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Debug.Log("МЫШКА DOWN");
    //         TextLog.text = "ЛКМ DOWN";
    //     }
    //     if (Input.GetMouseButton(0))
    //     {
    //         Debug.Log("МЫШКА HOLD");
    //         TextLog.text = "ЛКМ HOLD";
    //     }
    //     if (Input.GetMouseButtonUp(0))
    //     {
    //         Debug.Log("МЫШКА UP");
    //         TextLog.text = "ЛКМ UP";
    //     }

    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         TextLog.text = "ПКМ DOWN";
    //     }


    //     // Следующие команды работают только когда пульт в режиме VR - нажать (@ + C)
    //     if (Input.GetKeyDown(KeyCode.LeftShift))
    //     {
    //         Debug.Log("НИЖНЯЯ кнопка на VR контроллере под указательным пальцем");
    //     }
    //     if (Input.GetKeyDown(KeyCode.RightShift))
    //     {
    //         Debug.Log("ВЕРХНЯЯ кнопка на VR контроллере под указательным пальцем");
    //     }
    //     if (Input.GetKeyDown(KeyCode.JoystickButton0))
    //     {
    //         Debug.Log("Верхняя (C) кнопка пульт");
    //     }
    //     if (Input.GetKeyDown(KeyCode.JoystickButton1))
    //     {
    //         Debug.Log("Правая (A) кнопка пульт");
    //     }
    //     if (Input.GetKeyDown(KeyCode.JoystickButton2))
    //     {
    //         Debug.Log("Левая (B) кнопка пульт");
    //     }
    //     if (Input.GetKeyDown(KeyCode.JoystickButton3))
    //     {
    //         Debug.Log("Нижняя (D) кнопка пульт");
    //     }
    // }
}
