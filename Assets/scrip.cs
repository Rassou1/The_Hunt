using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaggingBoxCollisionHandler : MonoBehaviour
{
    // List to store colliding objects
    public List<GameObject> objectList = new List<GameObject>();

    void Start()
    {
        // Clear the list at the start
        objectList.Clear();
    }

    void Update()
    {
        // Update logic, if any, goes here
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null && !objectList.Contains(other.transform.root.gameObject) && other.transform.root.gameObject.name.Contains("PlayerNewPrefab"))
        {
            objectList.Add(other.transform.root.gameObject);
        }
    }


    public List<GameObject> GiveList()
    {
        return objectList;
    }

}
