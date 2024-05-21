using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : AttributesSync,IInteractable
{
    public GameObject[] players;
    public GameObject GiveObject()
    {
        return gameObject;
    }
    public void InitInteract(string interactor)
    {

        players = GameObject.FindGameObjectsWithTag("Player");
        BroadcastRemoteMethod("Interact", interactor);

    }

    [SynchronizableMethod]
    public void Interact(string interactor)
    {
        for (int i = 0; i < players.Length; i++)
        {
            Rigidbody rb = players[i].GetComponent<Rigidbody>();
            rb.position = new Vector3(0, 3, -10 - 5 * i);
        }
        Multiplayer.LoadScene("GAMETEMPMAP");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
