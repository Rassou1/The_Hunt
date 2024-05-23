using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpdbug : MonoBehaviour
{
    // Start is called before the first frame update
    NewRoleGiver mm;

    void Start()
    {
        mm = FindAnyObjectByType<NewRoleGiver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            mm.resetAllPrefabs();
        }
    }
}
