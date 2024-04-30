using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject items;
    public int numberOfItemspawn = 10;
    public float iYspread = 10;
    public float iXspread = 0;
    public float iZspread = 10;
    
    void Start()
    {
        for (int i = 0; i < numberOfItemspawn; i++)
        {
            RandomizeItem();
        }
    }

    // Update is called once per frame
    void RandomizeItem()
    {
        Vector3 randpos = new Vector3(Random.Range(-iXspread, iXspread), Random.Range(-iYspread,iYspread), Random.Range(-iZspread, iZspread));
        GameObject clone = Instantiate(items, randpos, Quaternion.identity);
        clone.transform.localScale = items.transform.localScale; // Orijinal ölçe?i koruyun
    }
}
