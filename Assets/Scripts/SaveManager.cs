using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    public bool hasLoaded;

    // ����������
    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        // ����� ������� ����� ���� ������

        var fSerializer = new XmlSerializer(typeof(SaveData));
        var fStream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        fSerializer.Serialize(fStream, activeSave);
        fStream.Close();

        Debug.Log("Saved");

    }
    // ��������
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        // ����� ������� ����� ���� ������

        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            var fSerializer = new XmlSerializer(typeof(SaveData));
            var fStream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = fSerializer.Deserialize(fStream) as SaveData;
            fStream.Close();

            Debug.Log("Load");
            hasLoaded = true;
        }
    }

    // �������� ����������
    public void SaveDelete()
    {
        string dataPath = Application.persistentDataPath;
        // ����� ������� ����� ���� ������
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
        }

        Debug.Log("Delete Save");
    }

    private void Awake()
    {
        instance = this;
        Load();
    }
}

[System.Serializable]
public class SaveData
{
    public string saveName;
    // ... 
    // ��� ������ ������ ��� ����������
}