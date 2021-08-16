using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для управления сохранением/загрузкой всех объектов на сцене
/// </summary>
public class SaverManager : MonoBehaviour
{
    public void SaveAll()
    {
        SaverObject[] obj = FindObjectsOfType<SaverObject>();
        foreach (var item in obj)
        {
            item.Save();
        }
    }

    public void LoadAll()
    {
        // удалить старые объекты (если есть)
        // создать нужное количество объектов нужных типов

        SaverObject[] obj = FindObjectsOfType<SaverObject>();
        foreach (var item in obj)
        {
            item.Load();
        }
    }
}
