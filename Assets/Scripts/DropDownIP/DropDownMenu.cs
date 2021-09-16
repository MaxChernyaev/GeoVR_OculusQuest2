using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// Класс для работы со словарём IPшников, хранящихся в JSON, через выпадающий список
/// </summary>
public class DropDownMenu : MonoBehaviour
{
    [SerializeField] public Text selectItem;
    [SerializeField] private Dropdown dropdown;
    private IPListSaver ip_saver = new IPListSaver();
    List<string> items = new List<string>();

    void Start()
    {
        dropdown.options.Clear();

        if(File.Exists( Path.Combine(Application.persistentDataPath, "Ryven_IP_list.json") ))
        {
            ip_saver = ip_saver.IPListLoadFromJson();

            if (ip_saver == null)
            {
                //Debug.Log("файл пустой2");
                ip_saver = new IPListSaver();
            }
            else
            {
                // наполняю список данными из словаря
                foreach(KeyValuePair<string, string> keyValue in ip_saver.IPDictionary)
                {
                    //Debug.Log("существует: " + keyValue.Key + " - " + keyValue.Value);
                    items.Add(keyValue.Key);
                }

                // наполняю выпадающий список данными из списка items
                items.Reverse(); // предварительно переворачиваю его, чтобы новые элементы были вверху
                foreach(var item in items)
                {
                    dropdown.options.Add(new Dropdown.OptionData() {text = item });
                }
                
                DropdownItemSelected(dropdown);
            }
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    public void UpdateDropDown()
    {
        dropdown.options.Clear();
        items.Clear();

        ip_saver = ip_saver.IPListLoadFromJson();

        // наполняю список данными из словаря
        foreach(KeyValuePair<string, string> keyValue in ip_saver.IPDictionary)
        {
            //Debug.Log("в словаре: " + keyValue.Key + " - " + keyValue.Value);
            items.Add(keyValue.Key);
        }

        // наполняю выпадающий список данными из списка items
        items.Reverse(); // предварительно переворачиваю его, чтобы новые элементы были вверху
        foreach(var item in items)
        {
            //Debug.Log("в списке: " + item);
            dropdown.options.Add(new Dropdown.OptionData() {text = item });
        }

        dropdown.RefreshShownValue();

        DropdownItemSelected(dropdown);
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        // получение элемента по ключу
        selectItem.text = ip_saver.IPDictionary[dropdown.options[index].text];
    }
}
