using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public GameObject[] itemss;
    // Start is called before the first frame update
    void Start()
    {
        Choose();
        
    }

    // Update is called once per frame
    void Choose()
    {
        int randIndex = Random.Range(0, itemss.Length);
        GameObject clone = Instantiate(itemss[randIndex],transform.position,Quaternion.identity);
    }
}
