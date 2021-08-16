using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Text MainText;
    public bool SplitCameraON;
    public void StartLevelButton()
    {
        DataHolder.SplitCameraON = SplitCameraON;
        SceneManager.LoadScene("Simple");

        if (SplitCameraON)
        {
            MainText.text = "Вставьте устройство в VR очки                                                           ЗАГРУЗКА...";
        }
        else
        {
            MainText.text = " ЗАГРУЗКА...";
        }
    }

    public void CheckBoxActive()
    {
        if (gameObject.GetComponent<Toggle>().isOn == true)
        {
            DataHolder.CheckBox = true;
        }
        else
        {
            DataHolder.CheckBox = false;
        }
    }

    void Update()
    {
        DataHolder.TextIP = GameObject.Find("InputFieldIP").transform.Find("Text").GetComponent<Text>().text;
        Debug.Log(DataHolder.TextIP);
    }
}