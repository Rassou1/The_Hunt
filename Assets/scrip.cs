using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaggingBoxCollitionHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> objectList = new List<GameObject>();

    void Start()
    {
        objectList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        objectList.Add(collision.gameObject);
        Debug.Log(objectList);
    }
}
