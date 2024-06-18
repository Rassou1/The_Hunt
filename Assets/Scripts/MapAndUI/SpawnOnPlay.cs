using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnPlay : MonoBehaviour
{
    private Spawner _spawner; 
    public List<SpawnableObject> spawnableObjects;

    private void Start()
    {
        // Find the NetworkManager GameObject and get the Spawner component from it
        GameObject networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
        if (networkManager != null)
        {
            _spawner = networkManager.GetComponent<Spawner>();
        }
        else
        {
            Debug.LogError("NetworkManager not found!");
        }

        // Spawn object for each object in the list
        foreach (SpawnableObject spawnableObject in spawnableObjects)
        {
            SpawnObject(spawnableObject.indexToSpawn, spawnableObject.transform.position,
                      spawnableObject.transform.rotation, spawnableObject.transform.localScale);
        }
    }

    public void SpawnObject(int indexToSpawn, Vector3 positionToSpawn, Quaternion rotationToSpawn, Vector3 sizeToSpawn)
    {
        if (_spawner != null)
        {
            _spawner.Spawn(indexToSpawn, positionToSpawn, rotationToSpawn, sizeToSpawn);
        }
        else
        {
            Debug.LogError("Spawner not assigned!");
        }
    }
}
[System.Serializable]
public class SpawnableObject
{
    public int indexToSpawn;
    public Transform transform;
}
