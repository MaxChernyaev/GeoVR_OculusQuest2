using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class VRButton : MonoBehaviour
{
    // GameObject RG1, RG2, RG3, RG4, RG5, RG6, RG7, RG8, RG9, RG10;

    // массив самих объектов - радарограмм
    public GameObject[] RGArray = new GameObject[11]; // используется с 1 до 10
    
    // массив флагов активности радарограмм
    public bool[] RGActiveArray = new bool[11]; // используется с 1 до 10

    // флаг, указывающий на то, что радарограммы были найдены
    public bool RadarogramWereFound = false;

    // объект ARSceneMakingManager, управляющий основной логикой. Будем в нём менять переменную активности радарограмм для синхронизации разных способов скрытия
    private ARSceneMakingManager ARSceneMakingManager;

    // private bool RG1Active = true;
    // private bool RG2Active = true;
    // private bool RG3Active = true;
    // private bool RG4Active = true;
    // private bool RG5Active = true;
    // private bool RG6Active = true;
    // private bool RG7Active = true;
    // private bool RG8Active = true;
    // private bool RG9Active = true;
    // private bool RG10Active = true;
    // private bool test = false;
    JsonRadarogramReader.CommonData myJsonRadarogramData;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private Text TextLog;  // лог на VRcanvas
    [SerializeField] public GameObject IPCanvas;
    [SerializeField] public GameObject MainCanvas;
    [SerializeField] public GameObject[] TextHR;

    public bool buttonActive = false;
    void Start()
    {
        ARSceneMakingManager = GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>();
        //FindRadarogram();
        MainCanvas.SetActive(false);
        IPCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(RadarogramWereFound == false) FindRadarogram();
    }

    public void HideRadarogram1() // скрытие радарограмм
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        // TextLog.text = "RGActiveArray[1] :" + RGActiveArray[1].ToString();
        RGActiveArray[1] = !RGActiveArray[1];
        ARSceneMakingManager.RGActiveArray[1] = RGActiveArray[1]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[1].SetActive(RGActiveArray[1]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[1].GetComponent<Text>().text == "Скрыть 1")
        {
            TextHR[1].GetComponent<Text>().text = "Отобр. 1";
        }
        else if (TextHR[1].GetComponent<Text>().text == "Отобр. 1")
        {
            TextHR[1].GetComponent<Text>().text = "Скрыть 1";
        }
    }
    public void HideRadarogram2()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[2] = !RGActiveArray[2];
        ARSceneMakingManager.RGActiveArray[2] = RGActiveArray[2]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[2].SetActive(RGActiveArray[2]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[2].GetComponent<Text>().text == "Скрыть 2")
        {
            TextHR[2].GetComponent<Text>().text = "Отобр. 2";
        }
        else if (TextHR[2].GetComponent<Text>().text == "Отобр. 2")
        {
            TextHR[2].GetComponent<Text>().text = "Скрыть 2";
        }
    }
    public void HideRadarogram3()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[3] = !RGActiveArray[3];
        ARSceneMakingManager.RGActiveArray[3] = RGActiveArray[3]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[3].SetActive(RGActiveArray[3]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[3].GetComponent<Text>().text == "Скрыть 3")
        {
            TextHR[3].GetComponent<Text>().text = "Отобр. 3";
        }
        else if (TextHR[3].GetComponent<Text>().text == "Отобр. 3")
        {
            TextHR[3].GetComponent<Text>().text = "Скрыть 3";
        }
    }
    public void HideRadarogram4()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[4] = !RGActiveArray[4];
        ARSceneMakingManager.RGActiveArray[4] = RGActiveArray[4]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[4].SetActive(RGActiveArray[4]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[4].GetComponent<Text>().text == "Скрыть 4")
        {
            TextHR[4].GetComponent<Text>().text = "Отобр. 4";
        }
        else if (TextHR[4].GetComponent<Text>().text == "Отобр. 4")
        {
            TextHR[4].GetComponent<Text>().text = "Скрыть 4";
        }
    }
    public void HideRadarogram5()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[5] = !RGActiveArray[5];
        ARSceneMakingManager.RGActiveArray[5] = RGActiveArray[5]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[5].SetActive(RGActiveArray[5]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[5].GetComponent<Text>().text == "Скрыть 5")
        {
            TextHR[5].GetComponent<Text>().text = "Отобр. 5";
        }
        else if (TextHR[5].GetComponent<Text>().text == "Отобр. 5")
        {
            TextHR[5].GetComponent<Text>().text = "Скрыть 5";
        }
    }
    public void HideRadarogram6()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[6] = !RGActiveArray[6];
        ARSceneMakingManager.RGActiveArray[6] = RGActiveArray[6]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[6].SetActive(RGActiveArray[6]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[6].GetComponent<Text>().text == "Скрыть 6")
        {
            TextHR[6].GetComponent<Text>().text = "Отобр. 6";
        }
        else if (TextHR[6].GetComponent<Text>().text == "Отобр. 6")
        {
            TextHR[6].GetComponent<Text>().text = "Скрыть 6";
        }
    }
    public void HideRadarogram7()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[7] = !RGActiveArray[7];
        ARSceneMakingManager.RGActiveArray[7] = RGActiveArray[7]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[7].SetActive(RGActiveArray[7]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[7].GetComponent<Text>().text == "Скрыть 7")
        {
            TextHR[7].GetComponent<Text>().text = "Отобр. 7";
        }
        else if (TextHR[7].GetComponent<Text>().text == "Отобр. 7")
        {
            TextHR[7].GetComponent<Text>().text = "Скрыть 7";
        }
    }
    public void HideRadarogram8()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[8] = !RGActiveArray[8];
        ARSceneMakingManager.RGActiveArray[8] = RGActiveArray[8]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[8].SetActive(RGActiveArray[8]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[8].GetComponent<Text>().text == "Скрыть 8")
        {
            TextHR[8].GetComponent<Text>().text = "Отобр. 8";
        }
        else if (TextHR[8].GetComponent<Text>().text == "Отобр. 8")
        {
            TextHR[8].GetComponent<Text>().text = "Скрыть 8";
        }
    }
    public void HideRadarogram9()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[9] = !RGActiveArray[9];
        ARSceneMakingManager.RGActiveArray[9] = RGActiveArray[9]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[9].SetActive(RGActiveArray[9]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[9].GetComponent<Text>().text == "Скрыть 9")
        {
            TextHR[9].GetComponent<Text>().text = "Отобр. 9";
        }
        else if (TextHR[9].GetComponent<Text>().text == "Отобр. 9")
        {
            TextHR[9].GetComponent<Text>().text = "Скрыть 9";
        }
    }
    public void HideRadarogram10()
    {
        if(RadarogramWereFound == false)
        {
            //Debug.Log("RadarogramWereFound == false");
            FindRadarogram();
        }
        RGActiveArray[10] = !RGActiveArray[10];
        ARSceneMakingManager.RGActiveArray[10] = RGActiveArray[10]; // также меняем в другом скрипте для синхронизации разных способов скрытия
        RGArray[10].SetActive(RGActiveArray[10]);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (TextHR[10].GetComponent<Text>().text == "Скрыть 10")
        {
            TextHR[10].GetComponent<Text>().text = "Отобр. 10";
        }
        else if (TextHR[10].GetComponent<Text>().text == "Отобр. 10")
        {
            TextHR[10].GetComponent<Text>().text = "Скрыть 10";
        }
    }

    public void ChangeIP()
    {
        // если доп. канвас с выбором IP открылся, то основной закрылся. И наоборот
        buttonActive = !buttonActive;
        IPCanvas.SetActive(buttonActive);
        MainCanvas.SetActive(!buttonActive);
        RadarogramWereFound = false;
        //GameObject.Find("Dropdown").GetComponent<DropDownMenu>().UpdateDropDown(); // обновляю выпадающий список
    }

    public void SaveScene()
    {
        myJsonRadarogramData = GameObject.Find("scripts").GetComponent<ObjectManager>().LoadDataJson(); // Читаю data.json;
        string CurrentTime = System.DateTime.Now.ToString();
        GameObject.Find("TextMessage").GetComponent<Text>().text = "Радарограммы сохранены в папку исследования:   " + myJsonRadarogramData.id;
        GameObject.Find("TextMessage2").GetComponent<Text>().text = "Подпапка:   " + CurrentTime;
        Directory.CreateDirectory(Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime);
        
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/0.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/0.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/1.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/1.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/2.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/2.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/3.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/3.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/4.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/4.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/5.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/5.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/6.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/6.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/7.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/7.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/8.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/8.png");
        File.Copy(Application.persistentDataPath + "/RadarogramTexture" + "/9.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/9.png");

        File.Copy(Application.persistentDataPath + "/data.json", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/data.json");
        File.Copy(Application.persistentDataPath + "/scene.json", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + CurrentTime + "/scene.json");
    }

    public void SaveScreenshot()
    {

        GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>().CanvasMenuActive = !GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>().CanvasMenuActive;
        GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>().CanvasMenu.SetActive(GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>().CanvasMenuActive);
        GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>().StartCoroutine("Button_BAN_05sec");

        StartCoroutine(TimerScreenshotCoroutine());
    }

    IEnumerator TimerScreenshotCoroutine()
    {
        textPanel.GetComponent<Text>().text = "5";
        yield return new WaitForSeconds(1f);
        textPanel.GetComponent<Text>().text = "4";
        yield return new WaitForSeconds(1f);
        textPanel.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1f);
        textPanel.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1f);
        textPanel.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1f);
        textPanel.GetComponent<Text>().fontSize = 50;
        textPanel.GetComponent<Text>().text = "вспышка";
        yield return new WaitForSeconds(1f);
        textPanel.GetComponent<Text>().fontSize = 300;
        textPanel.GetComponent<Text>().text = "";
        //ScreenCapture.CaptureScreenshot(Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + myJsonRadarogramData.id + "/" + System.DateTime.Now.ToString());
        ScreenCapture.CaptureScreenshot("NewScreenshot.png");
        yield return new WaitForSeconds(1f);
        Directory.CreateDirectory(Application.persistentDataPath  + "/" + "Saved Radarograms");
        File.Copy(Application.persistentDataPath + "/NewScreenshot.png", Application.persistentDataPath  + "/" + "Saved Radarograms" + "/" + System.DateTime.Now.ToString() + ".png");
        yield return new WaitForSeconds(1f);
        File.Delete(Application.persistentDataPath + "/NewScreenshot.png");
        //ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/скриншот");
    }

    // private void FindRadarogram()
    // {
    //     test = true;
    //     RG1 =  GameObject.Find("radarogramPrefab_1");
    //     RG2 =  GameObject.Find("radarogramPrefab_2");
    //     RG3 =  GameObject.Find("radarogramPrefab_3");
    //     RG4 =  GameObject.Find("radarogramPrefab_4");
    //     RG5 =  GameObject.Find("radarogramPrefab_5");
    //     RG6 =  GameObject.Find("radarogramPrefab_6");
    //     RG7 =  GameObject.Find("radarogramPrefab_7");
    //     RG8 =  GameObject.Find("radarogramPrefab_8");
    //     RG9 =  GameObject.Find("radarogramPrefab_9");
    //     RG10 =  GameObject.Find("radarogramPrefab_10");

    // }

    public void FindRadarogram()
    {
        for(int i = 1; i < 11; i++)
        {
            // TextLog.text = "попытка найти " + i.ToString();
            RGArray[i] = GameObject.Find("radarogramPrefab_" + i.ToString());
            if(RGArray[i] != null)
            {
                RGActiveArray[i] = true;
                //TextLog.text = "нашёл радарограмму " + i.ToString();
                RadarogramWereFound = true;
            }
        }
    }
}
