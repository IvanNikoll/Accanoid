using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;
using System.IO;

public class Data : MonoBehaviour
{
    public static Data Instance;
    public int[] test; // holds data during the game session

    private void Awake() // check and load data when after a new scene loaded
    {
        LoadData();
        if(test == null)
        {
            test[0] = 0;
            test[1] = 0;
            test[2] = 0;
        }
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveData // saves Top scores between game sessions
    {
        public int ScoreSaved1;
        public int ScoreSaved2;
        public int ScoreSaved3;

    }
    public void Save() // saves top scores into a .Json file
    {
        SaveData saveData = new Data.SaveData();
        saveData.ScoreSaved1 = test[0];
        saveData.ScoreSaved2 = test[1];
        saveData.ScoreSaved3 = test[2];
        Debug.Log("Saved:" + saveData.ScoreSaved1 + ";" + saveData.ScoreSaved2 + ";" + saveData.ScoreSaved3);
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadData() // loads top scores from .Json when the game started
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            test[0] = saveData.ScoreSaved1;
            test[1] = saveData.ScoreSaved2;
            test[2] = saveData.ScoreSaved3;
            Debug.Log("Loaded: " + test[0]+ ";" + test[1] + ";" + test[2]) ;
           
        }
    }
    

}




