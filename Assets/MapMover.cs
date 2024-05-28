using Alteruna;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
    using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMover : AttributesSync
{
    public List<GameObject> players;
    public List<GameObject> preyList;
    public List<GameObject> hunterList;
    P_StateManager pManager;

    Multiplayer networkManager;
    Transform spawn;
    PlayerStates playerStates;


    public GameObject GiveObject()
    {
        return gameObject;
    }

   

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



    [SynchronizableMethod]
    public void moveMaps(GameObject player)
    {
        
        networkManager = FindAnyObjectByType<Multiplayer>();
        spawn = networkManager.GetComponent<Transform>();
        playerStates = networkManager.GetComponent<PlayerStates>();
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(player);
        Debug.Log($"{networkManager} moveMaps1");
        Debug.Log($"{playerStates} moveMaps1");

        if ((scene.name == "LOOBY" || scene.name == "TEMPSTART") && !playerStates.gameStarted)
        {
            Debug.Log("moving from start");

            //Transform transform = player.GetComponent<Transform>();
            //transform.position = new Vector3(0, 3, -10);




            foreach (GameObject p in players)
            {
                Transform parentTransform = p.transform;
                Transform firstChild = parentTransform.Find("PreyComponent");
                Transform secondChild = parentTransform.Find("HunterComponent");
                parentTransform.position = Vector3.zero;
                int spawnLocation = Random.Range(0, 80000);
                if (firstChild.gameObject.active)
                {
                    //    if (spawnLocation <= 20000)
                    //    {
                    //        firstChild.position = playerStates.spawns[0];
                    //    }
                    //    else if (spawnLocation <= 40000 && spawnLocation > 20000)
                    //    {
                    //        firstChild.position = playerStates.spawns[1];
                    //    }
                    //    else if (spawnLocation <= 60000 && spawnLocation > 40000)
                    //    {
                    //        firstChild.position = playerStates.spawns[2];
                    //    }
                    //    else if (spawnLocation <= 80000 && spawnLocation > 60000)
                    //    {
                    //        firstChild.position = playerStates.spawns[3];
                    //    }
                    firstChild.position = new Vector3(0, 0, 0);


                }
                else
                {
                    secondChild.position = new Vector3(0, 0, 0);

                }
            }
                
                
               
                    spawn.position = new Vector3(15,5,2.4f);
            
            networkManager.LoadScene("Final_Map");
            playerStates.gameStarted = true;
            Debug.Log($"Has game started? {playerStates.gameStarted}");
        }
        else if (scene.name == "Final_Map" && playerStates.gameStarted && playerStates.gameEnded)
        {
            Debug.Log($"{playerStates} moveMaps2");
            Debug.Log("moving from smap");

            Transform transform = player.GetComponent<Transform>();
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
            networkManager = FindAnyObjectByType<Multiplayer>();
            networkManager.LoadScene("LOOBY");
            playerStates.gameStarted = false;
            Debug.Log($"Has game ended? {playerStates.gameEnded}");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        players = FindObjectsOnLayer(9);
        Debug.Log(players.Count);
        networkManager = FindAnyObjectByType<Multiplayer>();
        spawn = networkManager.GetComponent<Transform>();
        playerStates = networkManager.GetComponent<PlayerStates>();
        //Debug.Log(playerStates);
    }
}
