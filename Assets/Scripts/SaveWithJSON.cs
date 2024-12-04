using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveWithJSON : MonoBehaviour
{
    [SerializeField] SaveData data;
    [SerializeField] SaveData defaultData;

    string path;

    private void Start()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).Replace("\\", "/") 
                            + "/" + Application.productName;

        if(!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        //path = Application.persistentDataPath + "/SaveData.json";
        path = folderPath + "/SaveData.json";


        Debug.Log(folderPath);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void Save()
    {
        data.position = transform.position;
        string json = JsonUtility.ToJson(data, true);
        json = EncryptDecrypt(json);
        File.WriteAllText(path, json);

    }

    void Load()
    {
        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        json = EncryptDecrypt(json);

        data = JsonUtility.FromJson<SaveData>(json);

        transform.position = data.position;
        //JsonUtility.FromJsonOverwrite(json, data); //Para MonoBehaviour
    }

    string keyWord = "UnaContraseñaBastanteLargaParaQueSeaMasSegura289379";

    private string EncryptDecrypt(string Data)
    {
        string result = "";

        for (int i = 0; i < Data.Length; i++)
            result += (char)(Data[i] ^ keyWord[i % keyWord.Length]);

        return (result);
    }

    void DeleteData()
    {
        //if(!File.Exists(path)) return;

        //File.Delete(path);

        data = defaultData;
        Save();
    }
}
