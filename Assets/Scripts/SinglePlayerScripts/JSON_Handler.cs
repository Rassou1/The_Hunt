using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
//This script handles saving and loading JSON files. - Love
public static class JSON_Handler
{
    //A string to the folder the JSONs get saved in, purely for the convenience of refering to the variable instead of the file path when writing the code. - Love
    private static string _filePath = "/SP_JSONs";

    //The classes which are used to save data to the JSONs for the timer - Love
    private class SaveTime
    {
        public float _bestTime;
    }


    //This method is used to check if you should overwrite your old best time with your new one. It then returns your current best time - Love
    public static float? CheckOverwrite(float newTime, string fileName)
    {
        float? _loadedTime = Load(fileName);
        if(_loadedTime == null)
        {
            Save(newTime, fileName);
            return newTime;
        }
        else
        {
            if (newTime < _loadedTime)
            {
                Save(newTime, fileName);
                return newTime;
            }
            else return _loadedTime;
        }
    }

    //Method to save to a JSON of "_fileName" - Love
    private static void Save(float newBestTime, string fileName)
    {
        SaveTime saveObject = new SaveTime()
        {
            _bestTime = newBestTime
        };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + _filePath + fileName, json);
    }

    //Method to return a nullable float. Made the float nullabe to allow you to attempt to load and be able to run the CheckOverwrite method without there being any previous data - Love
    public static float? Load(string fileName)
    {
        if (File.Exists(Application.dataPath + _filePath + fileName))
        {
            string saveString = File.ReadAllText(Application.dataPath + _filePath + fileName);

            SaveTime saveObject = JsonUtility.FromJson<SaveTime>(saveString);
            return saveObject._bestTime;
        }
        else return null;
    }
}
