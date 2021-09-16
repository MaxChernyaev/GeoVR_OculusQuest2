using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс для кнопки "сохранить новый IP"
/// </summary>
public class AddNewIP : MonoBehaviour
{
    private IPListSaver ip_saver = new IPListSaver();
    [SerializeField] public InputField inputNameIP;
    [SerializeField] public InputField inputIPAddress;
    [SerializeField] private Text ErrorNotification;

    public void AddNewIPFromJson()
    {
        if (inputIPAddress.text != "" && inputNameIP.text == "") // не ввели название сети, но ввели сам IP
        {
            ErrorNotification.text = "";
            inputNameIP.text = inputIPAddress.text;
            AddIP();
        }
        if (inputIPAddress.text == "" && inputNameIP.text != "") // не ввели IP, но ввели название
        {
            ErrorNotification.text = "Вы не ввели IP адрес =(";
        }
        if (inputIPAddress.text == "" && inputNameIP.text == "") // не ввели ничего
        {
            ErrorNotification.text = "Вы ничего не ввели =(";
        }
        if (inputIPAddress.text != "" && inputNameIP.text != "") // ввели оба поля
        {
            ErrorNotification.text = "";
            AddIP();
        }
    }

    private void AddIP()
    {
        ip_saver.AddIPList(inputNameIP.text, inputIPAddress.text);
        ip_saver.IPListSaveToJson();
        GameObject.Find("Dropdown").GetComponent<DropDownMenu>().UpdateDropDown();
    }

}
