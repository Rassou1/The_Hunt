using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
//This script handles saving the times from the trial modes to a JSON file so your best time is saved between restarting the game - Love
public class JSON_TimerHandler : MonoBehaviour
{
    //String to allow the script to be used for multiple different files - Love
    [SerializeField] private string _fileName;
    private float? _loadedTime;

    //The class which is used to save data to the JSON - Love
    private class SaveObject
    {
        public float _bestTime;
    }

    //This method is used to check if you should overwrite your old best time with your new one. It then returns your current best time - Love
    public float? CheckOverwrite(float newTime)
    {
        _loadedTime = Load();
        if(_loadedTime == null)
        {
            Save(newTime);
            return newTime;
        }
        else
        {
            if (newTime < _loadedTime)
            {
                Save(newTime);
                return newTime;
            }
            else return _loadedTime;
        }
    }

    //Method to save to a JSON of "_fileName" - Love
    private void Save(float _newBestTime)
    {
        SaveObject saveObject = new SaveObject()
        {
            _bestTime = _newBestTime
        };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + _fileName, json);
    }

    //Method to return a nullable float. Made the float nullabe to allow you to attempt to load and be able to run the CheckOverwrite method without there being any previous data - Love
    public float? Load()
    {
        if (File.Exists(Application.dataPath + _fileName))
        {
            string saveString = File.ReadAllText(Application.dataPath + _fileName);

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            return saveObject._bestTime;
        }
        else return null;
    }
}
