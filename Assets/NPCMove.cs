using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, 6f) * Time.deltaTime;
    }
}
