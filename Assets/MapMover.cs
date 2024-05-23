using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMover : AttributesSync/*,IInteractable*/
{
    public List<GameObject> players;
    public List<GameObject> preyList;
    public List<GameObject> hunterList;
    public Multiplayer mp;
    P_StateManager pManager;

    public GameObject GiveObject()
    {
        return gameObject;
    }
    //public void InitInteract(string interactor)
    //{

    //    BroadcastRemoteMethod("Interact", interactor);
            
    //}

    public List<GameObject> FindObjectsOnLayer(int layer)
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> objectsInLayer = new List<GameObject>();

        foreach (var obj in allPlayers)
        {
            if (obj.layer == layer)
            {
                objectsInLayer.Add(obj);
            }
        }

        return objectsInLayer;
    }

    //[SynchronizableMethod]
    //public void Interact(string interactor)
    //{
    //   // moveMaps();
    //}
    [SynchronizableMethod]

    public void moveMaps()
    {
        players = FindObjectsOnLayer(9);
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();


        if (scene.name == "TEMPLOBBY" || scene.name == "TEMPSTART")
        {
            for (int i = 0; i < players.Count; i++)
            {
                Transform transform = players[i].GetComponent<Transform>();
                transform.position = new Vector3(0, 3, -10 - 5 * i);
                Debug.Log(transform.position);
            }
            Multiplayer.LoadScene("GAMETEMPMAP");
        }
        else if (scene.name == "GAMETEMPMAP")
        {
            for (int i = 0; i < players.Count; i++)
            {
                Transform transform = players[i].GetComponent<Transform>();
                preyList = FindObjectsOnLayer(7);
                foreach (GameObject prey in preyList)
                {
                    if (prey.GetComponentInChildren<P_StateManager>().Escaped == true)
                    {
                        prey.GetComponentInParent<Transform>().position = new Vector3(64.5f, 16.44f, 100);
                    }
                    else if (prey.GetComponentInChildren<P_StateManager>().Escaped == false)
                    {
                        prey.GetComponentInParent<Transform>().position = new Vector3(100f, 0.8f, 100);
                    }
                }
                hunterList = FindObjectsOnLayer(6);
                foreach (GameObject hunter in hunterList)
                {
                    hunter.GetComponentInParent<Transform>().position = new Vector3(64.5f, 30, 100);
                }
                Multiplayer.LoadScene("LOBBY");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mp = new Multiplayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
