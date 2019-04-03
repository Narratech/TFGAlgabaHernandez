using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance = null;

    [Serializable]
    class Data
    {
        public string info;
        public float[] data;
        public int epoch;

    }
    public int user = 0;
    
    public void SaveData(string info, float[] datos)
    {
        Data aux = new Data();
        aux.info = info;
        aux.data = new float[datos.Length];
        aux.epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;


        //No veo otro metodo mejor para copiarlo
        for (int i = 0; i< datos.Length; i++){
            aux.data[i] = datos[i];
        }

        string json = JsonUtility.ToJson(aux);
  
        File.WriteAllText(Application.dataPath + "/" + "User" + user +"Type"+info+".json", json);

    }
    public void Test()
    {
        Debug.Log("TEST");
    }


        void Awake()
    {
        if (instance == null)
            instance = this;
    }

}

