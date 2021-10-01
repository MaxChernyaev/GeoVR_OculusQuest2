using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NameOutput : MonoBehaviour
{
    private Text TextLog;  // лог на VRcanvas
    private ARSceneMakingManager ARSceneMakingManager;
    private VRButton VRButton;
    private GameObject[] TextHR;
    // private string[] hiddenRadarograms = new string[10];
    // private List<string> hiddenRadarograms = new List<string>();

    void Start()
    {
        TextLog = GameObject.Find("VRLOGText").GetComponent<Text>();
        ARSceneMakingManager = GameObject.Find("XR Rig").GetComponent<ARSceneMakingManager>();
        VRButton = GameObject.Find("Main Camera").GetComponent<VRButton>();
        TextHR = VRButton.TextHR;
    }

    public void VRNameOutputEnter()
    {
        if(GameObject.Find("scripts").GetComponent<buttonBAN>().button_ban == false)
        {
            GameObject.Find("scripts").GetComponent<buttonBAN>().StartCoroutine_Button_BAN_03sec();
            // TextLog.text = "скрываю объект: " + name + " ";
            // TextLog.text += GameObject.Find(name).GetComponent<BoxCollider>().size;
            GameObject.Find(name).SetActive(false);
            //     ARSceneMakingManager.hiddenRadarograms.Add(name); // добавляю в список скрытых радарограмм
            // foreach (string item in hiddenRadarograms)
            // {
            //     TextLog.text += item + " ";
            // }

            int i = Int32.Parse(name.Substring(17)); // получаем порядковый номер радарограммы из её имени
            ARSceneMakingManager.hiddenRadarograms.Add(i); // добавляю номер радарограммы в список скрытых радарограмм
            ARSceneMakingManager.RGActiveArray[i] = false; // меняем в другом скрипте для синхронизации разных способов скрытия
            VRButton.RGActiveArray[i] = false; // меняем в другом скрипте для синхронизации разных способов скрытия
            
            if (TextHR[i].GetComponent<Text>().text == "Скрыть " + i.ToString())
            {
                TextHR[i].GetComponent<Text>().text = "Отобр. " + i.ToString();
            }
            else if (TextHR[i].GetComponent<Text>().text == "Отобр. " + i.ToString())
            {
                TextHR[i].GetComponent<Text>().text = "Скрыть " + i.ToString();
            }

            //GameObject.Find(name).SetActive(false);
        }
    }

    public void VRNameOutputExit()
    {

    }
}
