using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Класс для сохранения и загрузки позиций объекта.
/// </summary>
public class SaverObject : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;

    /// <summary>
    /// Относительный путь к файлу загрузки данных.
    /// </summary>
    [SerializeField]
    private string _JsonPath;

    /// <summary>
    /// Полный путь к файлу загрузки данных.
    /// </summary>
    private string _saveDataPath;

    /// <summary>
    /// данные в формате json (поля текущего объекта)
    /// </summary>
    public string data;

    private void Start()
    {
        _saveDataPath = Path.Combine(Application.persistentDataPath, _JsonPath);
    }

    public void CollectInfo()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    public void SetInfo()
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Save()
    {
        CollectInfo();
        data = JsonUtility.ToJson(this);
        File.WriteAllText(_saveDataPath,data); // заменить на запись в один файл потоком всех объектов
    }

    public void Load()
    {
        data = File.ReadAllText(_saveDataPath); // заменить на парсинг нужного куска данных из файла
        JsonUtility.FromJsonOverwrite(data,this); // закидываем данные из json в объект
        SetInfo();
    }
}