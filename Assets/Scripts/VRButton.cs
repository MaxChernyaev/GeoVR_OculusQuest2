using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class VRButton : MonoBehaviour
{
    GameObject RG1;
    GameObject RG2;
    GameObject RG3;
    GameObject RG4;
    GameObject RG5;
    GameObject RG6;
    GameObject RG7;
    GameObject RG8;
    GameObject RG9;
    GameObject RG10;

    private bool RG1Active = true;
    private bool RG2Active = true;
    private bool RG3Active = true;
    private bool RG4Active = true;
    private bool RG5Active = true;
    private bool RG6Active = true;
    private bool RG7Active = true;
    private bool RG8Active = true;
    private bool RG9Active = true;
    private bool RG10Active = true;
    private bool test = false;
    JsonRadarogramReader.CommonData myJsonRadarogramData;
    [SerializeField] private GameObject textPanel;

    [SerializeField] private GameObject SetActiveButton;

    public bool buttonActive = false;
    void Start()
    {
        //FindRadarogram();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideRadarogram1() // скрытие радарограмм
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG1Active = !RG1Active;
        RG1.SetActive(RG1Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR1").GetComponent<Text>().text == "Скрыть 1")
        {
            transform.Find("TextHR1").GetComponent<Text>().text = "Отобр. 1";
        }
        else if (transform.Find("TextHR1").GetComponent<Text>().text == "Отобр. 1")
        {
            transform.Find("TextHR1").GetComponent<Text>().text = "Скрыть 1";
        }
    }
    public void HideRadarogram2()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG2Active = !RG2Active;
        RG2.SetActive(RG2Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR2").GetComponent<Text>().text == "Скрыть 2")
        {
            transform.Find("TextHR2").GetComponent<Text>().text = "Отобр. 2";
        }
        else if (transform.Find("TextHR2").GetComponent<Text>().text == "Отобр. 2")
        {
            transform.Find("TextHR2").GetComponent<Text>().text = "Скрыть 2";
        }
    }
    public void HideRadarogram3()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG3Active = !RG3Active;
        RG3.SetActive(RG3Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR3").GetComponent<Text>().text == "Скрыть 3")
        {
            transform.Find("TextHR3").GetComponent<Text>().text = "Отобр. 3";
        }
        else if (transform.Find("TextHR3").GetComponent<Text>().text == "Отобр. 3")
        {
            transform.Find("TextHR3").GetComponent<Text>().text = "Скрыть 3";
        }
    }
    public void HideRadarogram4()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG4Active = !RG4Active;
        RG4.SetActive(RG4Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR4").GetComponent<Text>().text == "Скрыть 4")
        {
            transform.Find("TextHR4").GetComponent<Text>().text = "Отобр. 4";
        }
        else if (transform.Find("TextHR4").GetComponent<Text>().text == "Отобр. 4")
        {
            transform.Find("TextHR4").GetComponent<Text>().text = "Скрыть 4";
        }
    }
    public void HideRadarogram5()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG5Active = !RG5Active;
        RG5.SetActive(RG5Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR5").GetComponent<Text>().text == "Скрыть 5")
        {
            transform.Find("TextHR5").GetComponent<Text>().text = "Отобр. 5";
        }
        else if (transform.Find("TextHR5").GetComponent<Text>().text == "Отобр. 5")
        {
            transform.Find("TextHR5").GetComponent<Text>().text = "Скрыть 5";
        }
    }
    public void HideRadarogram6()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG6Active = !RG6Active;
        RG6.SetActive(RG6Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR6").GetComponent<Text>().text == "Скрыть 6")
        {
            transform.Find("TextHR6").GetComponent<Text>().text = "Отобр. 6";
        }
        else if (transform.Find("TextHR6").GetComponent<Text>().text == "Отобр. 6")
        {
            transform.Find("TextHR6").GetComponent<Text>().text = "Скрыть 6";
        }
    }
    public void HideRadarogram7()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG7Active = !RG7Active;
        RG7.SetActive(RG7Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR7").GetComponent<Text>().text == "Скрыть 7")
        {
            transform.Find("TextHR7").GetComponent<Text>().text = "Отобр. 7";
        }
        else if (transform.Find("TextHR7").GetComponent<Text>().text == "Отобр. 7")
        {
            transform.Find("TextHR7").GetComponent<Text>().text = "Скрыть 7";
        }
    }
    public void HideRadarogram8()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG8Active = !RG8Active;
        RG8.SetActive(RG8Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR8").GetComponent<Text>().text == "Скрыть 8")
        {
            transform.Find("TextHR8").GetComponent<Text>().text = "Отобр. 8";
        }
        else if (transform.Find("TextHR8").GetComponent<Text>().text == "Отобр. 8")
        {
            transform.Find("TextHR8").GetComponent<Text>().text = "Скрыть 8";
        }
    }
    public void HideRadarogram9()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG9Active = !RG9Active;
        RG9.SetActive(RG9Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR9").GetComponent<Text>().text == "Скрыть 9")
        {
            transform.Find("TextHR9").GetComponent<Text>().text = "Отобр. 9";
        }
        else if (transform.Find("TextHR9").GetComponent<Text>().text == "Отобр. 9")
        {
            transform.Find("TextHR9").GetComponent<Text>().text = "Скрыть 9";
        }
    }
    public void HideRadarogram10()
    {
        if(test == false)
        {
            //Debug.Log("test == false");
            FindRadarogram();
        }
        RG10Active = !RG10Active;
        RG10.SetActive(RG10Active);
        //GameObject.Find("radarogramPrefab_" + transform.parent.Find("Number").GetComponent<TextMesh>().text).SetActive(false);
        if (transform.Find("TextHR10").GetComponent<Text>().text == "Скрыть 10")
        {
            transform.Find("TextHR10").GetComponent<Text>().text = "Отобр. 10";
        }
        else if (transform.Find("TextHR10").GetComponent<Text>().text == "Отобр. 10")
        {
            transform.Find("TextHR10").GetComponent<Text>().text = "Скрыть 10";
        }
    }

    public void ChangeIP()
    {
        SetActiveButton.SetActive(!buttonActive);
        buttonActive = !buttonActive;
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

    private void FindRadarogram()
    {
        test = true;
        RG1 =  GameObject.Find("radarogramPrefab_1");
        RG2 =  GameObject.Find("radarogramPrefab_2");
        RG3 =  GameObject.Find("radarogramPrefab_3");
        RG4 =  GameObject.Find("radarogramPrefab_4");
        RG5 =  GameObject.Find("radarogramPrefab_5");
        RG6 =  GameObject.Find("radarogramPrefab_6");
        RG7 =  GameObject.Find("radarogramPrefab_7");
        RG8 =  GameObject.Find("radarogramPrefab_8");
        RG9 =  GameObject.Find("radarogramPrefab_9");
        RG10 =  GameObject.Find("radarogramPrefab_10");

    }
}
