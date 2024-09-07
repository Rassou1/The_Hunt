using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TaggingBoxCollisionHandler : MonoBehaviour
{
    // List to store colliding objects
    public List<GameObject> objectList = new List<GameObject>();
    public GameObject particles;
    void Start()
    {
        // Clear the list at the start
        objectList.Clear();
    }

    void Update()
    {
        // Update logic, if any, goes here
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject != null && !objectList.Contains(other.transform.root.gameObject) && other.transform.root.gameObject.name.Contains("PlayerNewPrefab"))
    //    {
    //        objectList.Add(other.transform.root.gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // Ensure the collider and its root are not null
        if (other == null) return;

        GameObject rootObject = other.transform.root.gameObject;

        // Check if the collided object is a "PlayerNewPrefab" and not already in the list
        if (rootObject.name.Contains("PlayerNewPrefab") && !objectList.Contains(rootObject))
        {
            // Get the PreyComponent and HunterComponent from the children of the root object
            Transform prey = rootObject.transform.Find("PreyComponent");
            Transform hunter = rootObject.transform.Find("HunterComponent");

            // If the PreyComponent exists and is active, and the HunterComponent is either non-existent or inactive
            if (prey != null && prey.gameObject.activeSelf &&
                (hunter == null || !hunter.gameObject.activeSelf))
            {
                //spawn particall here plz
                Transform playerBody = prey.Find("PlayerAndBody");
                Transform head = playerBody.Find("Orientation");
                Transform body = playerBody.Find("FootRayCast");

                Instantiate(particles, body.position, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));
                Instantiate(particles, head.position, new Quaternion(-0.707106829f, 0, 0, 0.707106829f));

                // Add the root object to the objectList
                objectList.Add(rootObject);
            }
        }

    }



    public List<GameObject> GiveList()
    {
        return objectList;
    }

}
