using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Класс для сохранения словаря IPшников в json
/// </summary>
public class IPListSaver
{
    public Dictionary<string, string> IPDictionary = new Dictionary<string, string>(10); // словарь IPшников, ключ - название (офис, квартира...), значение - IP (192.168.110.33)
    [Serializable] public struct MyDictionary // словари нативно не сериализуются, поэтому я преобразую словарь в структуру перед сериализацией
    {
        [SerializeField] public string Key;
        [SerializeField] public string Value;
    }

    [SerializeField] public List<MyDictionary> IPList = new List<MyDictionary>(); // набор структур храним в List и уже его сериализуем в JSON

    /// <summary>
    /// Добавить ключ-значение в словарь IPшников
    /// </summary>
    public void AddIPList(string MyKey, string MyValue)
    {
        // если раньше уже был файл с IP
        if(File.Exists( Path.Combine(Application.persistentDataPath, "Ryven_IP_list.json") ))
        {
            IPDictionary.Clear(); // предварительно очищаем, чтобы не было конфликтов при нескольких сохранениях подряд
            IPList.Clear();
            // считаем существующий json, пройдёмся по его значениям и скопируем их в новый словарь
            try
            {
                foreach(KeyValuePair<string, string> keyValue in IPListLoadFromJson().IPDictionary)
                {
                    IPDictionary.Add(keyValue.Key, keyValue.Value);
                }
            }
            catch(Exception e)
            {
                Debug.Log("Exception: " + e);
            }
            
        }

        // добавим в новый словарь новые поля после старых
        IPDictionary.Add(MyKey, MyValue);
        // IPDictionary.Add("Офис 608","192.168.110.33");
        // IPDictionary.Add("Дача Торопчина","192.168.1.1");
        // IPDictionary.Add("Коттедж Вадима","192.168.137.1");

        // проходимся по словарю и переносим всё в List структур для сериализации
        foreach(KeyValuePair<string, string> keyValue in IPDictionary)
        {
            // Debug.Log("существует: " + keyValue.Key + " - " + keyValue.Value);
            IPList.Add(new MyDictionary{ Key = keyValue.Key, Value = keyValue.Value });
        }
    }

    /// <summary>
    /// Сохранить словарь IPшников в файл
    /// </summary>
    public void IPListSaveToJson()
    {        
        using (FileStream fileStream = File.Open(Path.Combine(Application.persistentDataPath, "Ryven_IP_list.json"), FileMode.OpenOrCreate, FileAccess.Write))
        {
            fileStream.SetLength(0);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(JsonUtility.ToJson(this, true)); // тут можно поставить true вторым аргуметом, чтобы был красивый вывод JSON с отступами
            }
        }

        Debug.Log("Json записан на диск! Successfully!");
    }

    /// <summary>
    /// Прочитать словарь IPшников из файла
    /// </summary>
    public IPListSaver IPListLoadFromJson()
    {
        if(File.Exists( Path.Combine(Application.persistentDataPath, "Ryven_IP_list.json") ))
        {
            using (FileStream fileStream = File.Open(Path.Combine(Application.persistentDataPath, "Ryven_IP_list.json"), FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                IPListSaver IPListSaverObj = JsonUtility.FromJson<IPListSaver>(reader.ReadToEnd());

                if (IPListSaverObj == null)
                {
                    //Debug.Log("файл пустой1");
                    return null;
                }
                else
                {
                    // преобразуем набор структур из List назад в словарь
                    for (int i = 0; i < IPListSaverObj.IPList.Count; i++)
                    {
                        IPListSaverObj.IPDictionary.Add(IPListSaverObj.IPList[i].Key, IPListSaverObj.IPList[i].Value);
                        // Debug.Log("добавляю в словарь: " + IPListSaverObj.IPList[i].Key + " - " + IPListSaverObj.IPList[i].Value);
                    }

                    // посмотреть что в словаре
                    // foreach(KeyValuePair<string, string> keyValue in IPListSaverObj.IPDictionary)
                    // {
                    //     Debug.Log("существует: " + keyValue.Key + " - " + keyValue.Value);
                    // }

                    return IPListSaverObj;
                }
            }
        }
        return null;
    }
}