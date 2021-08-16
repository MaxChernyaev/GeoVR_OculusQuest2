using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Класс для сохранения и загрузки.
/// </summary>
public class Saver : MonoBehaviour
{
   // public Text TextLog;  // лог на canvas

    /// <summary>
    /// Относительный путь к файлу загрузки данных.
    /// </summary>
    [SerializeField]
    private string _cubeDataJsonPath;
    
    /// <summary>
    /// Полный путь к файлу загрузки данных.
    /// </summary>
    private string _saveDataPath;

    /// <summary>
    /// Полный путь к файлу загрузки данных.
    /// </summary>
    public string SaveDataPath {
        get
        {
            if (_saveDataPath == null)
            {
                _saveDataPath = Path.Combine(Application.persistentDataPath, _cubeDataJsonPath);
            }

            return _saveDataPath;
        }
    }

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

    /// <summary>
    /// Загрузить данные объектов
    /// </summary>
    public CommonSaveData Load()
    {
        if(File.Exists(SaveDataPath))
        {
            //TextLog.text = "Файл найден, загружаю сцену...";
            using (FileStream fileStream = File.Open(SaveDataPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                CommonSaveData loadData = JsonUtility.FromJson<CommonSaveData>(reader.ReadToEnd());

                return loadData;
            }
        }
        return null;

    }




    public JsonRadarogramReader.CommonData LoadJsonRadarogram()
    {
        if(File.Exists(newSaveDataPath))
        {
            using (FileStream fileStream = File.Open(newSaveDataPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                JsonRadarogramReader.CommonData loadRadData = JsonUtility.FromJson<JsonRadarogramReader.CommonData>(reader.ReadToEnd());

                return loadRadData;
            }
        }
        return null;

    }

    public void TestSaveJson(JsonRadarogramReader.CommonData MySaveData)
    {
        using (FileStream fileStream = File.Open(newSaveDataPath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            fileStream.SetLength(0);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(JsonUtility.ToJson(MySaveData, true)); // тут можно поставить true вторым аргуметом, чтобы был красивый вывод JSON с отступами
            }
        }
    }
    



    /// <summary>
    /// Сохранить данные кубов.
    /// </summary>
    public void Save(CommonSaveData saveData)
    {
        if(File.Exists(SaveDataPath))
        {
            string testtime = System.DateTime.Now.ToString();
            string testname = Path.Combine(Application.persistentDataPath, _cubeDataJsonPath + testtime);
            File.Move(SaveDataPath, testname);
        }
        //TextLog.text = "Сохраняю данные в файл...";
        using (FileStream fileStream = File.Open(SaveDataPath, FileMode.OpenOrCreate, FileAccess.Write))
        {
            fileStream.SetLength(0);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(JsonUtility.ToJson(saveData)); // тут можно поставить true вторым аргуметом, чтобы был красивый вывод JSON с отступами
            }
        }
    }

    /// <summary>
    /// Оправить данные сцены по сокету.
    /// </summary>
    public void SendSaveJSON()
    {
        if(File.Exists(SaveDataPath))
        {
            //TextLog.text = "Файл найден. Отправляю по сокету...";
            using (FileStream fileStream = File.Open(SaveDataPath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                CommonSaveData loadData = JsonUtility.FromJson<CommonSaveData>(reader.ReadToEnd());
                
                UdpClient client = new UdpClient(5600);
                try
                {
                    //TextLog.text = DataHolder.TextIP;
                    // client.Connect("192.168.43.217", 8080);
                    client.Connect(DataHolder.TextIP, 8080);
                    byte[] sendBytes = Encoding.ASCII.GetBytes(JsonUtility.ToJson(loadData));
                    client.Send(sendBytes, sendBytes.Length);
                    //TextLog.text = "Отправил данные на IP: " + DataHolder.TextIP;
                    Debug.Log("Отправил данные на IP: " + DataHolder.TextIP);
                }
                catch(Exception e)
                {
                    print("Exception thrown " + e.Message);
                    //TextLog.text = "Exception thrown " + e.Message;
                }

            }
        }

    }
}