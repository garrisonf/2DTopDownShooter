using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Save
{
   //all variables in class are info to save
   //save playerPosition as Vector2 or Vector3 (tranform.position)
   //int to tell how many puzzles completed
   public Vector3 playerPosition;
   public int numLevelsCompleted;
}

public class SaveDataManager
{
   static readonly string filename = "horizonSaveData";
   
   //save and load functions
   public static void save(Vector3 vec, int numLevels)
   {
      Save savObject = new Save();
      savObject.playerPosition = vec;
      savObject.numLevelsCompleted = numLevels;
      string jsonStr = JsonUtility.ToJson(savObject);
      
      string path = Path.Combine(Application.persistentDataPath, filename);
      
      using (StreamWriter streamWriter = File.CreateText(path))
      {
         streamWriter.Write(jsonStr);
      }
   }
   
   public static bool saveExists()
   {
      string path = Path.Combine(Application.persistentDataPath, filename);
      FileInfo save_file = new FileInfo(path);
      return save_file.Exists;
   }
   
   public static Save load()
   {
      string path = Path.Combine(Application.persistentDataPath, filename);
      
      using (StreamReader streamReader = File.OpenText(path))
      {
         string jsonStr = streamReader.ReadToEnd();
         Save savObj = JsonUtility.FromJson<Save>(jsonStr);
         return savObj;
      }
   }
}
